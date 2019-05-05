﻿using System;
using System.Collections.Generic;
using static SDL2.SDL;

namespace TankWorld.src.ressources.Models
{
    class MenuTextModel : EntityModel
    {
        private string name;
        private SDL_Color color;
        //Constructors
        public MenuTextModel(string key, string menuText):base()
        {
            color = new SDL_Color
            {
                r = 255,
                g = 255,
                b = 255,
                a = 0
            };

            Sprite menuTextSprite;
            if (!Sprite.TextureExists(key))
            {
                menuTextSprite = new Sprite(key, TextGenerator.pixel_millenium, menuText, color);
            }
            else
            {
                // If it already exists, simply use existing one
                menuTextSprite = AllSprites[key];
            }

            name = key;

            menuTextSprite.Pos.x = 0;
            menuTextSprite.Pos.y = 0;
            AddSprite(key, menuTextSprite);
        }

        //Accessors


        //Methods
        public override void Render()
        {
            AllSprites[name].Render();
                       
        }

        public void SetPosition(int x, int y, int place)
        {
            AllSprites[name].Pos.x = x;
            AllSprites[name].Pos.y = y + AllSprites[name].Pos.h * (1+place);
        }
    }
}
