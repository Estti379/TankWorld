

using System;
using System.Collections.Generic;
using TankWorld.Engine;
using TankWorld.Game.Items;

namespace TankWorld.Game.Components
{
    class TankPhysicsComponent: PhysicsComponent
    {
        private HitBoxStruct hitBoxes;
        private int heightBox;
        private int widthBox;

        //Constructors
        public TankPhysicsComponent(TankObject owner)
        {
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
            TankObject parentTank = parentObject as TankObject;
            UpdateHitBox(parentTank);
        }

        public void InitializeHitBoxes(TankObject parent)
        {
            hitBoxes.hitBoxesList = new Dictionary<string, HitBox>();
            HitBox bulletHitBox = new HitBox
            {
                boxType = HitBox.Type.RECTANGLE,
            };

            hitBoxes.hitBoxesList.Add("Tank", bulletHitBox);
            UpdateHitBox(parent);
        }

        public void UpdateHitBox(TankObject parent)
        {
            hitBoxes.position = parent.Position;
            heightBox = parent.Model.AllSprites["TankBody"].Pos.h;
            widthBox = parent.Model.AllSprites["TankBody"].Pos.w;
            if (heightBox >= widthBox)
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
            double angle = parent.DirectionBody;

            hitBoxes.hitBoxesList["Tank"] = Helper.CreateRectangleHitBox(hitBoxes.position, angle, widthBox, heightBox);

        }
    }
}
