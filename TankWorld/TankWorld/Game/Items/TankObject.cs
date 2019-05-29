using System;
using TankWorld.Engine;
using TankWorld.Game.Commands;
using TankWorld.Game.Components;
using TankWorld.Game.Events;
using TankWorld.Game.Models;

namespace TankWorld.Game.Items
{
    public class TankObject: IRender
    {

        public enum TankColor
        {
            PLAYER,
            GREEN
        }
        public enum Faction
        {
            PLAYER,
            AI
        }

        private AiComponent aiComponent;

        private TankModel model;
        private TankColor color;
        private Faction currentFaction;

        private Camera camera;
        //timers in milliseconds
        private Timer BulletSalvoTimer;
        private Timer CannonCooldownTimer;

        private double x = 4;
        
        //Top speed expressed in m/s
        private const double TOP_SPEED = 20;
        private const double TOP_SPEED_REVERSE = -TOP_SPEED/2;
        //degrees turned per second at MaxRate
        private const double MAX_DEGREE_PER_SECONDS_TURN = 180;
        //Max Acceleration at Maxrate expressed in m/s^2
        private const double MAX_ACCELERATION = 50;

        private const double SECONDS_TO_STOP = 2;

        //Weapon constants
        /*TODO: looks like weapons could become it's own instance of a class "Weapon"
         *which would allow for weapon modularity!
         */
        private const int CANNON_SALVO_PROJECTILE_NUMBER = 4;
        private const int CANNON_COOLDOWN = 1000; //Time in milliseconds before cannon can shoot new salvo
        //A thirst percentage of cannon cooldown time is used to shoot salvo. Each bullet is spread out evenly
        //Keep in mind that one bullet is shot instantly (thus the -1)
        private const double CANNON_BULLET_COOLDOWN = (10.0/100) * (double)CANNON_COOLDOWN / (CANNON_SALVO_PROJECTILE_NUMBER-1); 

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
        public TankObject(Coordinate spawnPosition, TankColor type)
        {
            camera = Camera.Instance;
            this.color = type;
            model = new TankModel(this.color);
            position = spawnPosition;
            speed = 0;
            acceleration = 0;
            turningAngle = 0;
            directionBody = 3*Math.PI/2;
            cannonTarget = position; //So that cannon is facing forward
            cannonTarget.x = (model.AllSprites["TankBody"].SubRect.w) * Math.Cos(directionBody) + position.x;
            cannonTarget.y = (model.AllSprites["TankBody"].SubRect.w) * Math.Sin(directionBody) + position.y;
            UpdateCannonDirection();
            model.UpdateModel(this, directionBody, directionCannon);
            InitializeTimers();

            if(color == TankColor.PLAYER)
            {
                aiComponent = new DefaultAiComponent();
                currentFaction = Faction.PLAYER;
            }
            else
            {
                aiComponent = new TankAiComponent();
                currentFaction = Faction.AI;
            }

        }

        private void InitializeTimers()
        {
            BulletSalvoTimer = new Timer(Timer.Type.DESCENDING);
            BulletSalvoTimer.Pause();
            BulletSalvoTimer.DefaultTime = CANNON_BULLET_COOLDOWN*(CANNON_SALVO_PROJECTILE_NUMBER-1);
            BulletSalvoTimer.ExecuteTime = 0;
            BulletSalvoTimer.Command = new SalvoShotCommand(this, BulletSalvoTimer, CANNON_BULLET_COOLDOWN);

            CannonCooldownTimer = new Timer(Timer.Type.PAUSE_AT_ZERO);
            CannonCooldownTimer.Time = 0;
            CannonCooldownTimer.Pause();
            CannonCooldownTimer.DefaultTime = CANNON_COOLDOWN;
            CannonCooldownTimer.ExecuteTime = CANNON_COOLDOWN * 2;//avoid execution
        }

        //Accessors

        public Coordinate Position
        {
            get { return position; }
        }

        public Faction CurrentFaction { get => currentFaction;}
        public Coordinate CannonTarget { get => cannonTarget; set => cannonTarget = value; }
        public double DirectionCannon { get => directionCannon;}


