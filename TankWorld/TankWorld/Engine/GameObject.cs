//TODO: A class of nameSpace engine is using something from NameSpace Game = BAD
using System;
using TankWorld.Game;

namespace TankWorld.Engine
{
    abstract public class GameObject: IRender
    {
        private Guid id;

        private Coordinate position;

        //Constructors
        public GameObject()
        {
            id = Guid.NewGuid();
        }


        //Accessors
        public ref Coordinate Position { get => ref position;}
        public Guid Id { get => id;}

        //Methods
        public abstract void Update(ref WorldItems world);
        public abstract void Render(RenderLayer layer);

        //For better performance in Hashets!
        public bool Equals(GameObject other)
        {
            return this.id.Equals(other.id);
        }
        public override int GetHashCode()
        {
            return this.id.GetHashCode();
        }

    }
}


