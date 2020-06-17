using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace wmap_analysis
{
    public partial class Form1 : Form
    {
        PointF[] points1, points2;
        Line[] lines;
        List<Intersection> intersections;
        Dictionary<int, List<Intersection>> multiples = new Dictionary<int, List<Intersection>>();
        Bitmap bitmap = new Bitmap(512, 512);

        public Form1()
        {
            InitializeComponent();
        }

        private void OpenFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;
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

            GetIntersections();
        }

        private void GetIntersections()
        {
            int lineCount = points1.Length * points2.Length;
            intersections = new List<Intersection>();
            for (int i = 0; i < lineCount - 1; i++)
            {
                for (int j = i + 1; j < lineCount; j++)
                {
                    Intersection intersection = new Intersection(lines[i], lines[j], (float)0.1);
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

            for (int i = 0, j; i < count; i += j)
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
            FillDataGrids();
        }

        private void FillDataGrids()
        {
            ResetDataGridView();
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

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int multiple = (int)dataGridView1[0, e.RowIndex].Value;
            int count = (int)dataGridView1[1, e.RowIndex].Value;
            Bitmap bmp = (Bitmap)bitmap.Clone();
            using (Graphics gr = Graphics.FromImage(bmp))
            {
                using (Pen pen = cbLineColor.SelectedIndex == 0 ?  new Pen(Color.Black, 1) : new Pen(Color.White, 1))
                {
                    foreach (Intersection I in multiples[multiple])
                    {
                        gr.DrawLine(pen, I.Line1.Point1, I.Line1.Point2);
                        gr.DrawLine(pen, I.Line2.Point1, I.Line2.Point2);
                    }
                }
            }
            pictureBox1.Image = bmp;
            if (count == multiple)
            {
                Intersection I = multiples[multiple][0];
                lblIntersection.Text = string.Format("Intersection at ({0}, {1})", I.Point.X, I.Point.Y);
            }
            else
                lblIntersection.Text = string.Empty;
        }

        private void ResetDataGridView()
        {
            dataGridView1.CancelEdit();
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = null;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 512; i++)
                for (int j = 0; j < 512; j++)
                    bitmap.SetPixel(i, j, Color.White);
            cbLineColor.SelectedIndex = 0;
        }

        private void loadImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                Image image = Image.FromFile(openFileDialog2.FileName);
                bitmap = new Bitmap(image);
                pictureBox1.Image = bitmap;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = bitmap;
        }

        private bool SamePoints (Point p1, Point p2)
        {
            return (p1.X == p2.X && p1.Y == p2.Y);
        }
    }
}
