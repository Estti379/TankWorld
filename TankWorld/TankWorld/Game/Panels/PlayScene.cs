using System.Collections.Generic;
using TankWorld.Engine;
using TankWorld.Game.Commands;
using TankWorld.Game.Events;
using static TankWorld.Engine.InputEnum;

namespace TankWorld.Game.Panels
{
    class PlayScene: Scene, IObserver
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
            menuItems.Add(new MenuItem(new FlipMenuCommand(), "Continue", "Continue Game"));
            menuItems.Add(new MenuItem(new StartGameCommand(), "Restart", "Restart Level"));
            menuItems.Add(new MenuItem(new BackToMenuCommand(), "Back", "Back To Main Menu"));
            menuItems.Add(new MenuItem(new QuitGameCommand(), "Quit", "Quit Game"));
            menu = new MenuPanel(menuItems);
            menu.SetPosition((GameConstants.WINDOWS_X * 1 / 3), 100);
            showMenu = false;

            MainEventBus.Register(this);

        }

        public override void Exit()
        {
            Sprite.RemoveAll();
        }

        public override void HandleInput(InputStruct input)
        {

            if(input.inputEvent == PRESS_ESCAPE)
            {
                showMenu = !showMenu;
            }
            if (showMenu)
            {
                switch (input.inputEvent)
                {
                    case PRESS_S:
                        menu.GoDown();
                        break;
                    case PRESS_W:
                        menu.GoUp();
                        break;
                    case PRESS_SPACE:
                        menu.Act();
                        break;
                }
            }
            else
            {
                gameView.HandleInput(input);
            }
            

        }

        public void OnEvent(Event newEvent)
        {
            SceneStateEvent stateEvent;
            if ((stateEvent = newEvent as SceneStateEvent) != null)
            {
                switch (stateEvent.eventType)
                {
                    case SceneStateEvent.Type.FLIP_MENU:
                        showMenu = !showMenu;
                        break;

                }
            }
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
