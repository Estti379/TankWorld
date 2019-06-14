using System;
using SDL2;
using TankWorld.Engine;
using TankWorld.Game.Items;
using static SDL2.SDL;

namespace TankWorld.Game
{
    public class Camera: IUpdate
    {
        static private Camera singleton = null;

        private SDL_Rect subScreen;

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

        public int SubScreenX { get => subScreen.x;}
        public int SubScreenY { get => subScreen.y; }
        public int SubScreenW { get => subScreen.w; }
        public int SubScreenH { get => subScreen.h; }

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
            mapCoord.x = screenCoord.x + position.x - subScreen.w / 2;
            mapCoord.y = screenCoord.y + position.y - subScreen.h / 2;

            return mapCoord;
        }

        public Coordinate ConvertMapToScreenCoordinate(Coordinate mapCoord)
        {
            Coordinate screenCoord;
            screenCoord.x = mapCoord.x - position.x + subScreen.w / 2;
            screenCoord.y = mapCoord.y - position.y + subScreen.h / 2;

            return screenCoord;
        }

        public bool IsInsideCamera(Coordinate position, int width, int height)
        {

            Coordinate drawPosition = ConvertMapToScreenCoordinate(position);
            return  (drawPosition.x >= 0 - width)
                            && (drawPosition.x <= subScreen.w + width)
                            && (drawPosition.y >= 0 - height)
                            && (drawPosition.y <= subScreen.h + height);
        }

        public void SetSubScreenDimensions(int x, int y, int width, int heigth)
        {
            subScreen.x = x;
            subScreen.y = y;
            subScreen.w = width;
            subScreen.h = heigth;
        }
    }
}
