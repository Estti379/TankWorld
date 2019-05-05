using TankWorld.src.ressources;
using System;
using System.Collections.Generic;
using static SDL2.SDL;

namespace TankWorld.src
{
    static public class Render
    {
        static public void StartRender(Scene scene, IntPtr renderer)
        {
            //Fill the surface black
            SDL_SetRenderDrawColor(renderer, 0, 0, 0, 0);
            SDL_RenderClear(renderer);

            //Apply the images of all panels

            scene.Render();
            

            //Render everything
            SDL_RenderPresent(renderer);


        }
    }
}
