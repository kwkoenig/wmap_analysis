namespace wmap_analysis
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblIntersection = new System.Windows.Forms.Label();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.btnReset = new System.Windows.Forms.Button();
            this.cbLineColor = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nudMinRatio = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.nudTolerance = new System.Windows.Forms.NumericUpDown();
            this.nudLines = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.nudPoints1 = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.nudPoints2 = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.cbImageSize = new System.Windows.Forms.ComboBox();
            this.btnOdds = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.nudHits = new System.Windows.Forms.NumericUpDown();
            this.lblOdds = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.worker = new System.ComponentModel.BackgroundWorker();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinRatio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTolerance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLines)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPoints1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPoints2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHits)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "txt";
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "txt files (*.txt)|*.txt";
            this.openFileDialog1.Multiselect = true;
            this.openFileDialog1.RestoreDirectory = true;
            this.openFileDialog1.Title = "Browse For 2 Text Files";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(981, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFilesToolStripMenuItem,
            this.loadImageToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openFilesToolStripMenuItem
            // 
            this.openFilesToolStripMenuItem.Name = "openFilesToolStripMenuItem";
            this.openFilesToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.openFilesToolStripMenuItem.Text = "Open Files";
            this.openFilesToolStripMenuItem.Click += new System.EventHandler(this.OpenFilesToolStripMenuItem_Click);
            // 
            // loadImageToolStripMenuItem
            // 
            this.loadImageToolStripMenuItem.Name = "loadImageToolStripMenuItem";
            this.loadImageToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.loadImageToolStripMenuItem.Text = "Load Image";
            this.loadImageToolStripMenuItem.Click += new System.EventHandler(this.loadImageToolStripMenuItem_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 112);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(122, 457);
            this.dataGridView1.TabIndex = 3;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(462, 57);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(512, 512);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // lblIntersection
            // 
            this.lblIntersection.AutoSize = true;
            this.lblIntersection.Location = new System.Drawing.Point(462, 38);
            this.lblIntersection.Name = "lblIntersection";
            this.lblIntersection.Size = new System.Drawing.Size(0, 13);
            this.lblIntersection.TabIndex = 12;
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog2";
            this.openFileDialog2.Filter = "Image files|*.bmp;*.jpg;*.gif;*.png;*.tif|All files|*.*";
            this.openFileDialog2.Title = "Browse for Image File";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(898, 32);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 21);
            this.btnReset.TabIndex = 13;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // cbLineColor
            // 
            this.cbLineColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLineColor.FormattingEnabled = true;
            this.cbLineColor.Items.AddRange(new object[] {
            "Black",
            "White"});
            this.cbLineColor.Location = new System.Drawing.Point(823, 32);
            this.cbLineColor.Name = "cbLineColor";
            this.cbLineColor.Size = new System.Drawing.Size(57, 21);
            this.cbLineColor.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(784, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Lines:";
            // 
            // nudMinRatio
            // 
            this.nudMinRatio.DecimalPlaces = 2;
            this.nudMinRatio.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudMinRatio.Location = new System.Drawing.Point(72, 86);
            this.nudMinRatio.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            131072});
            this.nudMinRatio.Name = "nudMinRatio";
            this.nudMinRatio.Size = new System.Drawing.Size(51, 20);
            this.nudMinRatio.TabIndex = 16;
            this.nudMinRatio.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudMinRatio.ValueChanged += new System.EventHandler(this.nudMinRatio_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Min Ratio:";
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(140, 58);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(314, 510);
            this.dataGridView2.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Tolerance:";
            // 
            // nudTolerance
            // 
            this.nudTolerance.Location = new System.Drawing.Point(72, 57);
            this.nudTolerance.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudTolerance.Name = "nudTolerance";
            this.nudTolerance.Size = new System.Drawing.Size(51, 20);
            this.nudTolerance.TabIndex = 19;
            // 
            // nudLines
            // 
            this.nudLines.Location = new System.Drawing.Point(284, 595);
            this.nudLines.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudLines.Name = "nudLines";
            this.nudLines.Size = new System.Drawing.Size(51, 20);
            this.nudLines.TabIndex = 22;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(248, 596);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "Lines:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(358, 596);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 25;
            this.label6.Text = "Points 1:";
            // 
            // nudPoints1
            // 
            this.nudPoints1.Location = new System.Drawing.Point(407, 594);
            this.nudPoints1.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudPoints1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPoints1.Name = "nudPoints1";
            this.nudPoints1.Size = new System.Drawing.Size(51, 20);
            this.nudPoints1.TabIndex = 24;
            this.nudPoints1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(483, 596);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 13);
            this.label7.TabIndex = 27;
            this.label7.Text = "Points 2:";
            // 
            // nudPoints2
            // 
            this.nudPoints2.Location = new System.Drawing.Point(534, 594);
            this.nudPoints2.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudPoints2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPoints2.Name = "nudPoints2";
            this.nudPoints2.Size = new System.Drawing.Size(51, 20);
            this.nudPoints2.TabIndex = 26;
            this.nudPoints2.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(607, 596);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 13);
            this.label8.TabIndex = 28;
            this.label8.Text = "Image Size:";
            // 
            // cbImageSize
            // 
            this.cbImageSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbImageSize.FormattingEnabled = true;
            this.cbImageSize.Items.AddRange(new object[] {
            "256 X 512",
            "512 X 512"});
            this.cbImageSize.Location = new System.Drawing.Point(671, 592);
            this.cbImageSize.Name = "cbImageSize";
            this.cbImageSize.Size = new System.Drawing.Size(76, 21);
            this.cbImageSize.TabIndex = 29;
            // 
            // btnOdds
            // 
            this.btnOdds.Location = new System.Drawing.Point(13, 592);
            this.btnOdds.Name = "btnOdds";
            this.btnOdds.Size = new System.Drawing.Size(56, 23);
            this.btnOdds.TabIndex = 30;
            this.btnOdds.Text = "Odds";
            this.btnOdds.UseVisualStyleBackColor = true;
            this.btnOdds.Click += new System.EventHandler(this.btnOdds_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(148, 596);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 13);
            this.label4.TabIndex = 32;
            this.label4.Text = "Hits:";
            // 
            // nudHits
            // 
            this.nudHits.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudHits.Location = new System.Drawing.Point(178, 595);
            this.nudHits.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudHits.Name = "nudHits";
            this.nudHits.Size = new System.Drawing.Size(51, 20);
            this.nudHits.TabIndex = 31;
            this.nudHits.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // lblOdds
            // 
            this.lblOdds.AutoSize = true;
            this.lblOdds.Location = new System.Drawing.Point(775, 595);
            this.lblOdds.Name = "lblOdds";
            this.lblOdds.Size = new System.Drawing.Size(35, 13);
            this.lblOdds.TabIndex = 33;
            this.lblOdds.Text = "Odds:";
            // 
            // btnCancel
            // 
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new System.Drawing.Point(78, 592);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(56, 23);
            this.btnCancel.TabIndex = 34;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // worker
            // 
            this.worker.WorkerReportsProgress = true;
            this.worker.WorkerSupportsCancellation = true;
            this.worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.worker_DoWork);
            this.worker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.worker_ProgressChanged);
            this.worker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.worker_RunWorkerCompleted);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(981, 622);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblOdds);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.nudHits);
            this.Controls.Add(this.btnOdds);
            this.Controls.Add(this.cbImageSize);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.nudPoints2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.nudPoints1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.nudLines);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nudTolerance);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nudMinRatio);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbLineColor);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.lblIntersection);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "WMAP Analysis";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinRatio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTolerance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLines)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPoints1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPoints2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHits)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFilesToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblIntersection;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.ToolStripMenuItem loadImageToolStripMenuItem;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.ComboBox cbLineColor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudMinRatio;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudTolerance;
        private System.Windows.Forms.NumericUpDown nudLines;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nudPoints1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nudPoints2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbImageSize;
        private System.Windows.Forms.Button btnOdds;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudHits;
        private System.Windows.Forms.Label lblOdds;
        private System.Windows.Forms.Button btnCancel;
        private System.ComponentModel.BackgroundWorker worker;
    }
}

