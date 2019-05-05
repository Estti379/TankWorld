using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankWorld.src.ressources.Models;

namespace TankWorld.src.ressources.Panels
{
    public class StartGameMenuItem : MenuItem
    {
        EntityModel menuModel;
        //Constructors
        public StartGameMenuItem(string text)
        {
            menuModel = new MenuTextModel("Start", text);
        }

        //Accessors


        //Methods

        public override void Action()
        {
            throw new NotImplementedException();
        }

        public override void Render()
        {
            menuModel.Render();
        }

        public override void Update()
        {
          /* empty */
        }
    }
}
