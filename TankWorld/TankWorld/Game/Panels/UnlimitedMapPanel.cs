

using System;
using TankWorld.Engine;

namespace TankWorld.Game.Panels
{
    public class UnlimitedMapPanel: MapPanel
    {
        private Sprite background;
        private Coordinate leftToptexturePosition;

        //Constructor
        public UnlimitedMapPanel(PlayScene parent): base(parent)
        {
            background = new Sprite("SandBackground", "assets/images/sand-dune-seamless-texture.jpg", 0, 254, 0);
            leftToptexturePosition.x = 0;
            leftToptexturePosition.y = 0;
        }

        //Accessors


        //Methods
        public override void Update()
        {
            UpdateLeftTop();
        }
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
                    } while (coords.x < CurrentCamera.SubScreenW);
                    coords.y += background.Pos.h;
                    coords.x = leftToptexturePosition.x;
                } while (coords.y < CurrentCamera.SubScreenH);

            }

        }
        private void UpdateLeftTop()
        {
            leftToptexturePosition.x += -(CurrentCamera.Position.x - CurrentCamera.OldPosition.x);
            leftToptexturePosition.y += -(CurrentCamera.Position.y - CurrentCamera.OldPosition.y);

            //"While" instead of "if" just in case camera moves more than the length of a background image!
            while (leftToptexturePosition.x + background.Pos.w < 0)
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
