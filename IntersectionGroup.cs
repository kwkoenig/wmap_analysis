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
            Lines.Add(i.Line1);
            Lines.Add(i.Line2);
        }

        public void Add (Line newLine)
        {
            foreach (Line line in Lines)
            {
                if (line.Point1.Equals(newLine.Point1) ||
                    line.Point1.Equals(newLine.Point2) ||
                    line.Point2.Equals(newLine.Point1) ||
                    line.Point2.Equals(newLine.Point2))
                    return;
            }
            Lines.Add(newLine);
        }

        public void Add(Intersection i)
        {
            this.Add(i.Line1);
            this.Add(i.Line2);
        }
    }
}
