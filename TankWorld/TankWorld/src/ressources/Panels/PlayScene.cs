using System;
using System.Collections.Generic;
using static TankWorld.src.InputEnum;

namespace TankWorld.src.ressources.Panels
{
    class PlayScene: Scene
    {
        private GameViewPanel gameView;
        private MenuPanel menu;

        bool showMenu;

        //Constructors
        public PlayScene()
        {

        }


        //Accessors

        //Methods
        public override void Enter()
        {
            //Create mainGamePanel
            gameView = new GameViewPanel();

            //Create Menu Items, add MenuPanel and initialize it
            List<MenuItem> menuItems = new List<MenuItem>();
            menuItems.Add(new StartGameMenuItem("Continue", "Continue Game"));
            menuItems.Add(new StartGameMenuItem("Back", "Back To Main Menu"));
            menuItems.Add(new StartGameMenuItem("Quit", "Quit Game"));
            menu = new MenuPanel(menuItems);
            menu.SetPosition((GameConstants.WINDOWS_X * 1 / 3), 100);
            showMenu = false;

        }

        public override void Exit()
        {
            Sprite.RemoveAll();
        }

        public override Scene HandleInput(InputEnum input)
        {
            Scene nextScene = null;

            if(input == PRESS_ESCAPE)
            {
                showMenu = !showMenu;
            }
            if (showMenu)
            {
                switch (input)
                {
                    case PRESS_S:
                        menu.GoDown();
                        break;
                    case PRESS_W:
                        menu.GoUp();
                        break;
                    case PRESS_SPACE:
                        nextScene = menu.Act();
                        break;
                }
            }
            else
            {
                gameView.HandleInput(input);
            }
            

            return nextScene;
        }

        public override void Render()
        {
            gameView.Render();

            if (showMenu)
            {
                menu.Render();
            }
        }

        public override void Update()
        {
            if (showMenu)
            {
                menu.Update();
            }
            else
            {
                gameView.Update();
            }
        }
    }
}
