using static SDL2.SDL;
using static SDL2.SDL.SDL_EventType;
using static SDL2.SDL.SDL_Keycode;

namespace SDL_test.src
{
    static class ProcessInput
    {
        static public void processInput(ref bool done)
        {
            SDL_Event userEvent;
            //Handle events on queue
            while ( SDL_PollEvent( out userEvent ) != 0 )
            {
                //User requests quit
                switch (userEvent.type)
                {
                    case SDL_QUIT:
                        done = true;
                        break;
                    case SDL_KEYDOWN:
                        checkKeyDown(userEvent, ref done);
                        break;
                }
                                                
            }
        }


        static private void checkKeyDown(SDL_Event userEvent, ref bool done)
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
