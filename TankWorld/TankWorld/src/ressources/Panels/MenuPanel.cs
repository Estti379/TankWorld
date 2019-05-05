

using System;
using System.Collections.Generic;

namespace TankWorld.src.ressources.Panels
{
    class MenuPanel: Panel
    {
        List<MenuItem> items;
        
        //Constructors
        public MenuPanel()
        {
            items = new List<MenuItem>();
            items.Add(new StartGameMenuItem("StartGame") );
        }

        //Accessors


        //Methods

        public override void Render()
        {
            foreach(MenuItem entry in items)
            {
                entry.Render();
            }
        }

        public override void Update()
        {
            foreach (MenuItem entry in items)
            {
                entry.Update();
            }
        }
    }
}
