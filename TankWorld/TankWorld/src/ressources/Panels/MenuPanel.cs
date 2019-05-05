

using System;
using System.Collections.Generic;

namespace TankWorld.src.ressources.Panels
{
    class MenuPanel: Panel
    {
        List<MenuItem> items;
        
        //Constructors
        public MenuPanel(List<MenuItem> items)
        {
            this.items = items; 
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

        public void SetPosition(int x, int y)
        {
            for(int i = 0; i < items.Count ; i++)
            {
                items[i].SetPosition(x,y,i);
            }
        }
    }
}
