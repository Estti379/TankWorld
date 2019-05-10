using System;
using System.Collections.Generic;
using static SDL2.SDL;

namespace TankWorld.src.ressources.Models
{
    class MenuTextModel : EntityModel
    {
        private string name;
        private string status;
        //Constructors
        public MenuTextModel(string key, string menuText):base()
        {
            name = key;
            status = "Inactive";
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
                menuTextSprite = new Sprite(key + "Active", TextGenerator.pixel_millenium, menuText, color);

                menuTextSprite.Pos.x = 0;
                menuTextSprite.Pos.y = 0;
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
                menuTextSprite = new Sprite(key + "Inactive", TextGenerator.pixel_millenium, menuText, color);

                menuTextSprite.Pos.x = 0;
                menuTextSprite.Pos.y = 0;
                AddSprite("Inactive", menuTextSprite);
            }
        }

        //Accessors


        //Methods
        public override void Render()
        {
            AllSprites[status].Render();                       
        }

        public void SetPosition(int x, int y, int place)
        {
            AllSprites["Active"].Pos.x = x;
            AllSprites["Active"].Pos.y = y + AllSprites["Active"].Pos.h * (1+place);

            AllSprites["Inactive"].Pos.x = x;
            AllSprites["Inactive"].Pos.y = y + AllSprites["Inactive"].Pos.h * (1 + place);
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
