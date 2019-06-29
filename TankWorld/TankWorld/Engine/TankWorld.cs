using TankWorld.Engine;

namespace TankWorld
{
    class TankWorld
    {
        static void Main(string[] args)
        {
            Maploader loader = new Maploader();
            loader.LoadMapMetaData("test.json");

            GameContext myTankGame = GameContext.Instance;
            myTankGame.Start();
        }
    }
}
