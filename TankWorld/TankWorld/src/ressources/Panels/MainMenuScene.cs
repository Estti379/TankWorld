using System;
using System.Collections.Generic;
using static TankWorld.src.InputEnum;

namespace TankWorld.src.ressources.Panels
{
    public class MainMenuScene: Scene
    {
        private List<Panel> panels;

        //Constructors
        public MainMenuScene()
        {
            panels = new List<Panel>();
        }


        //Accessors
        public List<Panel> Panels
        {
            get { return panels; }
        }

        //Methods
        public override void Enter()
        {
            panels.Add(new MenuPanel() );
        }

        public override void Exit()
        {
            throw new System.NotImplementedException();
        }

        public override Scene HandleInput(InputEnum input)
        {
            Scene nextScene = null;
            switch(input){
                case PRESS_S:

                    break;
            }

            return nextScene;
        }

        public override void Render()
        {
            foreach (Panel entry in panels)
            {
                entry.Render();
            }
        }

        public override void Update()
        {
            foreach(Panel entry in panels)
            {
                entry.Update();
            }
        }

    }
}
