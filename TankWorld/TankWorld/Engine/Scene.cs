namespace TankWorld.Engine
{
    abstract public class Scene: IUpdate, IRender
    {

        private GameContext parent;

        //Constructors
        public Scene()
        {
            parent = GameContext.Instance;
        }

        //Accessors
        public int WindowX { get => parent.WindowX;}
        public int WindowY { get => parent.WindowY;}

        //Methods
        abstract public void Enter();
        abstract public void Exit();

        abstract public void HandleInput(InputStruct input);

        abstract public void Update();
        abstract public void Render(RenderLayer layer);
    }
}
