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
            InputStruct input;
            while ( SDL_PollEvent( out SDL_Event userEvent) != 0 )
            {
                input.inputEvent = UNDEFINED_INPUT;
                input.x = 0;
                input.y=0; 
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
                    case SDL_MOUSEMOTION:
                        input.inputEvent = MOUSE_MOTION;
                        input.x = userEvent.motion.x;
                        input.y = userEvent.motion.y;
                        break;
                    case SDL_MOUSEBUTTONUP:
                        input = CheckMouseUp(userEvent);
                        break;
                    case SDL_MOUSEBUTTONDOWN:
                        input = CheckMouseDown(userEvent);
                        break;
                }
                game.HandleInput(input);                             
            }
        }


        static private InputStruct CheckKeyDown(SDL_Event userEvent)
        {
            InputStruct input = new InputStruct { inputEvent = UNDEFINED_INPUT, x = 0, y = 0 };
            switch (userEvent.key.keysym.sym)
            {   
                case SDLK_ESCAPE:
                    input.inputEvent = PRESS_ESCAPE;
                    break;
                case SDLK_DOWN:
                    input.inputEvent = PRESS_DOWN;
                    break;
                case SDLK_UP:
                    input.inputEvent = PRESS_UP;
                    break;
                case SDLK_w:
                    input.inputEvent = PRESS_W;
                    break;
                case SDLK_s:
                    input.inputEvent = PRESS_S;
                    break;
                case SDLK_a:
                    input.inputEvent = PRESS_A;
                    break;
                case SDLK_d:
                    input.inputEvent = PRESS_D;
                    break;
                case SDLK_SPACE:
                    input.inputEvent = PRESS_SPACE;
                    break;
            }
            return input;
        }

        static private InputStruct CheckKeyUp(SDL_Event userEvent)
        {
            InputStruct input = new InputStruct { inputEvent = UNDEFINED_INPUT, x = 0, y = 0 };
            switch (userEvent.key.keysym.sym)
            {
                case SDLK_ESCAPE:
                    input.inputEvent = RELEASE_ESCAPE;
                    break;
                case SDLK_DOWN:
                    input.inputEvent = RELEASE_DOWN;
                    break;
                case SDLK_UP:
                    input.inputEvent = RELEASE_UP;
                    break;
                case SDLK_w:
                    input.inputEvent = RELEASE_W;
                    break;
                case SDLK_s:
                    input.inputEvent = RELEASE_S;
                    break;
                case SDLK_a:
                    input.inputEvent = RELEASE_A;
                    break;
                case SDLK_d:
                    input.inputEvent = RELEASE_D;
                    break;
                case SDLK_SPACE:
                    input.inputEvent = RELEASE_SPACE;
                    break;
            }
            return input;
        }

        static private InputStruct CheckMouseUp(SDL_Event userEvent)
        {
            InputStruct input = new InputStruct{ inputEvent = UNDEFINED_INPUT, x = 0, y = 0 };
            switch ((uint)userEvent.button.button)
            {
                case SDL_BUTTON_LEFT:
                    input.inputEvent = RELEASE_LEFT_BUTTON;
                    input.x = userEvent.button.x;
                    input.y = userEvent.button.y;
                    break;
                case SDL_BUTTON_RIGHT:
                    input.inputEvent = RELEASE_RIGHT_BUTTON;
                    input.x = userEvent.button.x;
                    input.y = userEvent.button.y;
                    break;

            }
            return input;
        }

        static private InputStruct CheckMouseDown(SDL_Event userEvent)
        {
            InputStruct input = new InputStruct { inputEvent = UNDEFINED_INPUT, x = 0, y = 0 };
            switch ((uint)userEvent.button.button)
            {
                case SDL_BUTTON_LEFT:
                    input.inputEvent = PRESS_LEFT_BUTTON;
                    input.x = userEvent.button.x;
                    input.y = userEvent.button.y;
                    break;
                case SDL_BUTTON_RIGHT:
                    input.inputEvent = PRESS_RIGHT_BUTTON;
                    input.x = userEvent.button.x;
                    input.y = userEvent.button.y;
                    break;

            }
            return input;
        }

    }
}
