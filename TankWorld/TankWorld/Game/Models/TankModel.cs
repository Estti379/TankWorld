using System;
using System.Collections.Generic;
using TankWorld.Engine;

namespace TankWorld.Game.Models
{
    public class TankModel : EntityModel
    {
        private string ID;

        private Coordinate bodyPosition;
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
            AllSprites["TankBody"].RotateAndRender(bodyPosition, directionAngle, AllSprites["TankBody"].SubRect.w/2, AllSprites["TankBody"].SubRect.h/2);

            AllSprites["TankCannon"].RotateAndRender(GetCannonPosition(), directionCannon, AllSprites["TankCannon"].SubRect.w / 2, AllSprites["TankCannon"].SubRect.h / 2);

            AllSprites["TankTurret"].RotateAndRender(GetTurretPosition(), directionCannon, AllSprites["TankTurret"].SubRect.w / 2, AllSprites["TankTurret"].SubRect.h / 2);

        }
        public void UpdateModel(Coordinate bodyPosition, double directionBody, double directionCannon)
        {
            this.bodyPosition = bodyPosition;
            directionAngle = directionBody;
            this.directionCannon = directionCannon;
        }

        public Coordinate GetTurretPosition()
        {
            Coordinate turretCoord;
            turretCoord.x = (AllSprites["TankBody"].SubRect.w / 4) * Math.Cos(directionAngle + Math.PI) + bodyPosition.x;
            turretCoord.y = (AllSprites["TankBody"].SubRect.w / 4) * Math.Sin(directionAngle + Math.PI) + bodyPosition.y;

            return turretCoord;
        }

        public Coordinate GetCannonPosition()
        {
            Coordinate turretCoord = GetTurretPosition();
            Coordinate cannonCoord;

            cannonCoord.x = (AllSprites["TankCannon"].SubRect.w / 2) * Math.Cos(directionCannon) + turretCoord.x;
            cannonCoord.y = (AllSprites["TankCannon"].SubRect.w / 2) * Math.Sin(directionCannon) + turretCoord.y;

            return cannonCoord;
        }

        public Coordinate GetBarrelEndPosition()
        {
            Coordinate turretCoord = GetTurretPosition();
            Coordinate barrelCoord;

            barrelCoord.x = (AllSprites["TankTurret"].SubRect.w) * Math.Cos(directionCannon) + turretCoord.x;
            barrelCoord.y = (AllSprites["TankTurret"].SubRect.w) * Math.Sin(directionCannon) + turretCoord.y;

            return barrelCoord;
        }

    }
}
