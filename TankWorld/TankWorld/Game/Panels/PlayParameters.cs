
namespace TankWorld.Game.Panels
{
    public struct PlayParameters
    {
        public MapTypeEnum mapType;
        public string mapFileName;
    }

    public enum MapTypeEnum
    {
        UNDEFINED,
        TILED,
        UNLIMITED
    }
}
