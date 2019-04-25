using System;
using static SDL2.SDL;

namespace Pong.src.ressources.Items
{
    class Paddle : DrawnEntity
    {

        //Constructors
        public Paddle(string key, IntPtr render) : base()
        {
            SpriteEntity paddleSprite;
            if (!SpriteEntity.TextureExists("paddle"))
            {
                paddleSprite = new SpriteEntity("paddle", render, "images/paddle.bmp", 0, 255, 0);
            }
            else
            {
                paddleSprite = new SpriteEntity("paddle");
            }
            paddleSprite.Pos.h = 10;
            paddleSprite.Pos.w = 2;
            paddleSprite.SubRect.h = 100;
            paddleSprite.SubRect.w = 20;
            AddSprite(key, paddleSprite);
        }

        //Accessors


        //Methods
        override public void EntityRender(IntPtr render)
        {
            foreach (SpriteEntity current in AllSprites.Values)
            {
                SDL_RenderCopy(render, current.Texture, ref current.SubRect, ref current.Pos);
            }
        }
    }
}
