using System;
using System.Collections.Generic;
using TankWorld.Engine;
using TankWorld.Game.Items;

namespace TankWorld.Game.Components
{
    public class TankAiComponent: AiComponent
    {
        private const int MAX_TARGET_RANGE = 100;
        private const int MAX_AGGRO_RANGE = 200;
        private const int MAX_SHOOTING_RANGE = 100;

        private TankObject targetTank;
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
            List<TankObject> nearbyTanks = new List<TankObject>();

            //Add each Tank to list only if they are in a square around "tank"
            foreach (TankObject entry in world.tanks)
            {
                if ( (Math.Abs(entry.Position.x - tank.Position.x) <= MAX_AGGRO_RANGE) && (Math.Abs(entry.Position.y - tank.Position.y) <= MAX_AGGRO_RANGE) )
                {
                    nearbyTanks.Add(entry);
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
                    tank.Shoot();
                }
                
            }
            else //if tank has no target, cannon should aim in the direction of the tank!
            {
                tank.CannonTarget = tank.Position;
            }




            //UpdateTargetSpeedvektor();


            //DriveTank();

        }


        private TankObject SearchForNewTargetTank(TankObject tank, List<TankObject> nearbyTanks)
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
