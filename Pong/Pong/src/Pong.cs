using System;
using System.Runtime.InteropServices;
using System.IO;
using SDL_test.src;
using static SDL2.SDL;
using static SDL2.SDL.SDL_RendererFlags;

namespace SDL_test
{
    class Pong
    {
        static void Main(string[] args)
        {
            // Initialize All
            const double MS_PER_UPDATE = 16; //Milliseconds between each game Update

            //Initialize SDL
            bool done = false;
            var window = IntPtr.Zero;
            IntPtr render = IntPtr.Zero;
            Initializer.initialize(ref done, ref window, ref render);

            //Load Sprites and create textures of them
            IntPtr sprite = IntPtr.Zero;
            sprite = SDL_LoadBMP("images/ball.bmp");
            if (sprite == IntPtr.Zero)
            {
                Console.Write("Unable to load image: ball.bmp! SDL Error:" + SDL_GetError() + " \n");
                Console.Write(Directory.GetCurrentDirectory() + " \n");
            }
            IntPtr ball = SDL_CreateTextureFromSurface(render, sprite);
            SDL_FreeSurface(sprite);
            if (ball == IntPtr.Zero)
            {
                Console.Write("Unable to create texture! SDL Error:" + SDL_GetError() + " \n");
            }
            SDL_Rect ballPosition;
            ballPosition.x = 0;
            ballPosition.y = 0;
            ballPosition.w = 40;
            ballPosition.h = 40;


            // END Innitialize all

            // START MainGameLoop
            double current = 0.0;
            double elapsed = 0.0;
            double previous = SDL_GetPerformanceCounter();
            double lag = 0.0;
            while (!done)
            {
                current = SDL_GetPerformanceCounter();
                elapsed = (current - previous) * 1000 / (double)SDL_GetPerformanceFrequency();
                previous = current;

                // ProcessInput
                ProcessInput.processInput(ref done);
                // END ProcessInput

                lag += elapsed;
                while (lag >= MS_PER_UPDATE)
                {
                    // GAMEUPDATE
                    if (ballPosition.x < 200)
                    {
                        ballPosition.x += 1;
                        ballPosition.y += 1;
                    }
                    //END GameUpdate
                    lag -= MS_PER_UPDATE;
                }

                // RENDERING
                //Fill the surface black
                SDL_RenderClear(render);

                //Apply the image
                SDL_RenderCopy(render, ball, IntPtr.Zero, ref ballPosition);

                //Render everything
                SDL_RenderPresent(render);


                // END Rendering
            }// END MainGameLoop
            
            
            // QuitingProgram Gracefully
            SDL_DestroyTexture(ball);
            SDL_DestroyRenderer(render);
            SDL_DestroyWindow(window);
            SDL_Quit();
        }
    }
}
