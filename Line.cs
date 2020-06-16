using System;
using System.Drawing;

namespace wmap_analysis
{
    public class Line
    {
        public PointF Point1 { get; }
        public PointF Point2 { get; }

        public Line(PointF point1, PointF point2)
        {
            this.Point1 = point1;
            this.Point2 = point2;
        }

        public float Length()
        {
            return Convert.ToSingle(Math.Sqrt(Math.Pow(Point2.X - Point1.X, 2) + Math.Pow(Point2.Y - Point1.Y, 2)));
        }
    }
}
