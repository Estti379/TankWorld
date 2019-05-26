using System;
using System.Collections.Generic;
using TankWorld.Engine;
using TankWorld.Game.Items;

namespace TankWorld.Game.Models
{
    public class TankModel : EntityModel
    {
        private string ID;

        private Coordinate bodyPosition;
        private double directionAngle;
        private double directionCannon;
        private Coordinate turretPosition;
        private Coordinate cannonPosition;

        private Camera camera;

        //Constructors
        public TankModel(string ID)
        {
            this.ID = ID;
            AddSprite("TankBody", new Sprite(ID+"TankBody", "assets/images/TankBody.bmp", 0, 254, 0) );
            AddSprite("TankTurret", new Sprite(ID+"TankTurret", "assets/images/TankTurret.bmp", 0, 254, 0));
            AddSprite("TankCannon", new Sprite(ID + "TankCannon", "assets/images/TankCannon.bmp", 0, 254, 0));
            camera = Camera.Instance;

        }

        //Accessors


        //Methods

        public override void Render()
        {
            Coordinate drawPosition;

            drawPosition.x = bodyPosition.x - camera.Position.x + GameConstants.WINDOWS_X / 2;
            drawPosition.y = bodyPosition.y - camera.Position.y + GameConstants.WINDOWS_Y / 2;

            AllSprites["TankBody"].RotateAndRender(drawPosition, directionAngle, AllSprites["TankBody"].SubRect.w/2, AllSprites["TankBody"].SubRect.h/2);

            drawPosition.x = cannonPosition.x - camera.Position.x + GameConstants.WINDOWS_X / 2;
            drawPosition.y = cannonPosition.y - camera.Position.y + GameConstants.WINDOWS_Y / 2;

            AllSprites["TankCannon"].RotateAndRender(drawPosition, directionCannon, AllSprites["TankCannon"].SubRect.w / 2, AllSprites["TankCannon"].SubRect.h / 2);

            drawPosition.x = turretPosition.x - camera.Position.x + GameConstants.WINDOWS_X / 2;
            drawPosition.y = turretPosition.y - camera.Position.y + GameConstants.WINDOWS_Y / 2;

            AllSprites["TankTurret"].RotateAndRender(drawPosition, directionCannon, AllSprites["TankTurret"].SubRect.w / 2, AllSprites["TankTurret"].SubRect.h / 2);

        }
        public void UpdateModel(TankObject tank, double directionBody, double directionCannon)
        {


            this.bodyPosition = tank.Position;
            this.turretPosition = tank.GetTurretPosition();
            this.cannonPosition = tank.GetCannonPosition();
            directionAngle = directionBody;
            this.directionCannon = directionCannon;
        }

    }
}
