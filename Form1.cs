﻿using System;
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
        Point[] points1, points2;
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
                List<Point> points = new List<Point>();
                foreach (string line in File.ReadLines(openFileDialog1.FileNames[i], Encoding.UTF8))
                {
                    string[] coords = line.Split(',');
                    Point point = new Point();
                    point.X = Convert.ToInt32(coords[0]);
                    point.Y = Convert.ToInt32(coords[1]);
                    points.Add(point);
                }
                if (i == 0)
                    points1 = points.ToArray();
                else
                    points2 = points.ToArray();
            }

            lines = GetLines(points1, points2);
            ComputeFromLinesAndFillGrid();
            chkPoints1FromFile.Enabled = true;
            //DataGridView1_CellClick(null, new DataGridViewCellEventArgs(1, 0));
        }

        private void open1FileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog3.ShowDialog() != DialogResult.OK)
                return;
            if (openFileDialog3.FileNames.Length != 1)
            {
                MessageBox.Show("1 file required");
                return;
            }

            List<Point> points = new List<Point>();
            foreach (string line in File.ReadLines(openFileDialog3.FileName, Encoding.UTF8))
            {
                string[] coords = line.Split(',');
                Point point = new Point();
                point.X = Convert.ToInt32(coords[0]);
                point.Y = Convert.ToInt32(coords[1]);
                points.Add(point);
            }
            points1 = points.ToArray();
            chkPoints1FromFile.Enabled = true;
            chkPoints1FromFile.Checked = true;
            nudPoints1.Value = points1.Length;
            nudPoints1.Enabled = false;
        }

        private void ComputeFromLinesAndFillGrid()
        {
            intersections = GetIntersections(lines, Convert.ToSingle(nudMinRatio.Value));
            intersectionGroups = GetIntersectionGroups(intersections, Convert.ToInt32(nudTolerance.Value));
            SetOddsControls();
            FillDataGrid1();
        }

        private Line[] GetLines(Point[] points1, Point[] points2)
        {
            int lineCount = points1.Length * points2.Length;
            Line[] lines = new Line[lineCount];
            int id = -1;

            for (int i = 0, len1 = points1.Length; i < len1; i++)
                for (int j = 0, len2 = points2.Length; j < len2; j++)
                    lines[++id] = new Line(points1[i], points2[j], id, i, j);

            return lines;
        }
        
        private List<Intersection> GetIntersections(Line[] lines, float minRatio)
        {
            List<Intersection> temp = new List<Intersection>();
            for (int i = 0, lineCount = lines.Length; i < lineCount - 1; i++)
            {
                for (int j = i + 1; j < lineCount; j++)
                {
                    if (lines[i].index1 == lines[j].index1 || lines[i].index2 == lines[j].index2)
                        continue;
                    Intersection intersection = new Intersection(lines[i], lines[j], minRatio);
                    if (intersection.Exists)
                    {
                        intersection.id = temp.Count;
                        temp.Add(intersection);
                    }
                }
            }
            return temp.OrderBy(i => i.Point.X).ToList<Intersection>();
        }

        private List<IntersectionGroup> GetIntersectionGroups(List<Intersection> intersections, int tolerance)
        {
            List<IntersectionGroup> intersectionGroups = new List<IntersectionGroup>();

            for (int i = 0, count = intersections.Count; i < count - 1; i++)
            {
                List<Intersection> temp = new List<Intersection>();
                temp.Add(intersections[i]);
                for (int j = i + 1; j < count; j++)
                {
                    if (intersections[j].Point.X - intersections[i].Point.X <= tolerance)
                        temp.Add(intersections[j]);
                    else
                        break;
                }
                if (temp.Count > 1)
                {
                    // sort temp by Y
                    // get groups from temp
                    GetGroups(temp.OrderBy(g => g.Point.Y).ToList<Intersection>(), intersectionGroups, tolerance);
                }
                while (i < count - 1 && intersections[i + 1].Point.X == intersections[i].Point.X)
                    ++i;
            }
            return intersectionGroups.OrderByDescending(g => g.Lines.Count).ToList<IntersectionGroup>();
        }

        private void GetGroups(List<Intersection> intersections, List<IntersectionGroup> groups, int tolerance)
        {
            for (int i = 0, count = intersections.Count; i < count - 1; i++)
            {
                IntersectionGroup group = new IntersectionGroup(intersections[i]);
                for (int j = i + 1; j < count; j++)
                {
                    if (intersections[j].Point.Y - intersections[i].Point.Y <= tolerance)
                        group.Add(intersections[j]);
                    else
                        break;
                }
                if (group.Lines.Count > 2)
                {
                    groups.Add(group);
                }
                while (i < count - 1 && intersections[i + 1].Point.Y == intersections[i].Point.Y)
                    ++i;
            }

        }
        private void SetOddsControls()
        {
            if (intersectionGroups.Count == 0)
                return;
            nudLines.Value = intersectionGroups[0].Lines.Count;
            nudPoints1.Value = points1.Length;
            nudPoints2.Value = points2.Length;
        }
        private void FillDataGrid1()
        {
            ResetDataGridView(1);
            DataTable table = new DataTable();
            table.Columns.Add("Lines", typeof(int));
            foreach (IntersectionGroup group in intersectionGroups)
            {
                DataRow row = table.NewRow();
                row["Lines"] = group.Lines.Count;
                table.Rows.Add(row);
            }
            table.AcceptChanges();
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.DataSource = table;
            dataGridView1.CellClick += DataGridView1_CellClick;
            DataGridView1_CellClick(null, new DataGridViewCellEventArgs(1, 0));
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
                    foreach (Line line in intersectionGroups[row].Lines)
                    {
                        gr.DrawLine(pen, line.Point1, line.Point2);
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
            foreach (Line line in intersectionGroups[row].Lines)
            {
                DataRow newRow = table.NewRow();
                newRow["Line ID"] = line.id;
                newRow["x1"] = line.Point1.X;
                newRow["y1"] = line.Point1.Y;
                newRow["x2"] = line.Point2.X;
                newRow["y2"] = line.Point2.Y;
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
            bitmap = new Bitmap(512, 512);
            pictureBox1.Image = bitmap;
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Point[] oPoints1 = null, oPoints2 = null;
            Line[] oLines = null;
            List<Intersection> oIntersections;
            List<IntersectionGroup> oIntersectionGroups;
            decimal[] args = (decimal[])e.Argument;
            int targetHits = Convert.ToInt32(args[0]);
            int lines = Convert.ToInt32(args[1]);
            int numPoints1 = Convert.ToInt32(args[2]);
            int numPoints2 = Convert.ToInt32(args[3]);
            int maxY = Convert.ToInt32(args[4]);
            int tolerance = Convert.ToInt32(args[5]);
            float minRatio = Convert.ToSingle(args[6]);
            bool points1FromFile = Convert.ToBoolean(args[7]);

            //!Thread safety issues
            if (points1FromFile)
            {
                int length = points1.Length;
                oPoints1 = new Point[length];
                for (int i = 0; i < length; i++)
                {
                    oPoints1[i].X = points1[i].X;
                }
            }

            // Seed the random number generator with the least significant bits of the date.
            long ticks = DateTime.Now.Ticks;
            ulong uticks = Convert.ToUInt64(ticks);
            for (int shift = 1; uticks > Int32.MaxValue; shift++)
            {
                uticks <<= shift;
                uticks >>= shift;
            }
            uint iticks = Convert.ToUInt32(uticks);
            Random rand = new Random(Convert.ToInt32(iticks));
            int[] progress = new int[2];

            // Performance is King
            if (!points1FromFile)
            {
                for (int trial = 1, hits = 0; hits < targetHits; trial++)
                {
                    if (worker.CancellationPending == true)
                    {
                        e.Cancel = true;
                        break;
                    }
                    else
                    {
                        oPoints1 = new Point[numPoints1];
                        for (int i = 0; i < numPoints1; i++)
                            oPoints1[i] = new Point(rand.Next(512), rand.Next(maxY));
                        oPoints2 = new Point[numPoints2];
                        for (int i = 0; i < numPoints2; i++)
                            oPoints2[i] = new Point(rand.Next(512), rand.Next(maxY));
                        oLines = GetLines(oPoints1, oPoints2);
                        oIntersections = GetIntersections(oLines, minRatio);
                        oIntersectionGroups = GetIntersectionGroups(oIntersections, tolerance);
                        for (int i = 0, j = oIntersectionGroups.Count; i < j; i++)
                        {
                            int count = oIntersectionGroups[i].Lines.Count;
                            if (count == lines)
                            {
                                ++hits;
                                progress[0] = trial;
                                progress[1] = hits;
                                worker.ReportProgress(0, progress);
                            }
                            else if (count < lines)
                                break;
                        }
                    }
                }
            }
            else
            {
                for (int trial = 1, hits = 0; hits < targetHits; trial++)
                {
                    if (worker.CancellationPending == true)
                    {
                        e.Cancel = true;
                        break;
                    }
                    else
                    {
                        oPoints2 = new Point[numPoints2];
                        for (int i = 0; i < numPoints2; i++)
                            oPoints2[i] = new Point(rand.Next(512), rand.Next(maxY));
                        oLines = GetLines(oPoints1, oPoints2);
                        oIntersections = GetIntersections(oLines, minRatio);
                        oIntersectionGroups = GetIntersectionGroups(oIntersections, tolerance);
                        for (int i = 0, j = oIntersectionGroups.Count; i < j; i++)
                        {
                            int count = oIntersectionGroups[i].Lines.Count;
                            if (count == lines)
                            {
                                ++hits;
                                progress[0] = trial;
                                progress[1] = hits;
                                worker.ReportProgress(0, progress);
                            }
                            else if (count < lines)
                                break;
                        }
                    }
                }
            }
        }
        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int[] progress = (int[])e.UserState;
            int trial = progress[0];
            int hits = progress[1];
            double dTrial = trial, dHits = hits;
            double pct = 100 * dHits / dTrial;
            double trialsPerHit = hits == 0 ? 0 : dTrial / dHits;
            lblOdds.Text = string.Format("Odds: {0} / {1} = 1 / {2} = {3}%", hits, trial, trialsPerHit.ToString("0"), pct.ToString("0.00000"));
        }

        private void btnOdds_Click(object sender, EventArgs e)
        {
            if (worker.IsBusy != true)
            {
                int targetHits = Convert.ToInt32(nudHits.Value);
                int lines = Convert.ToInt32(nudLines.Value);
                int numPoints1 = Convert.ToInt32(nudPoints1.Value);
                int numPoints2 = Convert.ToInt32(nudPoints2.Value);
                int maxY = 512;
                decimal[] args = new decimal[8];
                args[0] = targetHits;
                args[1] = lines;
                args[2] = numPoints1;
                args[3] = numPoints2;
                args[4] = maxY;
                args[5] = nudTolerance.Value;
                args[6] = nudMinRatio.Value;
                args[7] = chkPoints1FromFile.Checked ? 1 : 0;

                btnOdds.Enabled = false;
                btnCancel.Enabled = true;
                lblOdds.Text = "Odds:";
                worker.RunWorkerAsync(args);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            worker.CancelAsync();
            btnOdds.Enabled = true;
            btnCancel.Enabled = false;
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnOdds.Enabled = true;
            btnCancel.Enabled = false;
        }

        private void nudTolerance_ValueChanged(object sender, EventArgs e)
        {
            if (lines != null)
                ComputeFromLinesAndFillGrid();
        }

        private void saveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveFileDialog1.FileName;
                if (!fileName.EndsWith(".png"))
                    fileName = string.Format("{0}{1}", fileName, ".png");
                pictureBox1.Image.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);
            }

        }

        private void nudMinRatio_ValueChanged(object sender, EventArgs e)
        {
            if (lines != null)
                ComputeFromLinesAndFillGrid();
        }
    }
}
