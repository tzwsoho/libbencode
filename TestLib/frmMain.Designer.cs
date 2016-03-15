namespace TestTorrent
{
    partial class frmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tvwOutput = new System.Windows.Forms.TreeView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.cmbEncoding = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.lstTorrents = new System.Windows.Forms.ListBox();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvwOutput
            // 
            this.tvwOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvwOutput.HideSelection = false;
            this.tvwOutput.Location = new System.Drawing.Point(208, 25);
            this.tvwOutput.Name = "tvwOutput";
            this.tvwOutput.ShowNodeToolTips = true;
            this.tvwOutput.Size = new System.Drawing.Size(535, 307);
            this.tvwOutput.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.cmbEncoding,
            this.toolStripLabel2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(743, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(32, 22);
            this.toolStripLabel1.Text = "编码";
            // 
            // cmbEncoding
            // 
            this.cmbEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEncoding.Name = "cmbEncoding";
            this.cmbEncoding.Size = new System.Drawing.Size(121, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(209, 22);
            this.toolStripLabel2.Text = "请把 torrent 文件拖到下面的列表框...";
            // 
            // lstTorrents
            // 
            this.lstTorrents.AllowDrop = true;
            this.lstTorrents.Dock = System.Windows.Forms.DockStyle.Left;
            this.lstTorrents.FormattingEnabled = true;
            this.lstTorrents.ItemHeight = 12;
            this.lstTorrents.Location = new System.Drawing.Point(0, 25);
            this.lstTorrents.Name = "lstTorrents";
            this.lstTorrents.Size = new System.Drawing.Size(208, 307);
            this.lstTorrents.TabIndex = 3;
            this.lstTorrents.SelectedIndexChanged += new System.EventHandler(this.lstTorrents_SelectedIndexChanged);
            this.lstTorrents.DragDrop += new System.Windows.Forms.DragEventHandler(this.lstTorrents_DragDrop);
            this.lstTorrents.DragEnter += new System.Windows.Forms.DragEventHandler(this.lstTorrents_DragEnter);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 332);
            this.Controls.Add(this.tvwOutput);
            this.Controls.Add(this.lstTorrents);
            this.Controls.Add(this.toolStrip1);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "解析 torrent 文件";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView tvwOutput;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox cmbEncoding;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ListBox lstTorrents;
    }
}

