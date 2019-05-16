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

        private double xCoordinates;
        private double yCoordinates;
        private double speed;
        private double acceleration;
        private double turningAngle;
        private double directionAngle;
        
        //Constructors
        public TankObject(string ID)
        {
            this.ID = ID;
            model = new TankModel(ID);
            xCoordinates = 200;
            yCoordinates = 200;
            speed = 0;
            acceleration = 0;
            turningAngle = 0;
            directionAngle = 0;
            model.UpdateModel(xCoordinates, yCoordinates, directionAngle);

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
            model.UpdateModel(xCoordinates, yCoordinates, directionAngle);
        }

        private void UpdateDirection()
        {
            directionAngle = (directionAngle + turningAngle);
            while(directionAngle < 0)
            {
                directionAngle += 2 * Math.PI;
            }
            while (directionAngle > 2 * Math.PI)
            {
                directionAngle -= 2 * Math.PI;
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
            xCoordinates += speed * Math.Cos(directionAngle);
            yCoordinates += speed * Math.Sin(directionAngle);
        }

        public void Accelerate(double acceleration)
        {
            this.acceleration = acceleration * MULT;
        }
        public void Turn(double turnAngle)
        {
            turningAngle = turnAngle * MULT;
        }

    }
}