using System;
using System.Collections.Generic;
using TankWorld.Engine;
using static SDL2.SDL;

namespace TankWorld.Game.Models
{
    class MenuTextModel : EntityModel
    {
        private string name;
        private string status;
        private int posX;
        private int posY;

        //Constructors
        public MenuTextModel(string key, string menuText):base()
        {
            name = key;
            status = "Inactive";
            posX = 0;
            posY = 0;
            SDL_Color color = new SDL_Color
            {
                r = 255,
                g = 215,
                b = 0,
                a = 0
            };

            Sprite menuTextSprite;
            if (!Sprite.TextureExists(key + "Active"))
            {
                menuTextSprite = new Sprite(key + "Active", TextGenerator.pixel_millenium_big, menuText, color);

                menuTextSprite.Pos.x = posX;
                menuTextSprite.Pos.y = posY;
                AddSprite("Active", menuTextSprite);
            }

            color = new SDL_Color
            {
                r = 255,
                g = 255,
                b = 255,
                a = 0
            };

            if (!Sprite.TextureExists(key + "Inactive"))
            {
                menuTextSprite = new Sprite(key + "Inactive", TextGenerator.pixel_millenium_big, menuText, color);

                menuTextSprite.Pos.x = posX;
                menuTextSprite.Pos.y = posY;
                AddSprite("Inactive", menuTextSprite);
            }
        }

        //Accessors


        //Methods
        public override void Render(RenderLayer layer)
        {
            AllSprites[status].RenderAtPosition(posX, posY);
        }

        public void SetPosition(int x, int y, int place)
        {
            posX = x;
            posY = y + AllSprites["Active"].Pos.h * (1 + place);

            posX = x;
            posY = y + AllSprites["Inactive"].Pos.h * (1 + place);
        }

        public void ChangeStatus()
        {
            if ( status.Equals("Active"))
            {
                status = "Inactive";
            } else
            {
                status = "Active";
            }
        }
    }
}
