using Pong.src.ressources;
using Pong.src.ressources.Panels;
using System.Collections.Generic;
using static SDL2.SDL;
using static SDL2.SDL.SDL_EventType;
using static SDL2.SDL.SDL_Keycode;

namespace Pong.src
{
    static class ProcessInput
    {
        static public void ReadInput(ref bool done, List<Panel> scene)
        {
            //Handle events on queue
            while ( SDL_PollEvent( out SDL_Event userEvent) != 0 )
            {
                //User requests quit
                switch (userEvent.type)
                {
                    case SDL_QUIT:
                        done = true;
                        break;
                    case SDL_KEYDOWN:
                        CheckKeyDown(userEvent, ref done, scene[0]);
                        break;
                    case SDL_KEYUP:
                        CheckKeyUp(userEvent, ref done, scene[0]);
                        break;
                }
                                                
            }
        }


        static private void CheckKeyDown(SDL_Event userEvent, ref bool done, Panel panel)
        {
            PongPanel table;
            switch (userEvent.key.keysym.sym)
            {   
                case SDLK_ESCAPE:
                    done = true;
                    break;
                case SDLK_DOWN:
                    if(panel is PongPanel)
                    {
                        table = (PongPanel)panel;
                        table.DownButtonDown();
                    }
                    break;
                case SDLK_UP:
                    if (panel is PongPanel)
                    {
                        table = (PongPanel)panel;
                        table.UpButtonDown();
                    }
                    break;
                case SDLK_w:
                    if (panel is PongPanel)
                    {
                        table = (PongPanel)panel;
                        table.WButtonDown();
                    }
                    break;
                case SDLK_s:
                    if (panel is PongPanel)
                    {
                        table = (PongPanel)panel;
                        table.SButtonDown();
                    }
                    break;
            }
        }

        static private void CheckKeyUp(SDL_Event userEvent, ref bool done, Panel panel)
        {
            PongPanel table;
            switch (userEvent.key.keysym.sym)
            {
                case SDLK_DOWN:
                    if (panel is PongPanel)
                    {
                        table = (PongPanel)panel;
                        table.DownButtonUP();
                    }
                    break;
                case SDLK_UP:
                    if (panel is PongPanel)
                    {
                        table = (PongPanel)panel;
                        table.UpButtonUP();
                    }
                    break;
                case SDLK_w:
                    if (panel is PongPanel)
                    {
                        table = (PongPanel)panel;
                        table.WButtonUP();
                    }
                    break;
                case SDLK_s:
                    if (panel is PongPanel)
                    {
                        table = (PongPanel)panel;
                        table.SButtonUP();
                    }
                    break;
            }
        }

    }
}
