using TankWorld.Engine;
using TankWorld.Game.Events;
using TankWorld.Game.Panels;

namespace TankWorld.Game.Commands
{
    class BackToMenuCommand : MenuCommand
    {
        //Constructors
        public BackToMenuCommand()
        {

        }

        //Accessors


        //Methods
        public override void execute()
        {
            MainEventBus.PostEvent(new SceneStateEvent(SceneStateEvent.Type.CHANGE_SCENE, new MainMenuScene()));
        }

    }
}

