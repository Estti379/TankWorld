

using System;
using System.Collections.Generic;
using TankWorld.Engine;
using TankWorld.Game.Items;

namespace TankWorld.Game.Components
{
    class BulletPhysicsComponent: PhysicsComponent
    {

        private BulletObject parent;
        private HitBoxStruct hitBoxes;
        private int heightBox;
        private int widthBox;

        //Constructors
        public BulletPhysicsComponent(BulletObject owner)
        {
            parent = owner;
            InitializeHitBoxes();
        }


        //Accessors
        public override HitBoxStruct HitBoxes
        {
            get
            {
                return hitBoxes;
            }
        }

        

        //Methods
        override public void Update(GameObject parentObject, ref WorldItems world)
        {
            ICollide collidingParent = parentObject as ICollide;
            if (collidingParent != null)
            {
                foreach (GameObject entry in world.allObjects)
                {
                    ICollide collidingObject = entry as ICollide;
                    if (collidingObject != null)
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
        }

        public void InitializeHitBoxes()
        {
            hitBoxes.hitBoxesList = new Dictionary<string, HitBox>();
            HitBox bulletHitBox = new HitBox
            {
                boxType = HitBox.Type.RECTANGLE,
            };

            hitBoxes.hitBoxesList.Add("Bullet", bulletHitBox);
            UpdateHitBox();
        }

        public void UpdateHitBox()
        {
            hitBoxes.position = parent.Position;
            heightBox = parent.Model.AllSprites["Bullet"].Pos.h;
            widthBox = parent.Model.AllSprites["Bullet"].Pos.w;
            if(heightBox >= widthBox)
            {
                hitBoxes.collisionRange = heightBox;
            }
            else
            {
                hitBoxes.collisionRange = widthBox;
            }

            Coordinate temp;
            HitBox bulletHitBox = new HitBox
            {
                boxType = HitBox.Type.RECTANGLE,
            };
            double angle = Math.Atan2(parent.SpeedVektor.y, parent.SpeedVektor.x);



            hitBoxes.hitBoxesList["Bullet"] = Helper.CreateRectangleHitBox(hitBoxes.position, angle, widthBox, heightBox);


        }


    }
}
