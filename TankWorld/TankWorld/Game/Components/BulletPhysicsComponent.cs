

using System;
using System.Collections.Generic;
using TankWorld.Engine;
using TankWorld.Game.Items;

namespace TankWorld.Game.Components
{
    class BulletPhysicsComponent: PhysicsComponent
    {
        private int heightBox;
        private int widthBox;

        //Constructors
        public BulletPhysicsComponent(BulletObject owner)
        {
            //parent = owner;
            InitializeHitBoxes(owner);
        }


        //Accessors

        
        //Methods
        override public void Update(GameObject parentObject, ref WorldItems world)
        {
            CheckForCloseness(parentObject, ref world);
            BulletObject parentBullet = parentObject as BulletObject;
            UpdateHitBox(parentBullet);
        }

        public void InitializeHitBoxes(BulletObject parent)
        {
            HitBox bulletHitBox = new HitBox
            {
                boxType = HitBox.Type.RECTANGLE,
            };

            HitBoxes.hitBoxesList.Add("Bullet", bulletHitBox);
            UpdateHitBox(parent);
        }

        public void UpdateHitBox(BulletObject parent)
        {
            HitBoxes.Position = parent.Position;
            heightBox = parent.Model.AllSprites["Bullet"].Pos.h;
            widthBox = parent.Model.AllSprites["Bullet"].Pos.w;
            if(heightBox >= widthBox)
            {
                HitBoxes.CollisionRange = heightBox;
            }
            else
            {
                HitBoxes.CollisionRange = widthBox;
            }

            HitBox bulletHitBox = new HitBox
            {
                boxType = HitBox.Type.RECTANGLE,
            };
            double angle = Math.Atan2(parent.SpeedVektor.y, parent.SpeedVektor.x);



            HitBoxes.hitBoxesList["Bullet"] = Helper.UpdateRectangleHitBox(HitBoxes.hitBoxesList["Bullet"], HitBoxes.Position, angle, widthBox, heightBox);


        }

    }
}
