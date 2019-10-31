using TankWorld.Engine;
using TankWorld.Game.Events;
using TankWorld.Game.Panels;

namespace TankWorld.Game.Commands
{
    class StartGameCommand: MenuCommand
    {
        PlayParameters playparameters;
        //Constructors
        public StartGameCommand(PlayParameters parameters)
        {
            playparameters = parameters;
        }

        //Accessors


        //Methods
        public override void Execute()
        {
            MainEventBus.PostEvent(new SceneStateEvent(SceneStateEvent.Type.CHANGE_SCENE, new PlayScene(playparameters)));
        }

    }
}
