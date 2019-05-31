

using TankWorld.Engine;

namespace TankWorld.Game.Commands
{
    public class ThrowEventCommand : Command
    {
        Event eventToThrow;
        public ThrowEventCommand(Event eventToThrow)
        {
            this.eventToThrow = eventToThrow;
        }


        public override void Execute()
        {
            MainEventBus.PostEvent(eventToThrow);
        }
    }
}
