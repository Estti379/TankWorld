using System;
using static SDL2.SDL_ttf;
using static SDL2.SDL;


namespace TankWorld.Engine
{
    public class TextGenerator
    {
        static public IntPtr pixel_millenium_big = IntPtr.Zero;
        static public IntPtr pixel_millenium_medium = IntPtr.Zero;
        static public IntPtr pixel_millenium_small = IntPtr.Zero;
        //TODO: Make list of fonts

        //Constructors
        public TextGenerator()
        {
            pixel_millenium_big = TTF_OpenFont("assets/fonts/Pixel Millennium.ttf", 96);
            if (pixel_millenium_big == IntPtr.Zero)
            {
                Console.Write("Failed to load Pixel Millenium font! SDL_ttf Error: "+ SDL_GetError() + "\n");
            }
            pixel_millenium_small = TTF_OpenFont("assets/fonts/Pixel Millennium.ttf", 24);
            if (pixel_millenium_small == IntPtr.Zero)
            {
                Console.Write("Failed to load Pixel Millenium font! SDL_ttf Error: " + SDL_GetError() + "\n");
            }
            pixel_millenium_medium = TTF_OpenFont("assets/fonts/Pixel Millennium.ttf", 48);
            if (pixel_millenium_medium == IntPtr.Zero)
            {
                Console.Write("Failed to load Pixel Millenium font! SDL_ttf Error: " + SDL_GetError() + "\n");
            }
        }

        //Accessors

        //Methods
    }
}
