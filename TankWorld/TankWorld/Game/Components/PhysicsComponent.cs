using TankWorld.Engine;

namespace TankWorld.Game.Components
{
    abstract public class PhysicsComponent
    {
        //Constructor
        public PhysicsComponent()
        {

        }

        abstract public HitBoxStruct HitBoxes { get;}

        //Accessors


        //Methods
        abstract public void Update(GameObject parentObject, ref WorldItems world);
    }
}
