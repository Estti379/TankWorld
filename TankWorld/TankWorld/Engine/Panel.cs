namespace TankWorld.Engine
{
    abstract public class Panel: IRender, IUpdate
    {

        //Constructors
        public Panel()
        {

        }


        //Accessors


        //Methods
        public abstract void Update();
        public abstract void Render();
    }
}
