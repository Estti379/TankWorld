using Pong.src.ressources.Panels;
using System;
using static SDL2.SDL;

namespace Pong.src.ressources.Items
{
    public class Paddle : DrawnEntity, IUpdateEntity
    {
        PaddleState paddleState = PaddleState.STOP;

        //Constructors
        public Paddle(string key, IntPtr render, int offset) : base()
        {
            SpriteEntity paddleSprite;
            if (!SpriteEntity.TextureExists("paddle"))
            {
                paddleSprite = new SpriteEntity("paddle", render, "assets/images/paddle.bmp", 0, 255, 0);
            }
            else
            {
                paddleSprite = new SpriteEntity("paddle");
            }
            paddleSprite.Pos.h = 100;
            paddleSprite.Pos.w = 2;
            paddleSprite.SubRect.h = 100;
            paddleSprite.SubRect.w = 20;
            paddleSprite.Pos.x = offset;
            paddleSprite.Pos.y = GameConstants.PONG_TABLE_Y / 2 - paddleSprite.Pos.h/2;
            AddSprite("paddle", paddleSprite);
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

        public void Stop(PaddleState oldState)
        {
            if(oldState == paddleState)
            {
                paddleState = PaddleState.STOP;
            }
        }

        public void StartMoving(PaddleState newState)
        {
            paddleState = newState;
        }

        public void EntityUpdate(Panel panel)
        {

            switch (paddleState)
            {
                case PaddleState.STOP:
                    /* empty */
                    break;
                case PaddleState.UP:
                    if (AllSprites["paddle"].Pos.y >= 0 )
                    {
                        AllSprites["paddle"].Pos.y -= 1;
                    }
                    break;
                case PaddleState.DOWN:
                    if (AllSprites["paddle"].Pos.y <= GameConstants.PONG_TABLE_Y - AllSprites["paddle"].Pos.h)
                    {
                        AllSprites["paddle"].Pos.y += 1;
                    }
                    break;
            }
        }
    }
}
