using System;
using System.Drawing;

namespace wmap_analysis
{
    public class Intersection
    {
        public int id { get; set; }
        public Line Line1 { get; }
        public Line Line2 { get; }
        public Point Point { get; }
        public bool Exists { get; }

        public Intersection(Line line1, Line line2, float minRatio)
        {
            this.Line1 = line1;
            this.Line2 = line2;

            //https://en.wikipedia.org/wiki/Line–line_intersection
            float x1 = Line1.Point1.X;
            float y1 = Line1.Point1.Y;
            float x2 = Line1.Point2.X;
            float y2 = Line1.Point2.Y;

            float x3 = Line2.Point1.X;
            float y3 = Line2.Point1.Y;
            float x4 = Line2.Point2.X;
            float y4 = Line2.Point2.Y;

            float denominator = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4);
            if (denominator == 0.0)
            {
                Exists = false;
                return;
            }

            float t = ((x1 - x3) * (y3 - y4) - (y1 - y3) * (x3 - x4)) / denominator;
            if (t < 0 || t > 1)
            {
                Exists = false;
                return;
            }

            float u = -((x1 - x2) * (y1 - y3) - (y1 - y2) * (x1 - x3)) / denominator;
            if (u < 0 || u > 1)
            {
                Exists = false;
                return;
            }

            Exists = true;
            Point intersection = new Point();
            intersection.X = (int)Math.Round(x1 + t * (x2 - x1));
            intersection.Y = (int)Math.Round(y1 + t * (y2 - y1));
            this.Point = intersection;

            if (minRatio == 0.0)
                return;

            float distanceToIntersection = Convert.ToSingle(Math.Sqrt(Math.Pow(x1 - intersection.X, 2) + Math.Pow(y1 - intersection.Y, 2)));
            float ratio = distanceToIntersection / Line1.Length();
            if (ratio < minRatio || ratio > 1 - minRatio)
            {
                Exists = false;
                return;
            }
            distanceToIntersection = Convert.ToSingle(Math.Sqrt(Math.Pow(x3 - intersection.X, 2) + Math.Pow(y3 - intersection.Y, 2)));
            ratio = distanceToIntersection / Line2.Length();
            if (ratio < minRatio || ratio > 1 - minRatio)
                Exists = false;
        }

        public bool Equals(Intersection i)
        {
            return this.Point.X == i.Point.X && this.Point.Y == i.Point.Y;
        }
    }
}
