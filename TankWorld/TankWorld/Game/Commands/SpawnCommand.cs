

using TankWorld.Engine;
using TankWorld.Game.Events;

namespace TankWorld.Game.Commands
{
    public class SpawnCommand : Command
    {
        Timer time;
        public SpawnCommand(Timer time)
        {
            this.time = time;
        }

        public override void Execute()
        {
            time.Reset();
            MainEventBus.PostEvent(new SceneStateEvent(SceneStateEvent.Type.SPAWN_GROUP) );
        }
    }
}
