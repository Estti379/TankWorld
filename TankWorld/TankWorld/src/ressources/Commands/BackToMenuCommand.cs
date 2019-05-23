
using TankWorld.src.ressources.Events;
using TankWorld.src.ressources.Panels;

namespace TankWorld.src.ressources.Commands
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

