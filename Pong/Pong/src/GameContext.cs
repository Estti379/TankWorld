using System;
using System.IO;
using static SDL2.SDL;
using static SDL2.SDL.SDL_RendererFlags;

namespace SDL_test.src
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
            if ( initialize() )
            {
                start();
            }
            
        }

        // used to allow the creation of only one Instance of this class
        public static GameContext instance
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
        private bool initialize()
        {
            if (SDL_Init(SDL_INIT_VIDEO) < 0)
            {
                Console.Write("SDL could not initialize! SDL_Error: %s\n", SDL_GetError());
                return false;
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
                return false;
            }

            // Create a Render
            render = SDL_CreateRenderer(window, -1, SDL_RENDERER_ACCELERATED);
            if (render == null)
            {
                Console.Write("SDL could not create render! SDL_Error: %s\n", SDL_GetError());
                return false;
            }
            // Sets color to which render will change at each gamerender
            SDL_SetRenderDrawColor(render, 0, 0, 0, 0);
            return true;
        }




        private void start()
        {
            // Initialize All
            const double MS_PER_UPDATE = 16; //Milliseconds between each game Update

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
