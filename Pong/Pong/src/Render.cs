using Pong.src.ressources;
using System;
using System.Collections.Generic;
using static SDL2.SDL;

namespace Pong.src
{
    static public class Render
    {
        static public void StartRender(List<Panel> scene, IntPtr render)
        {
            //Fill the surface black
            SDL_SetRenderDrawColor(render, 0, 0, 0, 0);
            SDL_RenderClear(render);

            //Apply the images of all panels

            foreach (Panel item in scene)
            {
                item.RenderAll(render);
            }

            //Render everything
            SDL_RenderPresent(render);


        }
    }
}
