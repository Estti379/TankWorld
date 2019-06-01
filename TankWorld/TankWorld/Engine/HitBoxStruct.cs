using System.Collections.Generic;

namespace TankWorld.Engine
{
    public struct HitBoxStruct
    {

        private double collisionRange; //From point position!
        private Coordinate position;

        public Dictionary<string ,HitBox> hitBoxesList;


        //Accessors
        public double CollisionRange { get => collisionRange; set => collisionRange = value; }
        public Coordinate Position { get => position; set => position = value; }


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
