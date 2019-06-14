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



        public override void Render(RenderLayer layer)
        {
            if (layer == RenderLayer.BACKGROOUND)
            {
                Coordinate coords = leftToptexturePosition;

                do
                {
                    do
                    {
                        background.RenderAtPosition((int)Math.Round(coords.x), (int)Math.Round(coords.y));
                        coords.x += background.Pos.w;
                    } while (coords.x < camera.SubScreenW);
                    coords.y += background.Pos.h;
                    coords.x = leftToptexturePosition.x;
                } while (coords.y < camera.SubScreenH);
            }

        }

        public override void Update()
        {
            UpdateLeftTop();
        }

        private void UpdateLeftTop()
        {
            leftToptexturePosition.x += - (camera.Position.x - camera.OldPosition.x);
            leftToptexturePosition.y += - (camera.Position.y - camera.OldPosition.y);

            //"While" instead of "if" just in case camera moves more than the length of a background image!
            while ( leftToptexturePosition.x + background.Pos.w < 0)
            {
                leftToptexturePosition.x += background.Pos.w;
            }
            while (leftToptexturePosition.x > 0)
            {
                leftToptexturePosition.x -= background.Pos.w;
            }

            while (leftToptexturePosition.y + background.Pos.h < 0)
            {
                leftToptexturePosition.y += background.Pos.h;
            }
            while (leftToptexturePosition.y > 0)
            {
                leftToptexturePosition.y -= background.Pos.h;
            }
        }
    }
}
