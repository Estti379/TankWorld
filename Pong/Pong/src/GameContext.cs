﻿using Pong.src.ressources;
using System;
using System.IO;
using static SDL2.SDL;
using static SDL2.SDL.SDL_RendererFlags;

namespace Pong.src
{
    public class GameContext
    {
        static private GameContext singleton = null;

        private bool done = false; // Boolean for the main game loop
        private IntPtr window = IntPtr.Zero;
        private IntPtr render = IntPtr.Zero;



        private GameContext()
        {
            // initialize then start the game
            if ( Initialize() )
            {
                Start();
            }
            
        }

        // used to allow the creation of only one Instance of this class
        public static GameContext Instance
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new GameContext();
                }
                return singleton;
            }
        }

        /** Used to initialize SDL.
         * 
         * returns: True if the initialization was sucessfull, False otherwise.
         */
        private bool Initialize()
        {
            if (SDL_Init(SDL_INIT_VIDEO) < 0)
            {
                Console.Write("SDL could not initialize! SDL_Error: %s\n", SDL_GetError());
                return false;
            }

            //Initialize window
            window = IntPtr.Zero;
            window = SDL_CreateWindow("Pong!",
                SDL_WINDOWPOS_CENTERED,
                SDL_WINDOWPOS_CENTERED,
                1280,
                720,
                SDL_WindowFlags.SDL_WINDOW_SHOWN
            );

            if (window == IntPtr.Zero)
            {
                Console.Write("SDL could not initialize! SDL_Error: %s\n", SDL_GetError());
                return false;
            }

            // Create a Render
            render = SDL_CreateRenderer(window, -1, SDL_RENDERER_ACCELERATED);
            if (render == null)
            {
                Console.Write("SDL could not create render! SDL_Error: %s\n", SDL_GetError());
                return false;
            }
            return true;
        }




        private void Start()
        {
            // Initialize All
            const double MS_PER_UPDATE = 16; //Milliseconds between each game Update

            //Load Sprites and create textures of them
            SpriteEntity ball = new SpriteEntity("ball1", render, "images/ball.bmp", 0, 255, 0);

            ball.Pos.x = 0;
            ball.Pos.y = 0;
            ball.Pos.w = 100;
            ball.Pos.h = 100;

            ball.SubRect.x = 0;
            ball.SubRect.y = 0;
            ball.SubRect.w = 100;
            ball.SubRect.h = 100;

            SpriteEntity ball2 = new SpriteEntity("ball2", ball.Texture);

            ball2.Pos.x = 100;
            ball2.Pos.y = 120;
            ball2.Pos.w = 40;
            ball2.Pos.h = 40;

            ball2.SubRect.x = 0;
            ball2.SubRect.y = 0;
            ball2.SubRect.w = 100;
            ball2.SubRect.h = 100;

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
                ProcessInput.ReadInput(ref done);
                // END ProcessInput

                lag += elapsed;
                while (lag >= MS_PER_UPDATE)
                {
                    // GAMEUPDATE
                    if (ball.Pos.x < 200)
                    {
                        ball.Pos.x += 1;
                        ball.Pos.y += 1;
                    }
                    if (ball2.Pos.x > 0 && ball2.Pos.y > 0)
                    {
                        ball2.Pos.x -= 1;
                        ball2.Pos.y -= 1;
                    }
                    //END GameUpdate
                    lag -= MS_PER_UPDATE;
                }

                // RENDERING
                //Fill the surface black
                SDL_SetRenderDrawColor(render, 0, 0, 0, 0);
                SDL_RenderClear(render);

                //Apply the images
                SDL_RenderCopy(render, ball.Texture, ref ball.SubRect, ref ball.Pos);
                SDL_RenderCopy(render, ball2.Texture, ref ball2.SubRect, ref ball2.Pos);

                //Render everything
                SDL_RenderPresent(render);


                // END Rendering
            }// END MainGameLoop


            // QuitingProgram Gracefully
            SDL_DestroyTexture(ball.Texture);
            SDL_DestroyTexture(ball2.Texture);
            SDL_DestroyRenderer(render);
            SDL_DestroyWindow(window);
            SDL_Quit();
        }


    }
}
