

using System;
using System.Collections.Generic;
using TankWorld.Engine;
using TankWorld.Game.Items;
using static SDL2.SDL;

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
                boxType = HitBox.Type.CIRCLE,
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
                hitBoxes.collisionRange = heightBox * 1.1;
            }
            else
            {
                hitBoxes.collisionRange = widthBox * 1.1;
            }

            double angle = parent.DirectionBody;

            //hitBoxes.hitBoxesList["Tank"] = Helper.UpdateRectangleHitBox(hitBoxes.hitBoxesList["Tank"], hitBoxes.position, angle, widthBox, heightBox);
            hitBoxes.hitBoxesList["Tank"] = Helper.UpdateCircleHitBox(hitBoxes.hitBoxesList["Tank"], hitBoxes.position, widthBox/2);
        }

        override public void RenderHitBoxes()
        {
            SDL_Color color = new SDL_Color()
            {
                r = 255,
                g = 255,
                b = 0,
                a = 127
            };

            Coordinate pointA;
            Coordinate pointB;
            Coordinate pointC;
            Coordinate pointD;

            pointA.x = hitBoxes.position.x + hitBoxes.collisionRange / 2;
            pointA.y = hitBoxes.position.y + hitBoxes.collisionRange / 2;

            pointB.x = hitBoxes.position.x + hitBoxes.collisionRange / 2;
            pointB.y = hitBoxes.position.y - hitBoxes.collisionRange / 2;

            pointC.x = hitBoxes.position.x - hitBoxes.collisionRange / 2;
            pointC.y = hitBoxes.position.y - hitBoxes.collisionRange / 2;

            pointD.x = hitBoxes.position.x - hitBoxes.collisionRange / 2;
            pointD.y = hitBoxes.position.y + hitBoxes.collisionRange / 2;

            //Draw boundary boxes yellow
            Sprite.DrawFilledRectangle(Camera.Instance.ConvertMapToScreenCoordinate(pointA),
                                            Camera.Instance.ConvertMapToScreenCoordinate(pointB),
                                            Camera.Instance.ConvertMapToScreenCoordinate(pointC),
                                            Camera.Instance.ConvertMapToScreenCoordinate(pointD), color);

            //Draw hitboxes red
            color.g = 0;
            foreach (KeyValuePair<string, HitBox> entry in hitBoxes.hitBoxesList)
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
