using System;
using static SDL2.SDL_ttf;
using static SDL2.SDL;
using System.Runtime.InteropServices;


namespace Pong.src.ressources
{
    public class TextGenerator
    {
        static public IntPtr lazyFont = IntPtr.Zero;
        //TODO: Make list of fonts

        //Constructors
        public TextGenerator()
        {
            lazyFont = TTF_OpenFont("assets/fonts/Pixel Millennium.ttf", 96);
            if (lazyFont == IntPtr.Zero)
                {
                Console.Write("Failed to load Pixel Millenium font! SDL_ttf Error: "+ SDL_GetError() + "\n");
                }
        }

        //Accessors

        //Methods

        static public TextureStruct GenerateTexture(IntPtr render, IntPtr font, string text, SDL_Color color)
        {
            TextureStruct finalTexture = new TextureStruct(IntPtr.Zero, 0, 0);
            IntPtr textSurface = TTF_RenderText_Solid(font, text, color);
            IntPtr myTexture = IntPtr.Zero;
            if (textSurface == IntPtr.Zero)
            {
                Console.Write("Unable to render text surface! SDL_ttf Error: %s\n", SDL_GetError());
            }
            else
            {
                //Create texture from surface pixels
                myTexture = SDL_CreateTextureFromSurface(render, textSurface);
                if (myTexture == IntPtr.Zero)
                {
                    Console.Write("Unable to create texture from rendered text! SDL Error: %s\n", SDL_GetError());
                }
                //Create texture struct
                finalTexture.h = ((SDL_Surface)Marshal.PtrToStructure(textSurface, typeof(SDL_Surface))).h;
                finalTexture.w = ((SDL_Surface)Marshal.PtrToStructure(textSurface, typeof(SDL_Surface))).w;
                finalTexture.texture = myTexture;
                //Get rid of old surface
                SDL_FreeSurface(textSurface);
            }
            return finalTexture;
        }
    }
}
