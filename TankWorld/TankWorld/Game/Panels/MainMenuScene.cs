using System.Collections.Generic;
using TankWorld.Engine;
using TankWorld.Game.Commands;
using static TankWorld.Engine.InputEnum;

namespace TankWorld.Game.Panels
{
    public class MainMenuScene: Scene
    {
        private MenuPanel menuPanel;

        //Constructors
        public MainMenuScene()
        {

        }


        //Accessors

        //Methods
        public override void Enter()
        {
            PlayParameters playParameters = new PlayParameters();

            List<MenuItem> menuItems = new List<MenuItem>();

            playParameters.mapType = MapTypeEnum.UNLIMITED;
            menuItems.Add(new MenuItem(new StartGameCommand(playParameters), "StartUnlimited", "Start Unlimited"));
            playParameters.mapType = MapTypeEnum.TILED;
            playParameters.mapFileName = "test2.json";
            menuItems.Add(new MenuItem(new StartGameCommand(playParameters), "StartTiled", "Start Tiled"));
            menuItems.Add(new MenuItem(new QuitGameCommand(), "Quit", "Quit"));

            menuPanel = new MenuPanel(menuItems);

            menuPanel.SetPosition((WindowX * 1 / 3), 100);
            

        }

        public override void Exit()
        {
            Sprite.RemoveAll();
        }

        public override void HandleInput(InputStruct input)
        {
           switch (input.inputEvent)
           {
                case PRESS_S:
                    menuPanel.GoDown();
                    break;
                case PRESS_DOWN:
                    menuPanel.GoDown();
                    break;
                case PRESS_W:
                    menuPanel.GoUp();
                    break;
                case PRESS_UP:
                    menuPanel.GoUp();
                    break;
                case PRESS_SPACE:
                    menuPanel.Act();
                    break;
                case PRESS_ENTER:
                    menuPanel.Act();
                    break;
                case PRESS_A:
                    GameContext.Instance.ChangeResolution(1280,720);
                    Camera.Instance.SetSubScreenDimensions(0, 0, 1280, 720);
                    break;
                case PRESS_D:
                    GameContext.Instance.ChangeResolution(1920, 1080);
                    Camera.Instance.SetSubScreenDimensions(0, 0, 1920, 1080);
                    break;
                case PRESS_P:
                    GameContext.Instance.ToggleFullScreen();
                    break;
           }
            

        }

        public override void Render(RenderLayer layer)
        {
            if(layer == RenderLayer.MENU)
            {
                menuPanel.Render(layer);
            }
            

        }

        public override void Update()
        {
            menuPanel.Update();
        }

    }
}
