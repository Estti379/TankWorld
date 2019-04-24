using System;
using static SDL2.SDL;

namespace Pong.src.ressources
{
    class SpriteEntity
    {
        private IntPtr textureSprite;
        private SDL_Rect position;
        private SDL_Rect subDrawRect;


        //Constructors
        public SpriteEntity(IntPtr render, string imagePath)
        {
            textureSprite = ImageTotexture(render, imagePath);
            position.x = 0;
            position.y = 0;
            position.w = 0;
            position.h = 0;

            subDrawRect.x = 0;
            subDrawRect.y = 0;
            subDrawRect.w = 0;
            subDrawRect.h = 0;
        }

        public SpriteEntity(IntPtr texture)
        {
            textureSprite = texture;
            position.x = 0;
            position.y = 0;
            position.w = 0;
            position.h = 0;

            subDrawRect.x = 0;
            subDrawRect.y = 0;
            subDrawRect.w = 0;
            subDrawRect.h = 0;
        }

        //Accessors
        public IntPtr texture
        {
            get{return textureSprite; }
        }

        public ref SDL_Rect pos
        {
            get { return ref position; }
        }
        public ref SDL_Rect subRect
        {
            get { return ref subDrawRect; }
        }

        //Methods
        static public IntPtr ImageTotexture(IntPtr render, string imagePath)
        {
            IntPtr sprite = IntPtr.Zero;
            sprite = SDL_LoadBMP(imagePath);
            if (sprite == IntPtr.Zero)
            {
                Console.Write("Unable to load image: ball.bmp! SDL Error:" + SDL_GetError() + " \n");
                Console.Write(imagePath + " \n");
                return IntPtr.Zero;
            }
            IntPtr myTexture = SDL_CreateTextureFromSurface(render, sprite);
            SDL_FreeSurface(sprite);
            if (myTexture == IntPtr.Zero)
            {
                Console.Write("Unable to create texture! SDL Error:" + SDL_GetError() + " \n");
                Console.Write(imagePath + " \n");
                return IntPtr.Zero;
            }

            return myTexture;
        }

    }
}
