using System;
using TankWorld.Engine;

namespace TankWorld.Game.Panels
{
    class MapPanel : Panel
    {

        private Sprite background;
        private Coordinate leftToptexturePosition;
        private Camera camera;

        //Constructors
        public MapPanel()
        {
            background = new Sprite("SandBackground", "assets/images/sand-dune-seamless-texture.jpg", 0, 254, 0);
            leftToptexturePosition.x = 0;
            leftToptexturePosition.y = 0;
            this.camera = Camera.Instance;
        }

        //Accessors


        //Methods



        public override void Render()
        {
            Coordinate coords = leftToptexturePosition;

            do
            {
                background.Pos.y = (int)Math.Round(coords.y);
                do
                {
                    background.Pos.x = (int)Math.Round(coords.x);
                    background.Render();
                    coords.x += background.Pos.w;
                } while (coords.x < GameConstants.WINDOWS_X);
                coords.y += background.Pos.h;
                coords.x = leftToptexturePosition.x;
            } while (coords.y < GameConstants.WINDOWS_Y);

        }

        public override void Update()
        {
            UpdateLeftTop();
        }

        private void UpdateLeftTop()
        {
            leftToptexturePosition.x += - (camera.Position.x - camera.OldPosition.x);
            leftToptexturePosition.y += - (camera.Position.y - camera.OldPosition.y);

            if( leftToptexturePosition.x + background.Pos.w < 0)
            {
                leftToptexturePosition.x += background.Pos.w;
            }
            else if (leftToptexturePosition.x > 0)
            {
                leftToptexturePosition.x -= background.Pos.w;
            }

            if (leftToptexturePosition.y + background.Pos.h < 0)
            {
                leftToptexturePosition.y += background.Pos.h;
            }
            else if (leftToptexturePosition.y > 0)
            {
                leftToptexturePosition.y -= background.Pos.h;
            }
        }
    }
}
