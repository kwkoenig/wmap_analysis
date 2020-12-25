using System;
using System.Drawing;
using System.Collections.Generic;

namespace wmap_analysis
{
    class IntersectionGroup
    {
        public Point Intersection { get; }
        public List<int> LineIds { get; }

        public IntersectionGroup (Intersection i)
        {
            this.Intersection = new Point(i.Point.X, i.Point.Y);
            LineIds = new List<int>();
            LineIds.Add(i.Line1.id);
            LineIds.Add(i.Line2.id);
        }

        public void Add(Intersection i)
        {
            if (!LineIds.Contains(i.Line1.id))
                LineIds.Add(i.Line1.id);
            if (!LineIds.Contains(i.Line2.id))
                LineIds.Add(i.Line2.id);
        }
    }
}
