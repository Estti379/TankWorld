//TODO: A class of nameSpace engine is using something from NameSpace Game = BAD
using System;
using TankWorld.Game;

namespace TankWorld.Engine
{
    abstract public class GameObject: IRender
    {
        private Guid ID;
        
        //Constructors
        public GameObject()
        {
            ID = Guid.NewGuid();
        }


        //Accessors


        //Methods
        public abstract void Update(ref WorldItems world);
        public abstract void Render();

        //For better performance in Hashets!
        public bool Equals(GameObject other)
        {
            return this.ID.Equals(other.ID);
        }
        public override int GetHashCode()
        {
            return this.ID.GetHashCode();
        }

    }
}


