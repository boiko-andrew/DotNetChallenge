using System;
using System.Windows;

namespace ModularCircle
{
    class MyCirclePointsCalculator
    {
        public static Point[] CalcCirclePoints(int circlePointCount, double radius, double x, double y)
        {
            Point[] points = new Point[circlePointCount];
            double alpha;

            for (int i = 0; i < circlePointCount; ++i)
            {
                circlePointCount = circlePointCount != 0 ? circlePointCount : 200;
                alpha = 2 * Math.PI / circlePointCount * i;
                points[i].X = x + Math.Cos(alpha) * radius;
                points[i].Y = y + Math.Sin(alpha) * radius;
            }
            return points;
        }
    }
}
