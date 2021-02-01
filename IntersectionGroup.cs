using System;
using System.Drawing;
using System.Collections.Generic;

namespace wmap_analysis
{
    class IntersectionGroup
    {
        public Point Intersection { get; }
        public List<Line> Lines { get; }


        public IntersectionGroup (Intersection i)
        {
            this.Intersection = new Point(i.Point.X, i.Point.Y);
            Lines = new List<Line>();
        }

        public void Add(Intersection i)
        {
            if (!i.Point.Equals(this.Intersection))
                return;

            bool addLine1 = true;
            bool addLine2 = true;
            foreach (Line line in Lines)
            {
                if (line.Point1.Equals(i.Line1.Point1) ||
                    line.Point1.Equals(i.Line1.Point2) ||
                    line.Point2.Equals(i.Line1.Point1) ||
                    line.Point2.Equals(i.Line1.Point2)
                   )
                {
                    addLine1 = false;
                }
                if (line.Point1.Equals(i.Line2.Point1) ||
                    line.Point1.Equals(i.Line2.Point2) ||
                    line.Point2.Equals(i.Line2.Point1) ||
                    line.Point2.Equals(i.Line2.Point2)
                   )
                {
                    addLine2 = false;
                }
            }
            if (addLine1)
                Lines.Add(i.Line1);
            if (addLine2)
                Lines.Add(i.Line2);
        }
    }
}
