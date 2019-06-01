

using System;
using System.Collections.Generic;
using TankWorld.Engine;
using TankWorld.Game.Items;
using static SDL2.SDL;

namespace TankWorld.Game.Components
{
    class TankPhysicsComponent: PhysicsComponent
    {
        //private HitBoxStruct hitBoxes;
        private int heightBox;
        private int widthBox;

        //Constructors
        public TankPhysicsComponent(TankObject owner)
        {
            InitializeHitBoxes(owner);
        }


        //Accessors

        //Methods
        override public void Update(GameObject parentObject, ref WorldItems world)
        {
            CheckForCloseness(parentObject, ref world);
            TankObject parentTank = parentObject as TankObject;
            UpdateHitBox(parentTank);
        }

        public void InitializeHitBoxes(TankObject parent)
        {
            HitBox bulletHitBox = new HitBox
            {
                boxType = HitBox.Type.CIRCLE,
            };

            HitBoxes.hitBoxesList.Add("Tank", bulletHitBox);
            UpdateHitBox(parent);
        }

        public void UpdateHitBox(TankObject parent)
        {
            HitBoxes.Position = parent.Position;
            heightBox = parent.Model.AllSprites["TankBody"].Pos.h;
            widthBox = parent.Model.AllSprites["TankBody"].Pos.w;
            if (heightBox >= widthBox)
            {
                HitBoxes.CollisionRange = heightBox * 1.1;
            }
            else
            {
                HitBoxes.CollisionRange = widthBox * 1.1;
            }

            double angle = parent.DirectionBody;

            //hitBoxes.hitBoxesList["Tank"] = Helper.UpdateRectangleHitBox(hitBoxes.hitBoxesList["Tank"], hitBoxes.position, angle, widthBox, heightBox);
            HitBoxes.hitBoxesList["Tank"] = Helper.UpdateCircleHitBox(HitBoxes.hitBoxesList["Tank"], HitBoxes.Position, widthBox/2);
        }

        
    }
}
