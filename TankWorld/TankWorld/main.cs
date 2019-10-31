using TankWorld.Engine;

namespace TankWorld
{
    class main
    {
        static void Main(string[] args)
        {
            GameContext myTankGame = GameContext.Instance;
            myTankGame.Start();
        }
    }
}
