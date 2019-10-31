using TankWorld.Engine;
using TankWorld.Game.Items;

namespace TankWorld.Game.Commands
{
    public class HideTankHPCommand: Command
    {
        TankObject tank;
        Timer timer;
        //Constructors
        public HideTankHPCommand(TankObject owner, Timer timer)
        {
            tank = owner;
            this.timer = timer;
        }



        //Accessors


        //Methods
        public override void Execute()
        {
            tank.ShowHP = false;
            timer.Pause();
        }
    }
}
