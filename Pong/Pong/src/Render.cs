using Pong.src.ressources;
using System;
using static SDL2.SDL;

namespace Pong.src
{
    static public class Render
    {
        static public void StartRender(Panel panel, IntPtr render)
        {
            //Fill the surface black
            SDL_SetRenderDrawColor(render, 0, 0, 0, 0);
            SDL_RenderClear(render);

            //Apply the images of all panels

            panel.RenderAll(render);

            //Render everything
            SDL_RenderPresent(render);


        }
    }
}
