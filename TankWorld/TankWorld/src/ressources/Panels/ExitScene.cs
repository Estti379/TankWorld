

using TankWorld.src.ressources.Events;

namespace TankWorld.src.ressources.Panels
{
    class ExitScene: Scene
    {
        //Constructors
        public ExitScene()
        {

        }

        //Accessors

        //Methods


        public override void Enter()
        {
            MainEventBus.PostEvent(new SceneStateEvent(SceneStateEvent.Type.EXIT_GAME));
        }

        public override void Exit()
        {
            /*empty*/
        }

        public override void HandleInput(InputStruct input)
        {
            /*empty*/
        }

        public override void Render()
        {
            /*empty*/
        }

        public override void Update()
        {
            /*empty*/
        }

    }
}
