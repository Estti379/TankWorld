using Pong.src.ressources;
using System.Collections.Generic;

namespace Pong.src
{
    static public class Update
    {
        static public void StartUpdate(List<Panel> scene)
        {
            //Update all panels

            foreach(Panel item in scene)
            {
                item.UpdateAll();
            }
           

        }
    }
}
