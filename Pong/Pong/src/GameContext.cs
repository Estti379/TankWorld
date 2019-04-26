using Pong.src.ressources;
using Pong.src.ressources.Panels;
using System;
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
                GameConstants.WINDOWS_X,
                GameConstants.WINDOWS_Y,
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
            // START Innitializing panel
            Panel pongTablePanel = new PongPanel(render);
            // END innitializing panel

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
                ProcessInput.ReadInput(ref done, pongTablePanel);
                // END ProcessInput

                lag += elapsed;
                while (lag >= GameConstants.MS_PER_UPDATE)
                {
                    // GAMEUPDATE
                    Update.StartUpdate(pongTablePanel);
                    //END GameUpdate
                    lag -= GameConstants.MS_PER_UPDATE;
                }

                // RENDERING
                Render.StartRender(pongTablePanel, render);
                // END Rendering
            }// END MainGameLoop


            // QuitingProgram Gracefully

            //TODO: destroy all textures
            SDL_DestroyRenderer(render);
            SDL_DestroyWindow(window);
            SDL_Quit();
        }


    }
}
