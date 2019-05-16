using System;
using System.Collections.Generic;
using TankWorld.src.ressources.Models;

namespace TankWorld.src.ressources.Panels
{
    public class StartGameMenuItem : MenuItem
    {
        MenuTextModel menuModel;
        //Constructors
        public StartGameMenuItem(string key, string text)
        {
            menuModel = new MenuTextModel(key, text);
        }

        //Accessors


        //Methods

        public override Scene Action()
        {
            return new PlayScene();
        }

        public override void Render()
        {
            menuModel.Render();
        }

        public override void Update()
        {
          /* empty */
        }

        override public void SetPosition(int x, int y, int place)
        {
            menuModel.SetPosition(x, y, place);
        }

        override public void ChangeStatus()
        {
            menuModel.ChangeStatus();
        }

    }
}
