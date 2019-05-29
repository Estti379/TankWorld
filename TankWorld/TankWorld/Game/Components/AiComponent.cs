using TankWorld.Game.Items;

namespace TankWorld.Game.Components
{
    public abstract class AiComponent
    {
        //Constructor
        public AiComponent()
        {
        }

        //Accessors


        //Methods
        abstract public void Update(TankObject tank, ref WorldItems world);
    }
}
