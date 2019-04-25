using System;
using System.Runtime.InteropServices;
using static SDL2.SDL;
using static SDL2.SDL.SDL_bool;

namespace Pong.src.ressources
{
    public class SpriteEntity
    {
        private IntPtr textureSprite;
        private SDL_Rect position;
        private SDL_Rect subDrawRect;


        //Constructors
        public SpriteEntity(IntPtr render, string imagePath, byte r, byte g, byte b)
        {
            textureSprite = ImageTotexture(render, imagePath, r, g, b);
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
        public IntPtr Texture
        {
            get{ return textureSprite; }
        }

        public ref SDL_Rect Pos
        {
            get { return ref position; }
        }
        public ref SDL_Rect SubRect
        {
            get { return ref subDrawRect; }
        }

        //Methods
        static public IntPtr ImageTotexture(IntPtr render, string imagePath, byte r, byte g, byte b)
        {
            IntPtr sprite = IntPtr.Zero;
            sprite = SDL_LoadBMP(imagePath);
            if (sprite == IntPtr.Zero)
            {
                Console.Write("Unable to load image: ball.bmp! SDL Error:" + SDL_GetError() + " \n");
                Console.Write(imagePath + " \n");
                return IntPtr.Zero;
            }
            //Set Colorkey
            var format = ( (SDL_Surface)Marshal.PtrToStructure(sprite, typeof(SDL_Surface)) ).format;
            SDL_SetColorKey(sprite, 1, SDL_MapRGB(format, r, g, b));

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
