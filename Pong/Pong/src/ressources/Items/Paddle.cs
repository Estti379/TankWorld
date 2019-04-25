using System;
using static SDL2.SDL;

namespace Pong.src.ressources.Items
{
    class Paddle : DrawnEntity
    {

        //Constructors
        public Paddle() : base()
        {
            
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
