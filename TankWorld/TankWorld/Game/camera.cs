using TankWorld.Engine;
using TankWorld.Game.Items;

namespace TankWorld.Game
{
    public class Camera: IUpdate
    {
        static private Camera singleton = null;

        private Coordinate position;
        private Coordinate oldPosition;
        private Coordinate targetPosition;
        //Constructors
        private Camera()
        {
            position.x = 0;
            position.y = 0;
            oldPosition = position;
        }

        public static Camera Instance
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new Camera();
                }
                return singleton;
            }
        }

        //Accessors
        public Coordinate Position
        {
            get { return position; }
        }
        public Coordinate OldPosition
        {
            get { return oldPosition; }
        }

        //Methods
        public void UpdateTargetPosition(TankObject player)
        {
            targetPosition = player.Position;
        }
        public void Update()
        {
            oldPosition = position;
            position = targetPosition;
        }
    }
}
