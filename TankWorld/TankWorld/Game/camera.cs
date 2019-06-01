using System;
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

        public Coordinate ConvertScreenToMapCoordinate(Coordinate screenCoord)
        {
            Coordinate mapCoord;
            mapCoord.x = screenCoord.x + position.x - GameConstants.WINDOWS_X / 2;
            mapCoord.y = screenCoord.y + position.y - GameConstants.WINDOWS_Y / 2;

            return mapCoord;
        }

        public Coordinate ConvertMapToScreenCoordinate(Coordinate mapCoord)
        {
            Coordinate screenCoord;
            screenCoord.x = mapCoord.x - position.x + GameConstants.WINDOWS_X / 2;
            screenCoord.y = mapCoord.y - position.y + GameConstants.WINDOWS_Y / 2;

            return screenCoord;
        }

        public bool IsInsideCamera(Coordinate position, int width, int height)
        {

            Coordinate drawPosition = ConvertMapToScreenCoordinate(position);
            return  (drawPosition.x >= 0 - width)
                            && (drawPosition.x <= GameConstants.WINDOWS_X + width)
                            && (drawPosition.y >= 0 - height)
                            && (drawPosition.y <= GameConstants.WINDOWS_Y + height);
        }
    }
}
