using System;
using TankWorld.Engine;
using TankWorld.Game.Events;
using TankWorld.Game.Models;

namespace TankWorld.Game.Items
{
    public class TankObject: IRender, IUpdate
    {

        private TankModel model;
        private string ID;

        private double x = 4;
        
        //Top speed expressed in m/s
        private const double TOP_SPEED = 20;
        private const double TOP_SPEED_REVERSE = -TOP_SPEED/2;
        //degrees turned per second at MaxRate
        private const double MAX_DEGREE_PER_SECONDS_TURN = 180;
        //Max Acceleration at Maxrate expressed in m/s^2
        private const double MAX_ACCELERATION = 50;

        private const double SECONDS_TO_STOP = 2;

        private Coordinate position;
        private double speed;
        private double acceleration;
        private double turningAngle;
        private double directionBody;
        private double directionCannon;
        private Coordinate cannonTarget;

        private double forwardRate;
        private double reverseRate;
        private double turnLeftRate;
        private double turnRightRate;



        //Constructors
        public TankObject(string ID)
        {
            this.ID = ID;
            model = new TankModel(ID);
            position.x = GameConstants.WINDOWS_X/2;
            position.y = GameConstants.WINDOWS_Y/2;
            speed = 0;
            acceleration = 0;
            turningAngle = 0;
            directionBody = 3*Math.PI/2;
            cannonTarget.x = position.x;
            cannonTarget.y = position.y;
            UpdateCannonDirection();
            model.UpdateModel(position, directionBody, directionCannon);

        }

        //Accessors


        //Methods
        public void Render()
        {
            model.Render();
        }

        public void Update()
        {
            UpdateDirection();
            UpdateSpeed();
            UpdateCoordinates();
            UpdateCannonDirection();
            model.UpdateModel(position, directionBody, directionCannon);
        }

        private void UpdateDirection()
        {
            directionBody += turningAngle *GameConstants.MS_PER_UPDATE*1/1000;
            while(directionBody < 0)
            {
                directionBody += 2 * Math.PI;
            }
            while (directionBody > 2 * Math.PI)
            {
                directionBody -= 2 * Math.PI;
            }
        }

        private void UpdateSpeed()
        {
            double oldSpeed = speed;
            double a =  TOP_SPEED/Math.Pow(SECONDS_TO_STOP, x);
            double t = 10 - (Math.Pow(Math.Abs(oldSpeed), 1.0/x) / Math.Pow(a, 1.0/x) );
            double speedDecay = -x*a*Math.Pow( 10 - t, 1.0/(x-1) );
            
            //Apply speed_decay on top of acceleration
            if (speed < 0)
            {
                speed -= speedDecay * GameConstants.MS_PER_UPDATE * 1 / 1000;
            } else if (speed > 0)
            {
                speed += speedDecay * GameConstants.MS_PER_UPDATE * 1 / 1000;
            }
            //If there is no acceleration, allow tank to completely stop
            if (acceleration == 0)
            {
                //If speed goes from negative to positive (or the other way around), set it to 0
                if ( (oldSpeed < 0 && speed >0) || (oldSpeed > 0 && speed < 0) )
                {
                    speed = 0;
                }
                
            }
            //Add Acceleration to currentSpeed. Avoid going over TOP_SPEED!


            double trueAccel = acceleration - Math.Pow(oldSpeed/TOP_SPEED, x)* acceleration ;


            speed += trueAccel * GameConstants.MS_PER_UPDATE * 1 / 1000;
            if (speed > TOP_SPEED)
            {
                speed = TOP_SPEED;
            }
            else if(speed < TOP_SPEED_REVERSE)
            {
                speed = TOP_SPEED_REVERSE;
            }
            Console.WriteLine("Speed: "+ speed + "Decay: " + speedDecay);
            

        }

        private void UpdateCoordinates()
        {
            position.x += speed * Math.Cos(directionBody) * GameConstants.MS_PER_UPDATE * 1 / 1000 * GameConstants.METER_TO_PIXEL;
            position.y += speed * Math.Sin(directionBody) * GameConstants.MS_PER_UPDATE * 1 / 1000 * GameConstants.METER_TO_PIXEL;
        }

        private void UpdateCannonDirection()
        {
            Coordinate turretCoord = model.GetTurretPosition();
            //If mouse is at the same pixel as the turret center, don't calculate angle.
            if ((cannonTarget.y - turretCoord.y != 0) && (cannonTarget.x - turretCoord.x != 0))
            {
                directionCannon = Math.Atan2(cannonTarget.y - turretCoord.y, cannonTarget.x - turretCoord.x);
            }
            

        }

        
        private void Accelerate()
        {
            this.acceleration = (reverseRate+forwardRate) * MAX_ACCELERATION;
        }
        private void Turn()
        {
            turningAngle = (turnLeftRate+turnRightRate) * MAX_DEGREE_PER_SECONDS_TURN * Math.PI/180;
        }

        public void Forward(double rate)
        {
            forwardRate = rate;
            Accelerate();
        }
        public void Reverse(double rate)
        {
            reverseRate = -rate;
            Accelerate();
        }
        public void TurnLeft(double rate)
        {
            turnLeftRate = -rate;
            Turn();
        }
        public void TurnRight(double rate)
        {
            turnRightRate = rate;
            Turn();
        }

        public void TurretTarget(int x, int y)
        {
            cannonTarget.x = x;
            cannonTarget.y = y;
        }
        public void Shoot()
        {
            BulletObject bullet = new BulletObject(this, model.GetBarrelEndPosition(), directionCannon);
            MainEventBus.PostEvent(new SceneStateEvent(SceneStateEvent.Type.SPAWN_BULLET_ENTITY, bullet));
        }

    }
}