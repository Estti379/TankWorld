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

        static public bool HitBoxIntersection(HitBox box1, HitBox box2, ref Coordinate intersectionPoint)
        {
            bool doesIntersect = false;
            if (box1.boxType == HitBox.Type.CIRCLE && box2.boxType == HitBox.Type.CIRCLE)
            {
                doesIntersect = CircleCircleIntersection(box1, box2, ref intersectionPoint);
            }
            else if (box1.boxType == HitBox.Type.RECTANGLE && box2.boxType == HitBox.Type.RECTANGLE)
            {
                doesIntersect = RectgangleRectangleIntersection(box1, box2, ref intersectionPoint);
            }
            else if (box1.boxType == HitBox.Type.CIRCLE && box2.boxType == HitBox.Type.RECTANGLE)
            {
                doesIntersect = RectangleCircleIntersection(box2, box1, ref intersectionPoint);
            }
            else if (box1.boxType == HitBox.Type.RECTANGLE && box2.boxType == HitBox.Type.CIRCLE)
            {
                doesIntersect = RectangleCircleIntersection(box1, box2, ref intersectionPoint);
            }


            return doesIntersect;
        }

        private static bool RectangleCircleIntersection(HitBox box, HitBox circle, ref Coordinate intersectionPoint)
        {
            bool doesIntersect = false;
            if (PointCircleleIntersection(box.pointA, circle))
            {
                intersectionPoint = box.pointA;
                doesIntersect = true;
            }


            return doesIntersect;
        }

        private static bool PointCircleleIntersection(Coordinate point, HitBox circle)
        {
            if ( Distance(point, circle.origin) <= circle.radius )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool RectgangleRectangleIntersection(HitBox box1, HitBox box2, ref Coordinate intersectionPoint)
        {
            bool doesIntersect = false;

            if ( PointRectangleIntersection(box1.pointA,box2) )
            {
                intersectionPoint = box1.pointA;
                doesIntersect = true;
            }
            else if (PointRectangleIntersection(box1.pointB, box2))
            {
                intersectionPoint = box1.pointB;
                doesIntersect = true;
            }
            else if (PointRectangleIntersection(box1.pointC, box2))
            {
                intersectionPoint = box1.pointC;
                doesIntersect = true;
            }
            else if (PointRectangleIntersection(box1.pointD, box2))
            {
                intersectionPoint = box1.pointD;
                doesIntersect = true;
            }

            return doesIntersect;
        }

        private static bool PointRectangleIntersection(Coordinate point, HitBox box)
        {
            double areaBox = Math.Abs(Distance(box.pointA,box.pointB) * Distance(box.pointB,box.pointC) );
            double sumAreaTriangle = 0;

            //triangle A point B
            sumAreaTriangle += TriangleArea(point, box.pointA, box.pointB);

            //triangle B point C
            sumAreaTriangle += TriangleArea(point, box.pointA, box.pointC);

            //triangle C point D
            sumAreaTriangle += TriangleArea(point, box.pointC, box.pointD);

            //triangle D point A
            sumAreaTriangle += TriangleArea(point, box.pointD, box.pointD);

            if (areaBox < sumAreaTriangle)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private static double TriangleArea(Coordinate A, Coordinate B, Coordinate C)
        {
            return Math.Abs( (A.x*(B.y-C.y) + B.x*(C.y-A.y) + C.x*(A.y-B.y) ) ) / 2.0;
        }

        private static bool CircleCircleIntersection(HitBox circle1, HitBox circle2, ref Coordinate intersectionPoint)
        {
            bool doesIntersect = false;

            if(Distance(circle1.origin, circle2.origin) <= circle1.radius + circle2.radius)
            {
                doesIntersect = true;
                Coordinate delta;
                delta.x = circle1.origin.x - circle2.origin.x;
                delta.y = circle1.origin.y - circle2.origin.y;

                if (delta.x == 0 && delta.y == 0)
                {
                    intersectionPoint = circle1.origin;
                }
                else
                {//could be done better, for when there is a huge overlap
                    double anglePoints = Math.Atan2(delta.y, delta.x);
                    intersectionPoint.x = circle1.radius * Math.Cos(anglePoints);
                    intersectionPoint.y = circle1.radius * Math.Sin(anglePoints);
                }

            }

            return doesIntersect;
        }

        static public Coordinate RotatePoint(Coordinate point, double angle)
        {
            Coordinate rotatedPoint;

            rotatedPoint.x = point.x * Math.Cos(angle) - point.y * Math.Sin(angle);
            rotatedPoint.y = point.x * Math.Sin(angle) + point.y * Math.Cos(angle);

            return rotatedPoint;
        }
        static public HitBox UpdateCircleHitBox(HitBox circleleHitBox, Coordinate center, int radius)
        {
            circleleHitBox.origin = center;
            circleleHitBox.radius = radius;

            return circleleHitBox;
        }

        static public HitBox UpdateRectangleHitBox(HitBox rectangleHitBox, Coordinate center, double angle, int width, int height )
        {

            Coordinate temp;
            //pointA
            temp.x = width / 2;
            temp.y = height / 2;
            temp = Helper.RotatePoint(temp, angle);
            temp.x += center.x;
            temp.y += center.y;
            rectangleHitBox.pointA = temp;

            //pointB
            temp.x = width / 2;
            temp.y = -height / 2;
            temp = Helper.RotatePoint(temp, angle);
            temp.x += center.x;
            temp.y += center.y;
            rectangleHitBox.pointB = temp;

            //pointC
            temp.x = -width / 2;
            temp.y = -height / 2;
            temp = Helper.RotatePoint(temp, angle);
            temp.x += center.x;
            temp.y += center.y;
            rectangleHitBox.pointC = temp;

            //pointD
            temp.x = -width / 2;
            temp.y = height / 2;
            temp = Helper.RotatePoint(temp, angle);
            temp.x += center.x;
            temp.y += center.y;
            rectangleHitBox.pointD = temp;

            return rectangleHitBox;
        }

        static public int TimerToSeconds(Timer time)
        {
            return (int)Math.Round(time.Time/1000) % 60;
        }
        static public int TimerToMinutes(Timer time)
        {
            return ((int)Math.Round(time.Time/1000)/60 ) % 60;
        }

    }
}
