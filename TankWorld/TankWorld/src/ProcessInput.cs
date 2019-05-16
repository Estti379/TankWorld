using TankWorld.src.ressources;
//using System.Collections.Generic;
using static SDL2.SDL;
using static SDL2.SDL.SDL_EventType;
using static SDL2.SDL.SDL_Keycode;
using System;
using static TankWorld.src.InputEnum;

namespace TankWorld.src
{
    static class ProcessInput
    {
        static public void ReadInput(ref bool done, GameContext game)
        {
            //Handle events on queue
            InputEnum input;
            while ( SDL_PollEvent( out SDL_Event userEvent) != 0 )
            {
                input = UNDEFINED_INPUT; 
                switch (userEvent.type)
                {
                    //User requests quit
                    case SDL_QUIT:
                        done = true;
                        return;
                        break;
                    case SDL_KEYDOWN:
                        input = CheckKeyDown(userEvent);
                        break;
                    case SDL_KEYUP:
                        input = CheckKeyUp(userEvent);
                        break;
                }
                game.HandleInput(input);                             
            }
        }


        static private InputEnum CheckKeyDown(SDL_Event userEvent)
        {
            InputEnum input = UNDEFINED_INPUT;
            switch (userEvent.key.keysym.sym)
            {   
                case SDLK_ESCAPE:
                    input = PRESS_ESCAPE;
                    break;
                case SDLK_DOWN:
                    input = PRESS_DOWN;
                    break;
                case SDLK_UP:
                    input = PRESS_UP;
                    break;
                case SDLK_w:
                    input = PRESS_W;
                    break;
                case SDLK_s:
                    input = PRESS_S;
                    break;
                case SDLK_a:
                    input = PRESS_A;
                    break;
                case SDLK_d:
                    input = PRESS_D;
                    break;
                case SDLK_SPACE:
                    input = PRESS_SPACE;
                    break;
            }
            return input;
        }

        static private InputEnum CheckKeyUp(SDL_Event userEvent)
        {
            InputEnum input = UNDEFINED_INPUT;
            switch (userEvent.key.keysym.sym)
            {
                case SDLK_ESCAPE:
                    input = RELEASE_ESCAPE;
                    break;
                case SDLK_DOWN:
                    input = RELEASE_DOWN;
                    break;
                case SDLK_UP:
                    input = RELEASE_UP;
                    break;
                case SDLK_w:
                    input = RELEASE_W;
                    break;
                case SDLK_s:
                    input = RELEASE_S;
                    break;
                case SDLK_a:
                    input = RELEASE_A;
                    break;
                case SDLK_d:
                    input = RELEASE_D;
                    break;
                case SDLK_SPACE:
                    input = RELEASE_SPACE;
                    break;
            }
            return input;
        }

    }
}
