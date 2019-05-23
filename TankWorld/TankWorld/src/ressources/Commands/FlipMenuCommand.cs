using TankWorld.src.ressources.Events;
using TankWorld.src.ressources.Panels;

namespace TankWorld.src.ressources.Commands
{
    class FlipMenuCommand : MenuCommand
    {
        //Constructors
        public FlipMenuCommand()
        {

        }

        //Accessors


        //Methods
        public override void execute()
        {
            MainEventBus.PostEvent(new SceneStateEvent(SceneStateEvent.Type.FLIP_MENU));
        }

    }
}
