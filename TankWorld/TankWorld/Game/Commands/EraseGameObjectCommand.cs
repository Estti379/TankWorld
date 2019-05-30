

using TankWorld.Engine;
using TankWorld.Game.Events;

namespace TankWorld.Game.Commands
{
    class EraseGameObjectCommand : Command
    {
        GameObject owner;
        //Constructors
        public EraseGameObjectCommand(GameObject owner)
        {
            this.owner = owner;
        }

        //Accessors


        //Methods
        public override void Execute()
        {
            MainEventBus.PostEvent(new SceneStateEvent(SceneStateEvent.Type.DESPAWN_ENTITY, owner));

        }
    }
}
