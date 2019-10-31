using System;
using System.Collections.Generic;
using TankWorld.Engine;
using TankWorld.Game.Items;

namespace TankWorld.Game.Models
{
    public class TankModel : EntityModel
    {

        private Coordinate bodyPosition;
        private double directionAngle;
        private double directionCannon;
        private Coordinate turretPosition;
        private Coordinate cannonPosition;

        private bool showHP;

        private Camera camera;

        //Constructors
        public TankModel(TankObject.TankColor type)
        {
            bool showHP = false;
            string tankBodyPath = null;
            string tankTurretPath = null;
            string tankCannonPath = null;

            GetImagePaths(type, ref tankBodyPath, ref tankTurretPath, ref tankCannonPath);

            AddSprite("TankBody", new Sprite(type + "TankBody", tankBodyPath, 0, 254, 0));
            AddSprite("TankTurret", new Sprite(type + "TankTurret", tankTurretPath, 0, 254, 0));
            AddSprite("TankCannon", new Sprite(type + "TankCannon", tankCannonPath, 0, 254, 0));
            AddSprite("HPBar", new Sprite("HPBar", "assets/images/HPBar.bmp", 0, 254, 0));
            camera = Camera.Instance;

        }

        



        //Accessors
            public bool ShowHP { get => showHP; set => showHP = value; }

        //Methods

        public override void Render(RenderLayer layer)
        {

            Coordinate drawPosition = camera.ConvertMapToScreenCoordinate(bodyPosition);

            AllSprites["TankBody"].RotateAndRender(drawPosition, directionAngle, AllSprites["TankBody"].SubRect.w / 2, AllSprites["TankBody"].SubRect.h / 2);

            drawPosition = camera.ConvertMapToScreenCoordinate(cannonPosition);

            AllSprites["TankCannon"].RotateAndRender(drawPosition, directionCannon, AllSprites["TankCannon"].SubRect.w / 2, AllSprites["TankCannon"].SubRect.h / 2);

            drawPosition = camera.ConvertMapToScreenCoordinate(turretPosition);

            AllSprites["TankTurret"].RotateAndRender(drawPosition, directionCannon, AllSprites["TankTurret"].SubRect.w / 2, AllSprites["TankTurret"].SubRect.h / 2);

            if (showHP)
            {
                drawPosition.x = bodyPosition.x;
                drawPosition.y = bodyPosition.y + AllSprites["TankBody"].Pos.w + 3;
                drawPosition = camera.ConvertMapToScreenCoordinate(drawPosition);
                AllSprites["HPBar"].RotateAndRender(drawPosition, 0, AllSprites["HPBar"].SubRect.w / 2, AllSprites["HPBar"].SubRect.h / 2);
            }
            

        }
        public void UpdateModel(TankObject tank, double directionBody, double directionCannon)
        {


            this.bodyPosition = tank.Position;
            this.turretPosition = tank.GetTurretPosition();
            this.cannonPosition = tank.GetCannonPosition();
            directionAngle = directionBody;
            this.directionCannon = directionCannon;

            AllSprites["HPBar"].Pos.w = AllSprites["HPBar"].SubRect.w * tank.CurrentHP/tank.MaxHP;
        }

        private void GetImagePaths(TankObject.TankColor type, ref string tankBodyPath, ref string tankTurretPath, ref string tankCannonPath)
        {
            tankCannonPath = "assets/images/TankCannon.bmp";
            switch (type)
            {
                case TankObject.TankColor.PLAYER:
                    tankBodyPath = "assets/images/TankBodyPlayer.bmp";
                    tankTurretPath = "assets/images/TankTurretPlayer.bmp";
                    break;
                case TankObject.TankColor.GREEN:
                    tankBodyPath = "assets/images/TankBodyGreen.bmp";
                    tankTurretPath = "assets/images/TankTurretGreen.bmp";
                    break;
                default:
                    tankCannonPath = null;
                    Console.WriteLine("Couldn't create TankModel because unknown type was given!");
                    break;
            }
        }
    }
}
