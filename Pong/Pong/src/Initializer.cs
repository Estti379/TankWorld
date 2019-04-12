using System;
using static SDL2.SDL;
using static SDL2.SDL.SDL_RendererFlags;

namespace SDL_test.src
{
    static class Initializer
    {
        static public void initialize(ref bool done, ref IntPtr window, ref IntPtr render )
        {
            if (SDL_Init(SDL_INIT_VIDEO) < 0)
            {
                Console.Write("SDL could not initialize! SDL_Error: %s\n", SDL_GetError());
                done = true;
            }

            //Initialize window
            window = IntPtr.Zero;
            window = SDL_CreateWindow(".NET Core SDL2-CS Tutorial",
                SDL_WINDOWPOS_CENTERED,
                SDL_WINDOWPOS_CENTERED,
                1280,
                720,
                SDL_WindowFlags.SDL_WINDOW_SHOWN
            );

            if (window == IntPtr.Zero)
            {
                Console.Write("SDL could not initialize! SDL_Error: %s\n", SDL_GetError());
                done = true;
            }

            // Create a Render
            render = SDL_CreateRenderer(window, -1, SDL_RENDERER_ACCELERATED);
            if (render == null)
            {
                Console.Write("SDL could not create render! SDL_Error: %s\n", SDL_GetError());
                done = true;
            }
            SDL_SetRenderDrawColor(render, 0, 0, 0, 0);
        }
    }
}
