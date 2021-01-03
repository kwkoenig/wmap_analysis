using System;
using System.Drawing;
using System.Collections.Generic;

namespace wmap_analysis
{
    class IntersectionGroup
    {
        public Point Intersection { get; }
        public List<int> LineIds { get; }

        private List<Point> points;


        public IntersectionGroup (Intersection i)
        {
            this.Intersection = new Point(i.Point.X, i.Point.Y);
            LineIds = new List<int>();
            LineIds.Add(i.Line1.id);
            LineIds.Add(i.Line2.id);
            points = new List<Point>();
        }

        public void Add(Intersection i)
        {
            if (points.Contains(i.Line1.Point1) || points.Contains(i.Line1.Point2) || points.Contains(i.Line2.Point1) || points.Contains(i.Line2.Point2))
                return;
            if (!LineIds.Contains(i.Line1.id))
            {
                LineIds.Add(i.Line1.id);
                points.Add(i.Line1.Point1);
                points.Add(i.Line1.Point2);
            }
            if (!LineIds.Contains(i.Line2.id))
            {
                LineIds.Add(i.Line2.id);
                points.Add(i.Line2.Point1);
                points.Add(i.Line2.Point2);
            }
        }
    }
}
