using TankWorld.Engine;

namespace TankWorld.Game.Models
{
    public class BulletModel : EntityModel
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
        public override void Render(RenderLayer layer)
        {

            Coordinate drawPosition = camera.ConvertMapToScreenCoordinate(position);
            AllSprites["Bullet"].RotateAndRender(drawPosition, directionAngle, AllSprites["Bullet"].SubRect.w / 2, AllSprites["Bullet"].SubRect.h / 2);
            
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
