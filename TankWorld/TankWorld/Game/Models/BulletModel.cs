using TankWorld.Engine;

namespace TankWorld.Game.Models
{
    class BulletModel : EntityModel
    {

        private Coordinate position;
        private double directionAngle;

        //Constructors
        public BulletModel(Coordinate startPosition, double startAngle)
        {
            AddSprite("Bullet", new Sprite("simpleBullet", "assets/images/simpleBullet.bmp", 0, 254, 0));
            this.position = startPosition;
            directionAngle = startAngle;
        }

        //Accessors


        //Methods
        public override void Render()
        {
            AllSprites["Bullet"].RotateAndRender(position, directionAngle, AllSprites["Bullet"].SubRect.w / 2, AllSprites["Bullet"].SubRect.h / 2);
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
