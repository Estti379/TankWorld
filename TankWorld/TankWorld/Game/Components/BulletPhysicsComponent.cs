

using System;
using System.Collections.Generic;
using TankWorld.Engine;
using TankWorld.Game.Items;

namespace TankWorld.Game.Components
{
    class BulletPhysicsComponent: PhysicsComponent
    {

        //private BulletObject parent;
        private HitBoxStruct hitBoxes;
        private int heightBox;
        private int widthBox;

        //Constructors
        public BulletPhysicsComponent(BulletObject owner)
        {
            //parent = owner;
            InitializeHitBoxes(owner);
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
            CheckForCloseness(parentObject, ref world);
            BulletObject parentBullet = parentObject as BulletObject;
            UpdateHitBox(parentBullet);
        }

        public void InitializeHitBoxes(BulletObject parent)
        {
            hitBoxes.hitBoxesList = new Dictionary<string, HitBox>();
            HitBox bulletHitBox = new HitBox
            {
                boxType = HitBox.Type.RECTANGLE,
            };

            hitBoxes.hitBoxesList.Add("Bullet", bulletHitBox);
            UpdateHitBox(parent);
        }

        public void UpdateHitBox(BulletObject parent)
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

            HitBox bulletHitBox = new HitBox
            {
                boxType = HitBox.Type.RECTANGLE,
            };
            double angle = Math.Atan2(parent.SpeedVektor.y, parent.SpeedVektor.x);



            hitBoxes.hitBoxesList["Bullet"] = Helper.UpdateRectangleHitBox(hitBoxes.hitBoxesList["Bullet"], hitBoxes.position, angle, widthBox, heightBox);


        }

        public override void RenderHitBoxes()
        {
            throw new NotImplementedException();
        }
    }
}
