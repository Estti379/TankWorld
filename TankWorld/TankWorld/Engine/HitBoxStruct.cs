using System.Collections.Generic;

namespace TankWorld.Engine
{
    public struct HitBoxStruct
    {

        public double collisionRange; //From point position!
        public Coordinate position;

        public Dictionary<string ,HitBox> hitBoxesList;

    }

    public struct HitBox
    {
        public enum Type
        {
            CIRCLE,
            RECTANGLE
        }

        public Type boxType;
        //For circles
        public Coordinate origin;
        public int radius;

        //for rectangles
        public Coordinate pointA;
        public Coordinate pointB;
        public Coordinate pointC;
        public Coordinate pointD;
    }
}
