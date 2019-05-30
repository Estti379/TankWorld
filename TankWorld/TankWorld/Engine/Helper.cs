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
            double areaBox = Math.Abs((box.pointB.x * box.pointA.y - box.pointA.x * box.pointB.y) + (box.pointC.x * box.pointB.y - box.pointB.x * box.pointC.y) + (box.pointA.x * box.pointC.y - box.pointC.x + box.pointA.y));
            double sumAreaTriangle = 0;
            double triangleArea;

            //triangle A point B
            triangleArea = Math.Abs( (point.x * box.pointA.y - box.pointA.x * point.y) + (box.pointB.x*point.y - point.x * box.pointB.y) + (box.pointA.x*box.pointB.y - box.pointB.x + box.pointA.y) ) / 2.0;
            sumAreaTriangle += triangleArea;

            //triangle B point C
            triangleArea = Math.Abs((point.x * box.pointB.y - box.pointB.x * point.y) + (box.pointC.x * point.y - point.x * box.pointC.y) + (box.pointB.x * box.pointC.y - box.pointC.x + box.pointB.y)) / 2.0;
            sumAreaTriangle += triangleArea;

            //triangle C point D
            triangleArea = Math.Abs((point.x * box.pointC.y - box.pointC.x * point.y) + (box.pointD.x * point.y - point.x * box.pointD.y) + (box.pointC.x * box.pointD.y - box.pointD.x + box.pointC.y)) / 2.0;
            sumAreaTriangle += triangleArea;

            //triangle D point A
            triangleArea = Math.Abs((point.x * box.pointD.y - box.pointD.x * point.y) + (box.pointA.x * point.y - point.x * box.pointA.y) + (box.pointD.x * box.pointA.y - box.pointA.x + box.pointD.y)) / 2.0;
            sumAreaTriangle += triangleArea;

            if (areaBox < sumAreaTriangle)
            {
                return false;
            }
            else
            {
                return true;
            }
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
            rotatedPoint.y = point.x * Math.Sin(angle) - point.y * Math.Cos(angle);

            return rotatedPoint;
        }

        static public HitBox CreateRectangleHitBox(Coordinate center, double angle, int width, int height )
        {
            HitBox rectangleHitBox = new HitBox
            {
                boxType = HitBox.Type.RECTANGLE,
            };

            Coordinate temp;
            //pointA
            temp.x = -width / 2;
            temp.y = height / 2;
            temp = Helper.RotatePoint(temp, angle);
            temp.x += center.x;
            temp.y += center.y;
            rectangleHitBox.pointA = temp;

            //pointB
            temp.x = width / 2;
            temp.y = height / 2;
            temp = Helper.RotatePoint(temp, angle);
            temp.x += center.x;
            temp.y += center.y;
            rectangleHitBox.pointB = temp;

            //pointC
            temp.x = width / 2;
            temp.y = -height / 2;
            temp = Helper.RotatePoint(temp, angle);
            temp.x += center.x;
            temp.y += center.y;
            rectangleHitBox.pointC = temp;

            //pointD
            temp.x = -width / 2;
            temp.y = -height / 2;
            temp = Helper.RotatePoint(temp, angle);
            temp.x += center.x;
            temp.y += center.y;
            rectangleHitBox.pointC = temp;

            return rectangleHitBox;
        }

    }
}
