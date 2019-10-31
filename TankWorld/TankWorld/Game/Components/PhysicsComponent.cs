using System;
using System.Collections.Generic;
using TankWorld.Engine;
using static SDL2.SDL;

namespace TankWorld.Game.Components
{
    abstract public class PhysicsComponent
    {
        private HitBoxStruct hitBoxes;

        //Constructor
        public PhysicsComponent()
        {
            hitBoxes.hitBoxesList = new Dictionary<string, HitBox>();
        }

        

        //Accessors
        public ref HitBoxStruct HitBoxes { get => ref hitBoxes;}

        //Methods
        abstract public void Update(GameObject parentObject, ref WorldItems world);

        public void CheckForCloseness(GameObject parentObject, ref WorldItems world)
        {
            ICollide collidingParent = parentObject as ICollide;
            if (collidingParent != null)
            {
                ICollide collidingObject;
                foreach (GameObject entry in world.allObjects)
                {
                    collidingObject = entry as ICollide;
                    if (collidingObject != null && parentObject.Id != entry.Id)
                    {
                        HitBoxStruct parentHitBox = collidingParent.GetHitBoxes();
                        HitBoxStruct otherHitBox = collidingObject.GetHitBoxes();
                        double distance = parentHitBox.CollisionRange + otherHitBox.CollisionRange;

                        if ((Math.Abs(parentHitBox.Position.x - otherHitBox.Position.x) <= distance) && (Math.Abs(parentHitBox.Position.y - otherHitBox.Position.y) <= distance))
                        {
                            collidingParent.CheckForCollision(collidingObject);
                        }

                    }

                }
                //Check collision with player
                collidingObject = world.player as ICollide;
                if (collidingObject != null && parentObject.Id != world.player.Id)
                {
                    HitBoxStruct parentHitBox = collidingParent.GetHitBoxes();
                    HitBoxStruct otherHitBox = collidingObject.GetHitBoxes();
                    double distance = parentHitBox.CollisionRange + otherHitBox.CollisionRange;

                    if ((Math.Abs(parentHitBox.Position.x - otherHitBox.Position.x) <= distance) && (Math.Abs(parentHitBox.Position.y - otherHitBox.Position.y) <= distance))
                    {
                        collidingParent.CheckForCollision(collidingObject);
                    }

                }


            }


        }

        public void RenderHitBoxes()
        {
            SDL_Color color = new SDL_Color()
            {
                r = 255,
                g = 255,
                b = 0,
                a = 50
            };

            Coordinate pointA;
            Coordinate pointB;
            Coordinate pointC;
            Coordinate pointD;

            pointA.x = HitBoxes.Position.x + HitBoxes.CollisionRange / 2;
            pointA.y = HitBoxes.Position.y + HitBoxes.CollisionRange / 2;

            pointB.x = HitBoxes.Position.x + HitBoxes.CollisionRange / 2;
            pointB.y = HitBoxes.Position.y - HitBoxes.CollisionRange / 2;

            pointC.x = HitBoxes.Position.x - HitBoxes.CollisionRange / 2;
            pointC.y = HitBoxes.Position.y - HitBoxes.CollisionRange / 2;

            pointD.x = HitBoxes.Position.x - HitBoxes.CollisionRange / 2;
            pointD.y = HitBoxes.Position.y + HitBoxes.CollisionRange / 2;

            //Draw boundary boxes yellow
            Sprite.DrawFilledRectangle(Camera.Instance.ConvertMapToScreenCoordinate(pointA),
                                            Camera.Instance.ConvertMapToScreenCoordinate(pointB),
                                            Camera.Instance.ConvertMapToScreenCoordinate(pointC),
                                            Camera.Instance.ConvertMapToScreenCoordinate(pointD), color);

            //Draw hitboxes red
            color.g = 0;
            foreach (KeyValuePair<string, HitBox> entry in HitBoxes.hitBoxesList)
            {
                if (entry.Value.boxType == HitBox.Type.RECTANGLE)
                {
                    Sprite.DrawFilledRectangle(Camera.Instance.ConvertMapToScreenCoordinate(entry.Value.pointA),
                            Camera.Instance.ConvertMapToScreenCoordinate(entry.Value.pointB),
                            Camera.Instance.ConvertMapToScreenCoordinate(entry.Value.pointC),
                            Camera.Instance.ConvertMapToScreenCoordinate(entry.Value.pointD), color);
                }
                else if (entry.Value.boxType == HitBox.Type.CIRCLE)
                {
                    Sprite.DrawFilledCircle(Camera.Instance.ConvertMapToScreenCoordinate(entry.Value.origin), entry.Value.radius, color);
                }
            }
        }
    }
}
