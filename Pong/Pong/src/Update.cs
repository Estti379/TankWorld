using Pong.src.ressources;
using System;

namespace Pong.src
{
    static public class Update
    {
        static public void StartUpdate(Panel panel)
        {
            //Update all panels

            panel.UpdateAll(panel);

        }
    }
}
