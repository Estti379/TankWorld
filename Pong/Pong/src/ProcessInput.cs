using static SDL2.SDL;
using static SDL2.SDL.SDL_EventType;
using static SDL2.SDL.SDL_Keycode;

namespace Pong.src
{
    static class ProcessInput
    {
        static public void ReadInput(ref bool done)
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
                        CheckKeyDown(userEvent, ref done);
                        break;
                }
                                                
            }
        }


        static private void CheckKeyDown(SDL_Event userEvent, ref bool done)
        {
            switch (userEvent.key.keysym.sym)
            {
                case SDLK_ESCAPE:
                    done = true;
                    break;
            }
        }

    }
}
