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
            foreach (BulletObject entry in bullets) {
                entry.Render();
            }
            player.Render();
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

        public void AddBullet(BulletObject newBullet)
        {
            bullets.Add(newBullet);
        }

    }
}
