using System.Collections.Generic;
using TankWorld.Engine;
using TankWorld.Game.Items;
using static TankWorld.Engine.InputEnum;

namespace TankWorld.Game.Panels
{
    public class GameViewPanel: Panel
    {
        //TODO: Use dictionary instead of list, to keep track of ID (but is ID tracking necessary?)
        private List<TankObject> tanks;
        private List<BulletObject> bullets;
        private TankObject player;

        //Constructors
        public GameViewPanel()
        {
            player = new TankObject("Player");
            bullets = new List<BulletObject>();
            tanks = new List<TankObject>();
        }

        //Accessors

        //Methods

        public override void Render()
        {
            player.Render();
            foreach (BulletObject entry in bullets) {
                entry.Render();
            }
        }

        public override void Update()
        {
            foreach (BulletObject entry in bullets)
            {
                entry.Update();
            }
            player.Update();
        }

        public void HandleInput(InputStruct input)
        {

            switch (input.inputEvent)
            {
                case PRESS_S:
                    player.Reverse(1);
                    break;
                case RELEASE_S:
                    player.Reverse(0);
                    break;
                case PRESS_W:
                    player.Forward(1);
                    break;
                case RELEASE_W:
                    player.Forward(0);
                    break;
                case PRESS_A:
                    player.TurnLeft(1);
                    break;
                case RELEASE_A:
                    player.TurnLeft(0);
                    break;
                case PRESS_D:
                    player.TurnRight(1);
                    break;
                case RELEASE_D:
                    player.TurnRight(0);
                    break;
                case MOUSE_MOTION:
                    player.TurretTarget(input.x,input.y);
                    break;
                case PRESS_LEFT_BUTTON:
                    player.Shoot();
                    break;
            }
        }

        public void AddBullet(BulletObject newBullet)
        {
            bullets.Add(newBullet);
        }

    }
}
