using System.Collections.Generic;
using TankWorld.Engine;
using TankWorld.Game.Commands;
using TankWorld.Game.Events;
using static TankWorld.Engine.InputEnum;

namespace TankWorld.Game.Panels
{
    class PlayScene: Scene, IObserver
    {
        private List<Event> events;

        private MapPanel map;
        private GameViewPanel gameView;
        private MenuPanel menu;

        private Camera camera;

        bool showMenu;

        //Constructors
        public PlayScene()
        {

        }


        //Accessors

        //Methods
        public override void Enter()
        {
            //create camera
            camera = Camera.Instance;
            //create map Panel
            map = new MapPanel();
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
            events = new List<Event>();

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
            events.Add(newEvent);
        }

        public override void Render()
        {
            map.Render();
            gameView.Render();

            if (showMenu)
            {
                menu.Render();
            }
        }

        public override void Update()
        {
            //carefull, when creating new objects while game is paused. they will come into existence out of thin air!
            PollEvents();
            if (showMenu)
            {
                menu.Update();
            }
            else
            {
                gameView.Update();
                camera.Update();
                map.Update();
            }
        }

        private void PollEvents()
        {
            List<Event> events = new List<Event>();
            events.AddRange(this.events);
            this.events.Clear();

            SceneStateEvent stateEvent;
            foreach (Event entry in events)
            {
                if ((stateEvent = entry as SceneStateEvent) != null)
                {
                    switch (stateEvent.eventType)
                    {
                        case SceneStateEvent.Type.FLIP_MENU:
                            showMenu = !showMenu;
                            break;
                        case SceneStateEvent.Type.SPAWN_PROJECTILE_ENTITY:
                            gameView.AddProjectile(stateEvent.Bullet);
                            break;
                        case SceneStateEvent.Type.DESPAWN_PROJECTILE_ENTITY:
                            gameView.Removeprojectile(stateEvent.Bullet);
                            break;

                    }
                }
            }
        }
    }
}
