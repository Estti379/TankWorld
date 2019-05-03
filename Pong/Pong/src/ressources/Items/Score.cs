using System;
using System.Runtime.InteropServices;
using static SDL2.SDL;

namespace Pong.src.ressources.Items
{
    public class Score : DrawnEntity
    {
        private byte points = 0;
        private string name;
        private SDL_Color color;

        //Constructors
        public Score(string key, IntPtr render) : base()
        {
            color = new SDL_Color
            {
                r = 255,
                g = 255,
                b = 255,
                a = 0
            };

            SpriteEntity scoreSprite;
            TextureStruct textTexture = TextGenerator.GenerateTexture(render, TextGenerator.lazyFont, "void", color); 
            if (!SpriteEntity.TextureExists(key))
            {
                scoreSprite = new SpriteEntity("scorePlayer1", textTexture);
            }
            else
            {
                // If it already exists, simply use existing one
                scoreSprite = AllSprites["scorePlayer1"];
            }

            name = key;

            AddSprite(key, scoreSprite);
            ResetPoint(render);
        }
        //Accessors

        //Methods
        public override void EntityRender(IntPtr render)
        {
            SDL_RenderCopy(render, AllSprites[name].Texture,
                                ref AllSprites[name].SubRect,
                                ref AllSprites[name].Pos);
        }

        public void AddPoint(IntPtr render)
        {
            points += 1;
            UpdateTexture(render);
        }
        public void ResetPoint(IntPtr render)
        {
            points = 0;
            UpdateTexture(render);
        }

        private void UpdateTexture(IntPtr render)
        {
            string pointsSTR = "" + points;
            TextureStruct textTexture = TextGenerator.GenerateTexture(render, TextGenerator.lazyFont, pointsSTR, color);
            
            AllSprites[name].ReplaceTexture(textTexture);

            AllSprites[name].Pos.x = GameConstants.SCORE_OFFSET;
            AllSprites[name].Pos.y = GameConstants.WINDOWS_Y - GameConstants.SCORE_OFFSET;
        }
    }

}
