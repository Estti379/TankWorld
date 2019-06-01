using System;
using TankWorld.Engine;

namespace TankWorld.Game.Components
{
    abstract public class PhysicsComponent
    {
        //Constructor
        public PhysicsComponent()
        {

        }

        abstract public HitBoxStruct HitBoxes { get; }

        //Accessors


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
                        double distance = parentHitBox.collisionRange + otherHitBox.collisionRange;

                        if ((Math.Abs(parentHitBox.position.x - otherHitBox.position.x) <= distance) && (Math.Abs(parentHitBox.position.y - otherHitBox.position.y) <= distance))
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
                    double distance = parentHitBox.collisionRange + otherHitBox.collisionRange;

                    if ((Math.Abs(parentHitBox.position.x - otherHitBox.position.x) <= distance) && (Math.Abs(parentHitBox.position.y - otherHitBox.position.y) <= distance))
                    {
                        collidingParent.CheckForCollision(collidingObject);
                    }

                }


            }


        }

        abstract public void RenderHitBoxes();
    }
}
