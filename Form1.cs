using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace wmap_analysis
{
    public partial class Form1 : Form
    {
        PointF[] points1, points2;
        Line[] lines = null;
        List<Intersection> intersections;
        List<IntersectionGroup> intersectionGroups;
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
            int id = -1;

            for (int i = 0, len1 = points1.Length; i < len1; i++)
                for (int j = 0, len2 = points2.Length; j < len2; j++)
                    lines[++id] = new Line(points1[i], points2[j], id, i, j);

            GetIntersections();
            GetIntersectionGroups();
            FillDataGrid1();
        }

        private void GetIntersectionGroups()
        {
            intersectionGroups = new List<IntersectionGroup>();
            IntersectionGroup group = null;
            int tolerance = Convert.ToInt32(nudTolerance.Value);

            int count = intersections.Count;
            lblIntersectionCount.Text = "Initial Intersections: " + count.ToString();

            for (int i = 0; i < count - 1; i++)
            {
                for (int j = i + 1; j < count; j++)
                {
                    if (!intersections[i].Equals(intersections[j], tolerance))
                    {
                        i = j;
                        break;
                    }
                    if (j == i + 1)
                    {
                        group = new IntersectionGroup(intersections[i]);
                        intersectionGroups.Add(group);
                    }
                    group.Add(intersections[j]);
                }
            }
            intersectionGroups = intersectionGroups.OrderByDescending(g => g.LineIds.Count).ToList<IntersectionGroup>();
        }
        private void GetIntersections()
        {
            List<Intersection> temp = new List<Intersection>();
            for (int i = 0, lineCount = lines.Length; i < lineCount - 1; i++)
            {
                for (int j = i + 1; j < lineCount; j++)
                {
                    if (lines[i].index1 == lines[j].index1 || lines[i].index2 == lines[j].index2)
                        continue;
                    Intersection intersection = new Intersection(lines[i], lines[j], Convert.ToSingle(nudMinRatio.Value));
                    if (intersection.Exists)
                    {
                        intersection.id = temp.Count;
                        temp.Add(intersection);
                    }
                }
            }
            intersections = temp.OrderBy(i => i.Point.X).ThenBy(i => i.Point.Y).ToList<Intersection>();
        }

        private void FillDataGrid1()
        {
            ResetDataGridView(1);
            DataTable table = new DataTable();
            table.Columns.Add("Lines", typeof(int));
            foreach (IntersectionGroup group in intersectionGroups)
            {
                DataRow row = table.NewRow();
                row["Lines"] = group.LineIds.Count;
                table.Rows.Add(row);
            }
            table.AcceptChanges();
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.DataSource = table;
            dataGridView1.CellClick += DataGridView1_CellClick;

        }

        private void DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != 5)
                return;
            Bitmap bmp = (Bitmap)bitmap.Clone();
            using (Graphics gr = Graphics.FromImage(bmp))
            {
                using (Pen pen = cbLineColor.SelectedIndex == 0 ? new Pen(Color.Black, 1) : new Pen(Color.White, 1))
                {
                    foreach (DataGridViewRow r in dataGridView2.Rows)
                    {
                        bool drawLine = false;
                        bool isChecked = (bool)r.Cells[5].Value;
                        if (r.Index == e.RowIndex)
                        {
                            dataGridView2.CurrentCell = dataGridView2.Rows[e.RowIndex].Cells[0];
                            if (!isChecked)
                            {
                                drawLine = true;
                            }
                        }
                        else if (isChecked)
                        {
                            drawLine = true;
                        }
                        if (drawLine)
                        {
                            int lineId = (int)r.Cells[0].Value;
                            gr.DrawLine(pen, lines[lineId].Point1, lines[lineId].Point2);
                        }
                    }
                }
            }
            pictureBox1.Image = bmp;
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            if (row < 0)
                return;
            Bitmap bmp = (Bitmap)bitmap.Clone();
            using (Graphics gr = Graphics.FromImage(bmp))
            {
                using (Pen pen = cbLineColor.SelectedIndex == 0 ? new Pen(Color.Black, 1) : new Pen(Color.White, 1))
                {
                    foreach (int i in intersectionGroups[row].LineIds)
                    {
                        gr.DrawLine(pen, lines[i].Point1, lines[i].Point2);
                    }
                }
            }
            pictureBox1.Image = bmp;
            lblIntersection.Text = string.Format("Intersection at ({0}, {1})", intersectionGroups[row].Intersection.X, intersectionGroups[row].Intersection.Y);

            ResetDataGridView(2);
            DataTable table = new DataTable();
            table.Columns.Add("Line ID", typeof(int));
            table.Columns.Add("x1", typeof(int));
            table.Columns.Add("y1", typeof(int));
            table.Columns.Add("x2", typeof(int));
            table.Columns.Add("y2", typeof(int));
            table.AcceptChanges();
            foreach (int i in intersectionGroups[row].LineIds)
            {
                DataRow newRow = table.NewRow();
                newRow["Line ID"] = i;
                newRow["x1"] = lines[i].Point1.X;
                newRow["y1"] = lines[i].Point1.Y;
                newRow["x2"] = lines[i].Point2.X;
                newRow["y2"] = lines[i].Point2.Y;
                table.Rows.Add(newRow);
            }
            table.AcceptChanges();
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView2.AllowUserToAddRows = false;
            DataView view = table.DefaultView;
            view.Sort = "Line ID";
            dataGridView2.DataSource = view;

            DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
            chk.HeaderText = "Draw";
            chk.Name = "chk";
            chk.TrueValue = true;
            chk.FalseValue = false;
            dataGridView2.Columns.Add(chk);

            foreach (DataGridViewRow r in dataGridView2.Rows)
            {
                r.Cells[5].Value = true;
            }

            dataGridView2.CellContentClick -= DataGridView2_CellClick;
            dataGridView2.CellContentClick += DataGridView2_CellClick;
        }


        private void ResetDataGridView(int id)
        {
            DataGridView dataGridView = id == 1 ? dataGridView1 : dataGridView2;
            dataGridView.CancelEdit();
            dataGridView.Columns.Clear();
            dataGridView.DataSource = null;
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

        private void nudMinRatio_ValueChanged(object sender, EventArgs e)
        {
            if (lines != null)
                GetIntersections();
        }
    }
}
