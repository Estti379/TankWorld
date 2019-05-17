using System;
using System.Collections.Generic;

namespace TankWorld.src.ressources.Models
{
    public class TankModel : EntityModel
    {
        private string ID;

        private double xCoordinates;
        private double yCoordinates;
        private double directionAngle;
        private double directionCannon;

        //Constructors
        public TankModel(string ID)
        {
            this.ID = ID;
            AddSprite("TankBody", new Sprite(ID+"TankBody", "assets/images/TankBody.bmp", 0, 254, 0) );
            AddSprite("TankTurret", new Sprite(ID+"TankTurret", "assets/images/TankTurret.bmp", 0, 254, 0));
            AddSprite("TankCannon", new Sprite(ID + "TankCannon", "assets/images/TankCannon.bmp", 0, 254, 0));

        }

        //Accessors


        //Methods

        public override void Render()
        {
            AllSprites["TankBody"].RotateAndRender(xCoordinates, yCoordinates, directionAngle, AllSprites["TankBody"].SubRect.w/2, AllSprites["TankBody"].SubRect.h/2);

            double turretX = (AllSprites["TankBody"].SubRect.w / 4) * Math.Cos(directionAngle + Math.PI) + xCoordinates;
            double turretY = (AllSprites["TankBody"].SubRect.w / 4) * Math.Sin(directionAngle + Math.PI) + yCoordinates;

            double cannonX = AllSprites["TankCannon"].SubRect.w * Math.Cos(directionCannon) + turretX;
            double cannonY = AllSprites["TankCannon"].SubRect.w * Math.Sin(directionCannon) + turretY;

            AllSprites["TankCannon"].RotateAndRender(cannonX, cannonY, directionCannon, 0, AllSprites["TankCannon"].SubRect.h / 2);

            AllSprites["TankTurret"].RotateAndRender(turretX, turretY, directionCannon, AllSprites["TankTurret"].SubRect.w / 2, AllSprites["TankTurret"].SubRect.h / 2);

        }
        public void UpdateModel(double x, double y, double directionBody, double directionCannon)
        {
            xCoordinates = x;
            yCoordinates = y;
            directionAngle = directionBody;
            this.directionCannon = directionCannon;
        }
    }
}
