using System;

namespace TankWorld.Engine
{
    static public class Helper
    {
        static public double Distance(Coordinate position1, Coordinate position2)
        {
            return Math.Sqrt(Math.Pow(position2.x - position1.x, 2) + Math.Pow(position2.y - position1.y, 2));
        }
    }
}
