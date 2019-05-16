using System.Collections.Generic;
using TankWorld.src.ressources.Items;
using static TankWorld.src.InputEnum;

namespace TankWorld.src.ressources.Panels
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

        public void HandleInput(InputEnum input)
        {

            switch (input)
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
            }
        }

    }
}
