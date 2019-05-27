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
        private Camera camera;

        //Constructors
        public GameViewPanel()
        {
            Coordinate spawnPosition;
            spawnPosition.x = 0;
            spawnPosition.y = 0;
            player = new TankObject(spawnPosition, TankObject.TankColor.PLAYER);
            bullets = new List<BulletObject>();
            tanks = new List<TankObject>();
            this.camera = Camera.Instance;
        }

        //Accessors

        //Methods

        public override void Render()
        {
            foreach (TankObject entry in tanks)
            {
                entry.Render();
            }
            player.Render();
            foreach (BulletObject entry in bullets) {
                entry.Render();
            }
        }

        public override void Update()
        {
            foreach (TankObject entry in tanks)
            {
                entry.Update();
            }
            foreach (BulletObject entry in bullets)
            {
                entry.Update();
            }
            player.Update();
            camera.UpdateTargetPosition(player);
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
                case PRESS_P:
                    Coordinate spawnPosition;
                    spawnPosition.x = input.x;
                    spawnPosition.y = input.y;
                    spawnPosition = camera.ConvertScreenToMapCoordinate(spawnPosition);
                    TankObject newTank = new TankObject(spawnPosition, TankObject.TankColor.GREEN);
                    this.AddTank(newTank);
                    break;
            }
        }

        public void AddBullet(BulletObject newBullet)
        {
            bullets.Add(newBullet);
        }

        public void AddTank(TankObject newTank)
        {
            tanks.Add(newTank);
        }

    }
}
