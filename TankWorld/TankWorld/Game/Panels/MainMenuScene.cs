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
            List<MenuItem> menuItems = new List<MenuItem>();
            menuItems.Add(new MenuItem(new StartGameCommand(), "Start", "Start Game"));
            menuItems.Add(new MenuItem(new QuitGameCommand(), "Quit", "Quit"));
            menuPanel = new MenuPanel(menuItems);
            menuPanel.SetPosition((GameConstants.WINDOWS_X * 1 / 3), 100);
            

        }

        public override void Exit()
        {
            Sprite.RemoveAll();
        }

        public override void HandleInput(InputStruct input)
        {
           switch (input.inputEvent){
                case PRESS_S:
                    menuPanel.GoDown();
                    break;
                case PRESS_W:
                    menuPanel.GoUp();
                    break;
                case PRESS_SPACE:
                    menuPanel.Act();
                    break;
            }
            

        }

        public override void Render()
        {
            menuPanel.Render();
        }

        public override void Update()
        {
            menuPanel.Update();
        }

    }
}
