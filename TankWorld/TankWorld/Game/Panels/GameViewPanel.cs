using System;
using System.Collections.Generic;
using TankWorld.Engine;
using TankWorld.Game.Items;
using static TankWorld.Engine.InputEnum;

namespace TankWorld.Game.Panels
{
    public class GameViewPanel : Panel
    {

        private WorldItems world;
        private Camera camera;

        //Constructors
        public GameViewPanel()
        {
            Coordinate spawnPosition;
            spawnPosition.x = 0;
            spawnPosition.y = 0;
            world.player = new TankObject(spawnPosition, TankObject.TankColor.PLAYER);
            world.bullets = new List<BulletObject>();
            world.tanks = new List<TankObject>();
            this.camera = Camera.Instance;
        }

        //Accessors

        //Methods

        public override void Render()
        {
            foreach (TankObject entry in world.tanks)
            {
                entry.Render();
            }
            world.player.Render();
            foreach (BulletObject entry in world.bullets) {
                entry.Render();
            }
        }

        public override void Update()
        {
            foreach (TankObject entry in world.tanks)
            {
                entry.Update(ref world);
            }
            foreach (BulletObject entry in world.bullets)
            {
                entry.Update(ref world);
            }
            world.player.Update(ref world);
            camera.UpdateTargetPosition(world.player);
        }

        public void HandleInput(InputStruct input)
        {

            switch (input.inputEvent)
            {
                case PRESS_S:
                    world.player.Reverse(1);
                    break;
                case RELEASE_S:
                    world.player.Reverse(0);
                    break;
                case PRESS_W:
                    world.player.Forward(1);
                    break;
                case RELEASE_W:
                    world.player.Forward(0);
                    break;
                case PRESS_A:
                    world.player.TurnLeft(1);
                    break;
                case RELEASE_A:
                    world.player.TurnLeft(0);
                    break;
                case PRESS_D:
                    world.player.TurnRight(1);
                    break;
                case RELEASE_D:
                    world.player.TurnRight(0);
                    break;
                case MOUSE_MOTION:
                    world.player.TurretTarget(input.x,input.y);
                    break;
                case PRESS_LEFT_BUTTON:
                    world.player.Shoot();
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
            world.bullets.Add(newBullet);
        }

        public void AddTank(TankObject newTank)
        {
            world.tanks.Add(newTank);
        }

        internal void RemoveBullet(BulletObject bullet)
        {
            world.bullets.Remove(bullet);
        }
    }
}
