using System;

namespace TankWorld.Engine
{
    static public class Helper
    {
        static public Random random = new Random();
        static public double Distance(Coordinate position1, Coordinate position2)
        {
            return Math.Sqrt(Math.Pow(position2.x - position1.x, 2) + Math.Pow(position2.y - position1.y, 2));
        }

        static public double NormalizeRad(double angle)
        {
            while (angle < 0)
            {
                angle += 2 * Math.PI;
            }
            while (angle > 2 * Math.PI)
            {
                angle -= 2 * Math.PI;
            }
            return angle;
        }


    }
}
