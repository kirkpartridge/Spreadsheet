namespace SpreadsheetGUI
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.spreadsheetPanel1 = new SS.SpreadsheetPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cellNameBox = new System.Windows.Forms.TextBox();
            this.cellValueBox = new System.Windows.Forms.TextBox();
            this.cellNameLabel = new System.Windows.Forms.Label();
            this.cellContentsLabel = new System.Windows.Forms.Label();
            this.cellContentsBox = new System.Windows.Forms.TextBox();
            this.cellValueLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.graphButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.graph = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.graph)).BeginInit();
            this.SuspendLayout();
            // 
            // spreadsheetPanel1
            // 
            this.spreadsheetPanel1.AutoSize = true;
            this.spreadsheetPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.spreadsheetPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spreadsheetPanel1.Location = new System.Drawing.Point(0, 0);
            this.spreadsheetPanel1.Name = "spreadsheetPanel1";
            this.spreadsheetPanel1.Size = new System.Drawing.Size(1953, 1040);
            this.spreadsheetPanel1.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1953, 40);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(64, 36);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(149, 36);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click_1);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(149, 36);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(149, 36);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(149, 36);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click_1);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(77, 36);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // cellNameBox
            // 
            this.cellNameBox.Location = new System.Drawing.Point(138, 20);
            this.cellNameBox.Name = "cellNameBox";
            this.cellNameBox.ReadOnly = true;
            this.cellNameBox.Size = new System.Drawing.Size(63, 31);
            this.cellNameBox.TabIndex = 2;
            // 
            // cellValueBox
            // 
            this.cellValueBox.Location = new System.Drawing.Point(788, 20);
            this.cellValueBox.Name = "cellValueBox";
            this.cellValueBox.ReadOnly = true;
            this.cellValueBox.Size = new System.Drawing.Size(162, 31);
            this.cellValueBox.TabIndex = 3;
            // 
            // cellNameLabel
            // 
            this.cellNameLabel.AutoSize = true;
            this.cellNameLabel.Location = new System.Drawing.Point(12, 26);
            this.cellNameLabel.Name = "cellNameLabel";
            this.cellNameLabel.Size = new System.Drawing.Size(111, 25);
            this.cellNameLabel.TabIndex = 4;
            this.cellNameLabel.Text = "Cell Name";
            // 
            // cellContentsLabel
            // 
            this.cellContentsLabel.AutoSize = true;
            this.cellContentsLabel.Location = new System.Drawing.Point(220, 26);
            this.cellContentsLabel.Name = "cellContentsLabel";
            this.cellContentsLabel.Size = new System.Drawing.Size(141, 25);
            this.cellContentsLabel.TabIndex = 5;
            this.cellContentsLabel.Text = "Cell Contents";
            // 
            // cellContentsBox
            // 
            this.cellContentsBox.Location = new System.Drawing.Point(367, 20);
            this.cellContentsBox.Name = "cellContentsBox";
            this.cellContentsBox.Size = new System.Drawing.Size(287, 31);
            this.cellContentsBox.TabIndex = 6;
            // 
            // cellValueLabel
            // 
            this.cellValueLabel.AutoSize = true;
            this.cellValueLabel.Location = new System.Drawing.Point(672, 26);
            this.cellValueLabel.Name = "cellValueLabel";
            this.cellValueLabel.Size = new System.Drawing.Size(110, 25);
            this.cellValueLabel.TabIndex = 7;
            this.cellValueLabel.Text = "Cell Value";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Info;
            this.panel1.Controls.Add(this.graphButton);
            this.panel1.Controls.Add(this.cellContentsLabel);
            this.panel1.Controls.Add(this.cellValueBox);
            this.panel1.Controls.Add(this.cellValueLabel);
            this.panel1.Controls.Add(this.cellContentsBox);
            this.panel1.Controls.Add(this.cellNameLabel);
            this.panel1.Controls.Add(this.cellNameBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1953, 63);
            this.panel1.TabIndex = 8;
            // 
            // graphButton
            // 
            this.graphButton.Location = new System.Drawing.Point(991, 15);
            this.graphButton.Name = "graphButton";
            this.graphButton.Size = new System.Drawing.Size(190, 40);
            this.graphButton.TabIndex = 8;
            this.graphButton.Text = "Create BarGraph";
            this.graphButton.UseVisualStyleBackColor = true;
            this.graphButton.Click += new System.EventHandler(this.graphButton_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.graph);
            this.panel2.Controls.Add(this.spreadsheetPanel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 103);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1953, 1040);
            this.panel2.TabIndex = 9;
            // 
            // graph
            // 
            this.graph.AllowDrop = true;
            chartArea1.Name = "ChartArea1";
            this.graph.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.graph.Legends.Add(legend1);
            this.graph.Location = new System.Drawing.Point(439, 85);
            this.graph.Name = "graph";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.graph.Series.Add(series1);
            this.graph.Size = new System.Drawing.Size(689, 503);
            this.graph.TabIndex = 8;
            this.graph.Text = "chart1";
            this.graph.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1953, 1143);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.graph)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SS.SpreadsheetPanel spreadsheetPanel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.TextBox cellNameBox;
        private System.Windows.Forms.TextBox cellValueBox;
        private System.Windows.Forms.Label cellNameLabel;
        private System.Windows.Forms.Label cellContentsLabel;
        private System.Windows.Forms.TextBox cellContentsBox;
        private System.Windows.Forms.Label cellValueLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.DataVisualization.Charting.Chart graph;
        private System.Windows.Forms.Button graphButton;
    }
}

