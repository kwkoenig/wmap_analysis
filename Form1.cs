using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wmap_analysis
{
    public partial class Form1 : Form
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
        }

        public class Intersection
        {
            public Line Line1 { get; }
            public Line Line2 { get; }
            public Point Point { get; }
            public bool Exists { get; }

            public Intersection(Line line1, Line line2)
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
                if (t <= 0.0 || t >= 1.0)
                {
                    Exists = false;
                    return;
                }

                float u = -((x1 - x2) * (y1 - y3) - (y1 - y2) * (x1 - x3)) / denominator;
                if (u <= 0.0 || u >= 1.0)
                {
                    Exists = false;
                    return;
                }

                Exists = true;
                Point intersection = new Point();
                intersection.X = (int)Math.Round(x1 + t * (x2 - x1));
                intersection.Y = (int)Math.Round(y1 + t * (y2 - y1));
                this.Point = intersection;
            }
        }

        PointF[] points1, points2;
        Line[] lines;
        List<Intersection> intersections;
        List<Intersection> duplicates;
        Dictionary<int, List<Intersection>> multiples = new Dictionary<int, List<Intersection>>();

        public Form1()
        {
            InitializeComponent();
        }

        private void OpenFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (openFileDialog1.FileNames.Length != 2)
                {
                    MessageBox.Show("2 files required");
                    return;
                }
                for (int i = 0; i < 2; i++)
                {
                    List<PointF> points = new List<PointF>();
                    foreach (string line in File.ReadLines(openFileDialog1.FileNames[i], Encoding.UTF8))
                    {
                        string[] coords = line.Split(',');
                        PointF point = new PointF();
                        point.X = Convert.ToSingle(coords[0]);
                        point.Y = Convert.ToSingle(coords[1]);
                        points.Add(point);
                    }
                    if (i == 0)
                        points1 = points.ToArray();
                    else
                        points2 = points.ToArray();
                }
                int lineCount = points1.Length * points2.Length;
                lines = new Line[lineCount];
                int l = 0;
                foreach (PointF point1 in points1)
                    foreach (PointF point2 in points2)
                        lines[l++] = new Line(point1, point2);

                intersections = new List<Intersection>();
                for (int i = 0; i < lineCount - 1; i++)
                {
                    for (int j = i + 1; j < lineCount; j++)
                    {
                        Intersection intersection = new Intersection(lines[i], lines[j]);
                        if (intersection.Exists)
                            intersections.Add(intersection);
                    }
                }
                intersections.Sort((a, b) =>
                {
                    int ret = a.Point.X.CompareTo(b.Point.X);
                    if (ret == 0) ret = a.Point.Y.CompareTo(b.Point.Y);
                    return ret;
                });
                int count = intersections.Count;
                lblIntersectionCount.Text = "Initial Intersections: " + count.ToString();

                for (int i = 0, j; i < count; i +=j)
                {
                    for (j = 1; i + j < count; j++)
                    {
                        if (!SamePoints(intersections[i].Point, intersections[i + j].Point))
                            break;
                    }
                    if (j > 1)
                    {
                        if (!multiples.ContainsKey(j))
                            multiples.Add(j, new List<Intersection>());
                        for (int k = i; k < i + j; k++)
                            multiples[j].Add(intersections[k]);
                    }
                }

                DataTable table = new DataTable();
                table.Columns.Add("Multiple", typeof(int));
                table.Columns.Add("Count", typeof(int));
                foreach (KeyValuePair<int, List<Intersection>> multiple in multiples)
                {
                    DataRow row = table.NewRow();
                    row["Multiple"] = multiple.Key;
                    row["Count"] = multiple.Value.Count;
                    table.Rows.Add(row);
                }
                table.AcceptChanges();
                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.DataSource = table;
                dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Descending);
                DataGridViewButtonColumn btnCol = new DataGridViewButtonColumn();
                btnCol.Text = "Draw";
                btnCol.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Add(btnCol);
                dataGridView1.CellClick += DataGridView1_CellClick;
            }
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int multiple = (int)dataGridView1[0, e.RowIndex].Value;
        }

        private bool SamePoints (Point p1, Point p2)
        {
            return (p1.X == p2.X && p1.Y == p2.Y);
        }
    }
}
