using System;
using System.Collections.Generic;
using TankWorld.Engine;
using TankWorld.Game.Items;
using static TankWorld.Engine.InputEnum;

namespace TankWorld.Game.Panels
{
    public class GameViewPanel : Panel
    {

        private PlayScene parent;
        private Camera camera;

        //Constructors
        public GameViewPanel(PlayScene parent)
        {
            Coordinate spawnPosition;
            spawnPosition.x = 0;
            spawnPosition.y = 0;
            parent.World.player = new TankObject(spawnPosition, TankObject.TankColor.PLAYER);
            parent.World.allObjects = new List<GameObject>();
            this.camera = Camera.Instance;
            this.parent = parent;
        }

        //Accessors

        //Methods

        public override void Render(RenderLayer layer)
        {
            if (layer == RenderLayer.GROUND || layer == RenderLayer.HITBOXES || layer == RenderLayer.MAINBOARD || layer == RenderLayer.OVERHEAD)
            {
                foreach (GameObject entry in parent.World.allObjects)
                {
                    entry.Render(layer);
                }
                if (layer == RenderLayer.MAINBOARD  || layer == RenderLayer.HITBOXES)
                {
                    parent.World.player.Render(layer);
                }
                
            }
   
        }

        public override void Update()
        {

            foreach (GameObject entry in parent.World.allObjects)
            {
                entry.Update(ref parent.World);
            }
            parent.World.player.Update(ref parent.World);
            camera.UpdateTargetPosition(parent.World.player);
        }

        public void HandleInput(InputStruct input)
        {

            switch (input.inputEvent)
            {
                case PRESS_S:
                    parent.World.player.Reverse(1);
                    break;
                case RELEASE_S:
                    parent.World.player.Reverse(0);
                    break;
                case PRESS_W:
                    parent.World.player.Forward(1);
                    break;
                case RELEASE_W:
                    parent.World.player.Forward(0);
                    break;
                case PRESS_A:
                    parent.World.player.TurnLeft(1);
                    break;
                case RELEASE_A:
                    parent.World.player.TurnLeft(0);
                    break;
                case PRESS_D:
                    parent.World.player.TurnRight(1);
                    break;
                case RELEASE_D:
                    parent.World.player.TurnRight(0);
                    break;
                case MOUSE_MOTION:
                    parent.World.player.TurretTarget(input.x,input.y);
                    break;
                case PRESS_LEFT_BUTTON:
                    parent.World.player.Shoot();
                    break;
                case PRESS_P:
                    
                    Coordinate spawnPosition;
                    spawnPosition.x = input.x;
                    spawnPosition.y = input.y;
                    spawnPosition = camera.ConvertScreenToMapCoordinate(spawnPosition);
                    TankObject newTank;
                    double angle;
                    for (int i = 0; i < 30; i++)
                    {
                        angle = Helper.random.NextDouble();
                        spawnPosition.x += 200 * Math.Cos(2 * Math.PI * angle);
                        spawnPosition.y += 200 * Math.Sin(2 * Math.PI * angle);
                        newTank = new TankObject(spawnPosition, TankObject.TankColor.GREEN);
                        this.AddNewObject(newTank);
                    }
                    break;
            }
        }

        public void AddNewObject(GameObject newObject)
        {
            parent.World.allObjects.Add(newObject);
        }

        internal void RemoveObject(GameObject projectile)
        {
            parent.World.allObjects.Remove(projectile);
        }
    }
}
