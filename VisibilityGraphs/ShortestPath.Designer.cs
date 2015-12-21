namespace VisibilityGraphs
{
    partial class ShortestPath
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShortestPath));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.m_cmd_clearObstacle = new DevExpress.XtraEditors.SimpleButton();
            this.m_cmd_addObstacle = new DevExpress.XtraEditors.SimpleButton();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.m_grp_pEnd = new DevExpress.XtraEditors.GroupControl();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.m_txt_pE_y = new System.Windows.Forms.TextBox();
            this.m_txt_pE_x = new System.Windows.Forms.TextBox();
            this.m_cmd_findShortestPath = new DevExpress.XtraEditors.SimpleButton();
            this.m_grp_pStart = new DevExpress.XtraEditors.GroupControl();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_txt_pS_y = new System.Windows.Forms.TextBox();
            this.m_txt_pS_x = new System.Windows.Forms.TextBox();
            this.m_pan_paint = new System.Windows.Forms.Panel();
            this.m_grv_toa_do = new System.Windows.Forms.DataGridView();
            this.X = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_cmd_add_pstart_pend = new DevExpress.XtraEditors.SimpleButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_grp_pEnd)).BeginInit();
            this.m_grp_pEnd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_grp_pStart)).BeginInit();
            this.m_grp_pStart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_grv_toa_do)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.m_pan_paint, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.m_grv_toa_do, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(908, 589);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(175, 583);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.m_cmd_clearObstacle, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.m_cmd_addObstacle, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(169, 285);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // m_cmd_clearObstacle
            // 
            this.m_cmd_clearObstacle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_cmd_clearObstacle.Image = ((System.Drawing.Image)(resources.GetObject("m_cmd_clearObstacle.Image")));
            this.m_cmd_clearObstacle.Location = new System.Drawing.Point(3, 145);
            this.m_cmd_clearObstacle.Name = "m_cmd_clearObstacle";
            this.m_cmd_clearObstacle.Size = new System.Drawing.Size(163, 137);
            this.m_cmd_clearObstacle.TabIndex = 2;
            this.m_cmd_clearObstacle.Text = "Clear Obstacle";
            this.m_cmd_clearObstacle.Click += new System.EventHandler(this.m_cmd_clearObstacle_Click);
            // 
            // m_cmd_addObstacle
            // 
            this.m_cmd_addObstacle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_cmd_addObstacle.Image = ((System.Drawing.Image)(resources.GetObject("m_cmd_addObstacle.Image")));
            this.m_cmd_addObstacle.Location = new System.Drawing.Point(3, 3);
            this.m_cmd_addObstacle.Name = "m_cmd_addObstacle";
            this.m_cmd_addObstacle.Size = new System.Drawing.Size(163, 136);
            this.m_cmd_addObstacle.TabIndex = 1;
            this.m_cmd_addObstacle.Text = "Add Obstacle";
            this.m_cmd_addObstacle.Click += new System.EventHandler(this.m_cmd_addObstacle_Click);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.m_grp_pEnd, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.m_grp_pStart, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.m_cmd_findShortestPath, 0, 3);
            this.tableLayoutPanel4.Controls.Add(this.m_cmd_add_pstart_pend, 0, 2);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 294);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 4;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(169, 286);
            this.tableLayoutPanel4.TabIndex = 2;
            // 
            // m_grp_pEnd
            // 
            this.m_grp_pEnd.Controls.Add(this.label4);
            this.m_grp_pEnd.Controls.Add(this.label5);
            this.m_grp_pEnd.Controls.Add(this.m_txt_pE_y);
            this.m_grp_pEnd.Controls.Add(this.m_txt_pE_x);
            this.m_grp_pEnd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_grp_pEnd.Location = new System.Drawing.Point(3, 74);
            this.m_grp_pEnd.Name = "m_grp_pEnd";
            this.m_grp_pEnd.Size = new System.Drawing.Size(163, 65);
            this.m_grp_pEnd.TabIndex = 4;
            this.m_grp_pEnd.Text = "P_End";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(96, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(12, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "y";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(12, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "x";
            // 
            // m_txt_pE_y
            // 
            this.m_txt_pE_y.Location = new System.Drawing.Point(88, 41);
            this.m_txt_pE_y.Name = "m_txt_pE_y";
            this.m_txt_pE_y.Size = new System.Drawing.Size(70, 20);
            this.m_txt_pE_y.TabIndex = 4;
            this.m_txt_pE_y.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_txt_pE_y_KeyPress);
            // 
            // m_txt_pE_x
            // 
            this.m_txt_pE_x.Location = new System.Drawing.Point(5, 41);
            this.m_txt_pE_x.Name = "m_txt_pE_x";
            this.m_txt_pE_x.Size = new System.Drawing.Size(70, 20);
            this.m_txt_pE_x.TabIndex = 3;
            this.m_txt_pE_x.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_txt_pE_x_KeyPress);
            // 
            // m_cmd_findShortestPath
            // 
            this.m_cmd_findShortestPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_cmd_findShortestPath.Image = ((System.Drawing.Image)(resources.GetObject("m_cmd_findShortestPath.Image")));
            this.m_cmd_findShortestPath.Location = new System.Drawing.Point(3, 216);
            this.m_cmd_findShortestPath.Name = "m_cmd_findShortestPath";
            this.m_cmd_findShortestPath.Size = new System.Drawing.Size(163, 67);
            this.m_cmd_findShortestPath.TabIndex = 2;
            this.m_cmd_findShortestPath.Text = "Shortest Path";
            this.m_cmd_findShortestPath.Click += new System.EventHandler(this.m_cmd_findShortestPath_Click);
            // 
            // m_grp_pStart
            // 
            this.m_grp_pStart.Controls.Add(this.label3);
            this.m_grp_pStart.Controls.Add(this.label2);
            this.m_grp_pStart.Controls.Add(this.m_txt_pS_y);
            this.m_grp_pStart.Controls.Add(this.m_txt_pS_x);
            this.m_grp_pStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_grp_pStart.Location = new System.Drawing.Point(3, 3);
            this.m_grp_pStart.Name = "m_grp_pStart";
            this.m_grp_pStart.Size = new System.Drawing.Size(163, 65);
            this.m_grp_pStart.TabIndex = 3;
            this.m_grp_pStart.Text = "P_Start";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(96, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(12, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "y";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(12, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "x";
            // 
            // m_txt_pS_y
            // 
            this.m_txt_pS_y.Location = new System.Drawing.Point(88, 41);
            this.m_txt_pS_y.Name = "m_txt_pS_y";
            this.m_txt_pS_y.Size = new System.Drawing.Size(70, 20);
            this.m_txt_pS_y.TabIndex = 4;
            this.m_txt_pS_y.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_txt_pS_y_KeyPress);
            // 
            // m_txt_pS_x
            // 
            this.m_txt_pS_x.Location = new System.Drawing.Point(5, 41);
            this.m_txt_pS_x.Name = "m_txt_pS_x";
            this.m_txt_pS_x.Size = new System.Drawing.Size(70, 20);
            this.m_txt_pS_x.TabIndex = 3;
            this.m_txt_pS_x.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_txt_pS_x_KeyPress);
            // 
            // m_pan_paint
            // 
            this.m_pan_paint.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.m_pan_paint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pan_paint.Location = new System.Drawing.Point(365, 3);
            this.m_pan_paint.Name = "m_pan_paint";
            this.m_pan_paint.Size = new System.Drawing.Size(540, 583);
            this.m_pan_paint.TabIndex = 2;
            this.m_pan_paint.Paint += new System.Windows.Forms.PaintEventHandler(this.m_pan_paint_Paint);
            // 
            // m_grv_toa_do
            // 
            this.m_grv_toa_do.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.m_grv_toa_do.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.X,
            this.Y});
            this.m_grv_toa_do.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_grv_toa_do.Location = new System.Drawing.Point(184, 3);
            this.m_grv_toa_do.Name = "m_grv_toa_do";
            this.m_grv_toa_do.Size = new System.Drawing.Size(175, 583);
            this.m_grv_toa_do.TabIndex = 3;
            // 
            // X
            // 
            this.X.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.X.HeaderText = "X";
            this.X.Name = "X";
            // 
            // Y
            // 
            this.Y.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Y.HeaderText = "Y";
            this.Y.Name = "Y";
            // 
            // m_cmd_add_pstart_pend
            // 
            this.m_cmd_add_pstart_pend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_cmd_add_pstart_pend.Image = ((System.Drawing.Image)(resources.GetObject("m_cmd_add_pstart_pend.Image")));
            this.m_cmd_add_pstart_pend.Location = new System.Drawing.Point(3, 145);
            this.m_cmd_add_pstart_pend.Name = "m_cmd_add_pstart_pend";
            this.m_cmd_add_pstart_pend.Size = new System.Drawing.Size(163, 65);
            this.m_cmd_add_pstart_pend.TabIndex = 5;
            this.m_cmd_add_pstart_pend.Text = "Add P_Start P_End";
            this.m_cmd_add_pstart_pend.Click += new System.EventHandler(this.m_cmd_add_pstart_pend_Click);
            // 
            // ShortestPath
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(908, 589);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ShortestPath";
            this.Text = "Shortest Path";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ShortestPath_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_grp_pEnd)).EndInit();
            this.m_grp_pEnd.ResumeLayout(false);
            this.m_grp_pEnd.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_grp_pStart)).EndInit();
            this.m_grp_pStart.ResumeLayout(false);
            this.m_grp_pStart.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_grv_toa_do)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private DevExpress.XtraEditors.SimpleButton m_cmd_clearObstacle;
        private DevExpress.XtraEditors.SimpleButton m_cmd_addObstacle;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private DevExpress.XtraEditors.GroupControl m_grp_pEnd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox m_txt_pE_y;
        private System.Windows.Forms.TextBox m_txt_pE_x;
        private DevExpress.XtraEditors.SimpleButton m_cmd_findShortestPath;
        private DevExpress.XtraEditors.GroupControl m_grp_pStart;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox m_txt_pS_y;
        private System.Windows.Forms.TextBox m_txt_pS_x;
        private System.Windows.Forms.Panel m_pan_paint;
        private System.Windows.Forms.DataGridView m_grv_toa_do;
        private System.Windows.Forms.DataGridViewTextBoxColumn X;
        private System.Windows.Forms.DataGridViewTextBoxColumn Y;
        private DevExpress.XtraEditors.SimpleButton m_cmd_add_pstart_pend;

    }
}

