using TankWorld.Engine;
using TankWorld.Game.Events;
using TankWorld.Game.Panels;

namespace TankWorld.Game.Commands
{
    class QuitGameCommand: MenuCommand
    {
        //Constructors
        public QuitGameCommand()
        {

        }

        //Accessors


        //Methods
        public override void Execute()
        {
            MainEventBus.PostEvent(new SceneStateEvent(SceneStateEvent.Type.CHANGE_SCENE, new ExitScene()));
        }

    }
}
