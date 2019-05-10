using System;
using System.Collections.Generic;
using static TankWorld.src.InputEnum;

namespace TankWorld.src.ressources.Panels
{
    class PlayScene: Scene
    {
        private List<Panel> panels;

        bool showMenu;

        //Constructors
        public PlayScene()
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
            List<MenuItem> menuItems = new List<MenuItem>();
            menuItems.Add(new StartGameMenuItem("Continue", "Continue Game"));
            menuItems.Add(new StartGameMenuItem("Back", "Back To Main Menu"));
            menuItems.Add(new StartGameMenuItem("Quit", "Quit Game"));
            MenuPanel menuPanel = new MenuPanel(menuItems);
            menuPanel.SetPosition((GameConstants.WINDOWS_X * 1 / 3), 100);
            panels.Add(menuPanel);
            showMenu = false;



        }

        public override void Exit()
        {
            Sprite.RemoveAll();
        }

        public override Scene HandleInput(InputEnum input)
        {
            Scene nextScene = null;
            MenuPanel menu = panels[0] as MenuPanel;
            if (menu != null)
            {


                switch (input)
                {
                    case PRESS_S:
                        if (showMenu)
                        {
                            menu.GoDown();
                        }
                        else
                        {
                            
                        }
                        break;
                    case PRESS_W:
                        if (showMenu)
                        {
                            menu.GoUp();
                        }
                        else
                        {

                        }
                        break;
                    case PRESS_SPACE:
                        if (showMenu)
                        {
                            nextScene = menu.Act();
                        }
                        else
                        {

                        }
                        
                        break;
                    case PRESS_ESCAPE:
                        showMenu = true;
                        break;
                }
            }

            return nextScene;
        }

        public override void Render()
        {
            for (int i = 0; i < (panels.Count - 1); i++)
            {
                panels[i].Render();
            }
            if (showMenu)
            {
                panels[panels.Count - 1].Render();
            }
        }

        public override void Update()
        {
            if (showMenu)
            {
                panels[panels.Count-1].Update();
            }
            else
            {
                for (int i = 0; i < (panels.Count - 1); i++)
                {
                    panels[i].Update();
                }
            }
        }
    }
}
