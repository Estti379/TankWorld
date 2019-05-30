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
            world.allObjects = new HashSet<GameObject>();
            this.camera = Camera.Instance;
        }

        //Accessors

        //Methods

        public override void Render()
        {
            foreach (GameObject entry in world.allObjects)
            {
                TankObject tankEntry = entry as TankObject;
                if (tankEntry != null)
                {
                    tankEntry.Render();
                }
            }
            world.player.Render();
            foreach (GameObject entry in world.allObjects) {
                WeaponProjectileObject projectileEntry = entry as WeaponProjectileObject;
                if (projectileEntry != null)
                {
                    projectileEntry.Render();
                }
            }
        }

        public override void Update()
        {

            foreach (GameObject entry in world.allObjects)
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
                    TankObject newTank;
                    for (int i = 0; i < 50; i++)
                    {
                        newTank = new TankObject(spawnPosition, TankObject.TankColor.GREEN);
                        this.AddNewObject(newTank);
                    }
                        

                    break;
            }
        }

        public void AddNewObject(GameObject newObject)
        {
            world.allObjects.Add(newObject);
        }

        internal void RemoveObject(GameObject projectile)
        {
            world.allObjects.Remove(projectile);
        }
    }
}
