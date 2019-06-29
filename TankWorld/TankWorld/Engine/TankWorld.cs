using TankWorld.Engine;

namespace TankWorld
{
    class TankWorld
    {
        static void Main(string[] args)
        {
            Maploader loader = new Maploader();
            loader.LoadMapMetaData("testMap.json");
            loader.LoadTileSetMetaData("testTileSet.json");

            GameContext myTankGame = GameContext.Instance;
            myTankGame.Start();
        }
    }
}
