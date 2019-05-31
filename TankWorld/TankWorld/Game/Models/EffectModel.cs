

using TankWorld.Engine;

namespace TankWorld.Game.Models
{
    abstract public class EffectModel: EntityModel
    {
        private Coordinate position;
        private Camera camera;

        //Constructors
        public EffectModel()
        {
            camera = Camera.Instance;
        }

        public Camera Camera { get => camera;}
        public Coordinate Position { get => position; set => position = value; }

        //Accessors


        //Methods
    }
}
