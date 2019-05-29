using TankWorld.Engine;

namespace TankWorld.Game.Models
{
    class BulletModel : EntityModel
    {

        private Coordinate position;
        private double directionAngle;
        private Camera camera;

        //Constructors
        public BulletModel(Coordinate startPosition, double startAngle)
        {
            AddSprite("Bullet", new Sprite("simpleBullet", "assets/images/simpleBullet.bmp", 0, 254, 0));
            this.position = startPosition;
            directionAngle = startAngle;
            camera = Camera.Instance;
        }

        //Accessors


        //Methods
        public override void Render()
        {
            Coordinate drawPosition;
            bool isInsideCamera = false;

            drawPosition = camera.ConvertMapToScreenCoordinate(position);

            isInsideCamera = (drawPosition.x >= 0-AllSprites["Bullet"].Pos.w)
                            && (drawPosition.x <= GameConstants.WINDOWS_X + AllSprites["Bullet"].Pos.w)
                            && (drawPosition.y >= 0 - AllSprites["Bullet"].Pos.h)
                            && (drawPosition.y <= GameConstants.WINDOWS_Y + AllSprites["Bullet"].Pos.h);

            if (isInsideCamera)
            {
                AllSprites["Bullet"].RotateAndRender(drawPosition, directionAngle, AllSprites["Bullet"].SubRect.w / 2, AllSprites["Bullet"].SubRect.h / 2);
            }
            
        }
        public void UpdatePosition(Coordinate position)
        {
            this.position = position;
        }

        public void UpdateAngle(double newAngle)
        {
            directionAngle = newAngle;
        }

    }
}
