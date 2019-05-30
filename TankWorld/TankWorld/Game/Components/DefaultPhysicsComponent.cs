using System.Collections.Generic;
using TankWorld.Engine;

namespace TankWorld.Game.Components
{
    class DefaultPhysicsComponent: PhysicsComponent
    {
        HitBoxStruct hitBoxes;
        public DefaultPhysicsComponent()
        {
            hitBoxes.collisionRange = 0;
            hitBoxes.hitBoxesList = new Dictionary<string, HitBox>();
        }

        public override HitBoxStruct HitBoxes
        {
            get
            {
                return hitBoxes;
            }
        }

        //Accessors


        //Methods
        override public void Update(GameObject parentObject, ref WorldItems world)
        {
           hitBoxes.position = parentObject.Position;
        }
    }
}