        //Methods
        public void Render()
        {
            model.Render();
        }

        public void Update(ref WorldItems world)
        {
            aiComponent.Update(this, ref world);
            UpdateDirection();
            UpdateSpeed();
            UpdateCoordinates();
            UpdateCannonDirection();
            model.UpdateModel(this, directionBody, directionCannon);
            UpdateTimers();
        }

        private void UpdateTimers()
        {
            BulletSalvoTimer.Update();
            CannonCooldownTimer.Update();
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
            

        }

        private void UpdateCoordinates()
        {
            position.x += speed * Math.Cos(directionBody) * GameConstants.MS_PER_UPDATE * 1 / 1000 * GameConstants.METER_TO_PIXEL;
            position.y += speed * Math.Sin(directionBody) * GameConstants.MS_PER_UPDATE * 1 / 1000 * GameConstants.METER_TO_PIXEL;
        }

        public void UpdateCannonDirection()
        {
            Coordinate turretCoord = GetTurretPosition();

            /* TODO: a player tank has a different behaviour than an not player tank.
             * Create a new Class wich extends tankobject and override this method?
             */
            if(color == TankColor.PLAYER) //Player cannon should follow current mouse position on the screen
            {
                cannonTarget.x += camera.Position.x - camera.OldPosition.x;
                cannonTarget.y += camera.Position.y - camera.OldPosition.y;
            }
            else //All Other tanks shouldn't change their target location, since they are targeting a mapLocation, not a screen location, like the player
            {
                /*empty*/
            }
            

            //If mouse is at the same pixel as the turret center, don't calculate angle.
            if ( !( (cannonTarget.y - turretCoord.y == 0) && (cannonTarget.x - turretCoord.x == 0) ) )
            {
                directionCannon = Math.Atan2(cannonTarget.y - turretCoord.y, cannonTarget.x - turretCoord.x);
            }

            while (directionCannon < 0)
            {
                directionCannon += 2 * Math.PI;
            }
            while (directionCannon > 2 * Math.PI)
            {
                directionCannon -= 2 * Math.PI;
            }


        }

        public Coordinate GetTurretPosition()
        {
            Coordinate turretCoord;
            turretCoord.x = (model.AllSprites["TankBody"].SubRect.w / 4) * Math.Cos(directionBody + Math.PI) + position.x;
            turretCoord.y = (model.AllSprites["TankBody"].SubRect.w / 4) * Math.Sin(directionBody + Math.PI) + position.y;

            return turretCoord;
        }

        public Coordinate GetCannonPosition()
        {
            Coordinate turretCoord = GetTurretPosition();
            Coordinate cannonCoord;

            cannonCoord.x = (model.AllSprites["TankCannon"].SubRect.w / 2) * Math.Cos(directionCannon) + turretCoord.x;
            cannonCoord.y = (model.AllSprites["TankCannon"].SubRect.w / 2) * Math.Sin(directionCannon) + turretCoord.y;

            return cannonCoord;
        }

        public Coordinate GetBarrelEndPosition()
        {
            Coordinate turretCoord = GetTurretPosition();
            Coordinate barrelCoord;

            barrelCoord.x = (model.AllSprites["TankTurret"].SubRect.w) * Math.Cos(directionCannon) + turretCoord.x;
            barrelCoord.y = (model.AllSprites["TankTurret"].SubRect.w) * Math.Sin(directionCannon) + turretCoord.y;

            return barrelCoord;
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
            cannonTarget = camera.ConvertScreenToMapCoordinate(cannonTarget);


        }
        public void Shoot()
        {
            if (CannonCooldownTimer.Time <= 0)
            {
                CannonCooldownTimer.Reset();
                CannonCooldownTimer.UnPause();
                BulletSalvoTimer.Reset();
                BulletSalvoTimer.UnPause();
                BulletSalvoTimer.ExecuteTime = BulletSalvoTimer.DefaultTime;
            }
        }

    }
}