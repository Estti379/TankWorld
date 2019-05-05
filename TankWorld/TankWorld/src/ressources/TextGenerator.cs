using System;
using static SDL2.SDL_ttf;
using static SDL2.SDL;
using System.Runtime.InteropServices;


namespace TankWorld.src.ressources
{
    public class TextGenerator
    {
        static public IntPtr pixel_millenium = IntPtr.Zero;
        //TODO: Make list of fonts

        //Constructors
        public TextGenerator()
        {
            pixel_millenium = TTF_OpenFont("assets/fonts/Pixel Millennium.ttf", 96);
            if (pixel_millenium == IntPtr.Zero)
                {
                Console.Write("Failed to load Pixel Millenium font! SDL_ttf Error: "+ SDL_GetError() + "\n");
                }
        }

        //Accessors

        //Methods
    }
}
