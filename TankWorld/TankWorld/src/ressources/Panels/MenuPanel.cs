

using System;
using System.Collections.Generic;

namespace TankWorld.src.ressources.Panels
{
    class MenuPanel: Panel
    {
        List<MenuItem> items;
        int activeItemIndex;
        
        //Constructors
        public MenuPanel(List<MenuItem> items)
        {
            this.items = items;
            activeItemIndex = 0;
            UpdateCurrentItem();
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

        public void GoDown()
        {
            UpdateCurrentItem();
            activeItemIndex = (activeItemIndex -1 + items.Count) % items.Count;
            UpdateCurrentItem();
        }

        public void GoUp()
        {
            UpdateCurrentItem();
            activeItemIndex = (activeItemIndex + 1) % items.Count;
            UpdateCurrentItem();
        }
        public void Act()
        {
            Console.WriteLine("MenuAction on "+ activeItemIndex +".");
        }

        private void UpdateCurrentItem()
        {
            items[activeItemIndex].ChangeStatus();
        }
    }
}
