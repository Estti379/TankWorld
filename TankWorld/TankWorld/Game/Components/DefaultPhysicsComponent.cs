using System.Collections.Generic;
using TankWorld.Engine;

namespace TankWorld.Game.Components
{
    class DefaultPhysicsComponent: PhysicsComponent
    {
        public DefaultPhysicsComponent()
        {
            HitBoxes.CollisionRange = 0;
        }



        //Accessors


        //Methods
        override public void Update(GameObject parentObject, ref WorldItems world)
        {
           HitBoxes.Position = parentObject.Position;
        }

    }
}
