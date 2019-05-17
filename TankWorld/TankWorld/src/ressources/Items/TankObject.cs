using System;
using TankWorld.src.ressources.Models;

namespace TankWorld.src.ressources.Items
{
    public class TankObject: IRender, IUpdate
    {

        private TankModel model;
        private string ID;

        private const double MULT = GameConstants.MS_PER_UPDATE/500;
        private const double TOP_SPEED = 100 * MULT;
        private const double TOP_SPEED_REVERSE = -50 * MULT;
        private const double SPEED_DECAY = 0.1 * MULT;

        private Coordinate position;
        private double speed;
        private double acceleration;
        private double turningAngle;
        private double directionBody;
        private double directionCannon;
        private Coordinate cannonTarget;

        //Constructors
        public TankObject(string ID)
        {
            this.ID = ID;
            model = new TankModel(ID);
            position.x = 200;
            position.y = 200;
            speed = 0;
            acceleration = 0;
            turningAngle = 0;
            directionBody = 0;
            directionCannon = 0;
            cannonTarget.x = 0;
            cannonTarget.y = 0;
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
            directionBody = (directionBody + turningAngle);
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
            //Decay Tanks speed if there is no acceleration
            if (acceleration == 0)
            {
                if(speed < (-SPEED_DECAY) )
                {
                    speed += SPEED_DECAY;
                }
                /*If currentSpeed is small enough that using it would make
                *currentSpeed overshoot 0, set it to 0!
                */
                else if ( (speed > (-SPEED_DECAY) ) && (speed < SPEED_DECAY) )
                {
                    speed = 0;
                }
                else if (speed > SPEED_DECAY)
                {
                    speed -= SPEED_DECAY;
                }
                //If currentSpeed is already 0, do nothing!
            }
            else // Acceleration is not 0: add it to currentSpeed. Avoid going over TOP_SPEED!
            {
                speed += acceleration;
                if (speed > TOP_SPEED)
                {
                    speed = TOP_SPEED;
                }
                else if(speed < TOP_SPEED_REVERSE)
                {
                    speed = TOP_SPEED_REVERSE;
                }
            }

        }

        private void UpdateCoordinates()
        {
            position.x += speed * Math.Cos(directionBody);
            position.y += speed * Math.Sin(directionBody);
        }

        private void UpdateCannonDirection()
        {
            Coordinate turretCoord = model.GetTurretPosition();
            directionCannon = Math.Atan2(cannonTarget.y - turretCoord.y, cannonTarget.x - turretCoord.x);

        }

        public void Accelerate(double acceleration)
        {
            this.acceleration = acceleration * MULT;
        }
        public void Turn(double turnAngle)
        {
            turningAngle = turnAngle * MULT;
        }

        public void TurretTarget(int x, int y)
        {
            cannonTarget.x = x;
            cannonTarget.y = y;
        }
        public void Shoot()
        {

        }

    }
}