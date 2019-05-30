using System;
using System.Collections.Generic;
using TankWorld.Engine;
using TankWorld.Game.Items;

namespace TankWorld.Game.Components
{
    public class TankAiComponent: AiComponent
    {
        private const int MAX_TARGET_RANGE = 600;
        private const int MAX_AGGRO_RANGE = 1000;
        private const int MAX_SHOOTING_RANGE = 600;

        private const double SHOOTING_PROBABILITY = 0.75 / 100;

        //How fast this AI is compared to normal tank speed rate
        private const double ACCELERATION_RATE = 0.5;

        private TankObject targetTank;
        private double angleFromTarget; //in rad
        private Coordinate targetSpeedVektor;

        
        //Constructor
        public TankAiComponent()
        {
            targetTank = null;

        }

        //Accessors


        //Methods
        override public void Update(TankObject tank, ref WorldItems world)
        {
            HashSet<TankObject> nearbyTanks = new HashSet<TankObject>();

            //Add each Tank to list only if they are in a square around "tank"
            foreach (GameObject entry in world.allObjects)
            {
                TankObject tankEntry = entry as TankObject;
                if (tankEntry != null)
                {
                    if ( (Math.Abs(tankEntry.Position.x - tank.Position.x) <= MAX_AGGRO_RANGE) && (Math.Abs(tankEntry.Position.y - tank.Position.y) <= MAX_AGGRO_RANGE) )
                    {
                        nearbyTanks.Add(tankEntry);
                    }
                }                    

            }

            if ((Math.Abs(world.player.Position.x - tank.Position.x) <= MAX_AGGRO_RANGE) && (Math.Abs(world.player.Position.y - tank.Position.y) <= MAX_AGGRO_RANGE))
            {
                nearbyTanks.Add(world.player);
            }



            if (targetTank == null)
            {
                //Look for new targetTank
                targetTank = SearchForNewTargetTank(tank, nearbyTanks);
                if(targetTank != null)
                {
                    angleFromTarget = Math.PI * Helper.random.NextDouble(); //returns number between 0 and 2 * PI
                }
            }
            else if ( Helper.Distance(tank.Position, targetTank.Position) > MAX_TARGET_RANGE)
            {
                //Look for new targetTank and change to it. It might decide to keep same target.
                targetTank = SearchForNewTargetTank(tank, nearbyTanks);
            }//If current Target distance < MAX_TARGET_RANGE, then target doesn't change at all

            
            //Update CannonTarget position of "tank"
            if(targetTank != null)
            {
                tank.CannonTarget = targetTank.Position;
                tank.UpdateCannonDirection();
                //Try to shoot target if it is in the shooting range
                if(Helper.Distance(tank.Position, targetTank.Position) <= MAX_SHOOTING_RANGE)
                {
                    double random = Helper.random.NextDouble();
                    if (random >= 1-SHOOTING_PROBABILITY)
                    {
                        tank.Shoot();
                    }
                    
                }
                
            }
            else //if tank has no target, cannon should aim in the direction of the tank!
            {
                tank.CannonTarget = tank.Position;
            }



            if (targetTank != null)
            {
                //UpdateTargetSpeedvektor();
                angleFromTarget += Math.PI / 1000.0;
                angleFromTarget = Helper.NormalizeRad(angleFromTarget);
                Coordinate targetPosition;
                targetPosition.x = targetTank.Position.x + MAX_SHOOTING_RANGE * 0.9 * Math.Cos(angleFromTarget);
                targetPosition.y = targetTank.Position.y + MAX_SHOOTING_RANGE * 0.9 * Math.Sin(angleFromTarget);

                double angleToTargetPosition;
                Coordinate delta;
                delta.x = targetPosition.x - tank.Position.x;
                delta.y = targetPosition.y - tank.Position.y;
                if (delta.x == 0 && delta.y == 0) //Just in case targetposition = current tank position
                {
                    angleToTargetPosition = tank.DirectionBody;
                }
                else
                {
                    angleToTargetPosition = Math.Atan2(delta.y, delta.x);
                }

                targetSpeedVektor.x = tank.GetTopSpeed() * Math.Cos(angleToTargetPosition);
                targetSpeedVektor.y = tank.GetTopSpeed() * Math.Sin(angleToTargetPosition);
            } else
            {
                targetSpeedVektor.x = 0;
                targetSpeedVektor.y = 0;
            }


            if (targetSpeedVektor.x == 0 && targetSpeedVektor.y == 0)
            {
                tank.TurnLeft(0);
                tank.TurnRight(0);
                tank.Reverse(0);
                tank.Forward(0);
            }
            else
            {
                //DriveTank();
                double turnDirection = tank.DirectionBody - Math.Atan2(targetSpeedVektor.y, targetSpeedVektor.x);
                //Bring turn direction back to between -Pi and +Pi
                if (turnDirection < -Math.PI)
                {
                    turnDirection += 2 * Math.PI;
                }
                else if ((turnDirection > Math.PI))
                {
                    turnDirection -= 2 * Math.PI;
                }

                if (turnDirection == 0)
                {//tank is already on the right trajectory
                 /*Keep moving Forward*/
                    tank.TurnLeft(0);
                    tank.TurnRight(0);
                    tank.Reverse(0);
                    tank.Forward(ACCELERATION_RATE);
                }
                else if (turnDirection == Math.PI || turnDirection == -Math.PI)
                {//targetvektor is 180degres behind tank
                    tank.TurnLeft(0);
                    tank.TurnRight(0);
                    tank.Reverse(ACCELERATION_RATE);
                    tank.Forward(0);
                }
                else if (turnDirection > 0)
                {//if positive, turn left
                    tank.TurnLeft(1);
                    tank.TurnRight(0);
                    tank.Reverse(0);
                    tank.Forward(ACCELERATION_RATE);
                }
                else if (turnDirection < 0)
                {//if negative, turn right
                    tank.TurnLeft(0);
                    tank.TurnRight(1);
                    tank.Reverse(0);
                    tank.Forward(ACCELERATION_RATE);
                }
            }




        }


        private TankObject SearchForNewTargetTank(TankObject tank, HashSet<TankObject> nearbyTanks)
        {
            double currentLowestDistance = -1;
            double tempDistance = -1;
            TankObject chosenTarget = null;

            foreach(TankObject entry in nearbyTanks)
            {
                //Just try to target tanks of a different Faction!
                if (entry.CurrentFaction != tank.CurrentFaction)
                {
                    tempDistance = Helper.Distance(entry.Position, tank.Position);
                    if ( (currentLowestDistance == -1) || (tempDistance < currentLowestDistance))
                    {
                        currentLowestDistance = tempDistance;
                        chosenTarget = entry;
                    }
                }
            }

            //Refuse target if it is outside aggro range
            if (currentLowestDistance > MAX_AGGRO_RANGE)
            {
                chosenTarget = null;
            }

            return chosenTarget;
        }
    }
}
