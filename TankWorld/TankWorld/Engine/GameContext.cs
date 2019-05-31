using System;
using static SDL2.SDL;
using static SDL2.SDL_ttf;
using static SDL2.SDL.SDL_RendererFlags;
using static SDL2.SDL_image;
using System.Collections.Generic;
using TankWorld.Game.Events;
using TankWorld.Game.Panels;

namespace TankWorld.Engine
{
    public class GameContext: IUpdate, IRender,IObserver
    {
        static private GameContext singleton = null;

        private bool done = false; // Boolean for the main game loop
        private IntPtr window = IntPtr.Zero;
        private IntPtr renderer = IntPtr.Zero;

        private Scene currentScene;
        private List<Event> events;



        private GameContext()
        {
            MainEventBus.Register(this);
            events = new List<Event>();
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
            window = SDL_CreateWindow("Tank World!",
                SDL_WINDOWPOS_CENTERED,
                SDL_WINDOWPOS_CENTERED,
                GameConstants.WINDOWS_X,
                GameConstants.WINDOWS_Y,
                SDL_WindowFlags.SDL_WINDOW_FULLSCREEN
            );

            if (window == IntPtr.Zero)
            {
                Console.Write("SDL could not initialize! SDL_Error: %s\n", SDL_GetError());
                return false;
            }

            // Create a Render
            renderer = SDL_CreateRenderer(window, -1, SDL_RENDERER_ACCELERATED);
            if (renderer == null)
            {
                Console.Write("SDL could not create render! SDL_Error: %s\n", SDL_GetError());
                return false;
            }

            if (TTF_Init() == -1)
            {
                Console.Write("SDL_ttf could not initialize! SDL_ttf Error: %s\n", SDL_GetError());
                return false;
            }

            if (IMG_Init(IMG_InitFlags.IMG_INIT_JPG |IMG_InitFlags.IMG_INIT_PNG) == -1)
            {
                Console.Write("SDL_image could not initialize! SDL_Error: %s\n", SDL_GetError());
                return false;
            }

            new TextGenerator();
            Sprite.Renderer = renderer;
            //TODO: change into Inits

            return true;
        }

        public void ChangeScene(Scene nextScene)
        {
            if(nextScene != null)
            {
                currentScene.Exit();
                nextScene.Enter();
                currentScene = nextScene;
            }
        }

        private void PollEvents()
        {
            List<Event> events = new List<Event>();
            events.AddRange(this.events);
            this.events.Clear();

            SceneStateEvent stateEvent;
            foreach (Event entry in events)
            {
                if ((stateEvent = entry as SceneStateEvent) != null)
                {
                    switch (stateEvent.eventType)
                    {
                        case SceneStateEvent.Type.CHANGE_SCENE:
                            ChangeScene(stateEvent.NewScene);
                            break;
                        case SceneStateEvent.Type.EXIT_GAME:
                            this.done = true;
                            break;
                    }
                }
            }
        }

        public void OnEvent(Event newEvent)
        {
            events.Add(newEvent);
        }

        public void Update()
        {
            PollEvents();
            currentScene.Update();
        }

        public void Render()
        {
            //Fill the surface black
            SDL_SetRenderDrawColor(renderer, 0, 0, 0, 0);
            SDL_RenderClear(renderer);

            //Apply the images of all panels
            currentScene.Render();

            //Render everything
            SDL_RenderPresent(renderer);
        }




        private void Start()
        {
            // START Innitializing startingScene
            currentScene = new MainMenuScene();
            currentScene.Enter();
            // END innitializing startingScene

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
                ProcessInput.ReadInput(ref done, currentScene);
                //TODO: Make this method return input instead of having currentScene as parameter
                // END ProcessInput

                lag += elapsed;
                while (lag >= GameConstants.MS_PER_UPDATE)
                {
                    // GAMEUPDATE
                    Update();
                    //END GameUpdate
                    lag -= GameConstants.MS_PER_UPDATE;
                }

                // RENDERING
                Render();
                // END Rendering
            }// END MainGameLoop


            // QuitingProgram Gracefully

            SDL_DestroyRenderer(renderer);
            SDL_DestroyWindow(window);
            TTF_Quit();
            IMG_Quit();
            SDL_Quit();
        }

    }
}
