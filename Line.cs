using System;
using System.Drawing;

namespace wmap_analysis
{
    public class Line
    {
        public int id { get; }
        public int index1 { get; }   // index of first point to form line
        public int index2 { get; }   // index of second point to form line
        public PointF Point1 { get; }
        public PointF Point2 { get; }

        public Line(PointF point1, PointF point2, int id, int index1, int index2)
        {
            this.Point1 = point1;
            this.Point2 = point2;
            this.id = id;
            this.index1 = index1;
            this.index2 = index2;
        }

        public float Length()
        {
            return Convert.ToSingle(Math.Sqrt(Math.Pow(Point2.X - Point1.X, 2) + Math.Pow(Point2.Y - Point1.Y, 2)));
        }
    }
}
