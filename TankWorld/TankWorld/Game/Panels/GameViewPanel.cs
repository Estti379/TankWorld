using System.Collections.Generic;
using TankWorld.Engine;
using TankWorld.Game.Items;
using static TankWorld.Engine.InputEnum;

namespace TankWorld.Game.Panels
{
    public class GameViewPanel: Panel
    {
        //TODO: Use dictionary instead too keep track of ID
        List<TankObject> tanks;
        TankObject player;

        //Constructors
        public GameViewPanel()
        {
            player = new TankObject("Player");
        }

        //Accessors


        //Methods

        public override void Render()
        {
            player.Render();
        }

        public override void Update()
        {
            player.Update();
        }

        public void HandleInput(InputStruct input)
        {

            switch (input.inputEvent)
            {
                case PRESS_S:
                    player.Accelerate(-1);
                    break;
                case RELEASE_S:
                    player.Accelerate(0);
                    break;
                case PRESS_W:
                    player.Accelerate(1);
                    break;
                case RELEASE_W:
                    player.Accelerate(0);
                    break;
                case PRESS_A:
                    player.Turn(-0.5);
                    break;
                case RELEASE_A:
                    player.Turn(0);
                    break;
                case PRESS_D:
                    player.Turn(0.5);
                    break;
                case RELEASE_D:
                    player.Turn(0);
                    break;
                case MOUSE_MOTION:
                    player.TurretTarget(input.x,input.y);
                    break;
                case PRESS_LEFT_BUTTON:
                    player.Shoot();
                    break;
            }
        }

    }
}
