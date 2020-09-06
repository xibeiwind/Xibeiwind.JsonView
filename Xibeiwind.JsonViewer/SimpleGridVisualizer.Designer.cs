namespace Xibeiwind.JsonViewer
{
    partial class SimpleGridVisualizer
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.simpleGridView = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.simpleGridView)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // simpleGridView
            // 
            this.simpleGridView.AllowUserToAddRows = false;
            this.simpleGridView.AllowUserToDeleteRows = false;
            this.simpleGridView.AllowUserToOrderColumns = true;
            this.simpleGridView.AllowUserToResizeRows = false;
            this.simpleGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.simpleGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.simpleGridView.ContextMenuStrip = this.contextMenuStrip1;
            this.simpleGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.simpleGridView.Location = new System.Drawing.Point(0, 0);
            this.simpleGridView.Name = "simpleGridView";
            this.simpleGridView.ReadOnly = true;
            this.simpleGridView.RowTemplate.Height = 23;
            this.simpleGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.simpleGridView.Size = new System.Drawing.Size(358, 301);
            this.simpleGridView.TabIndex = 0;
            this.simpleGridView.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.simpleGridView_RowPostPaint);
            this.simpleGridView.KeyUp += new System.Windows.Forms.KeyEventHandler(this.simpleGridView_KeyUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 26);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(100, 22);
            this.toolStripMenuItem1.Text = "拷贝";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // SimpleGridVisualizer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.simpleGridView);
            this.Name = "SimpleGridVisualizer";
            this.Size = new System.Drawing.Size(358, 301);
            ((System.ComponentModel.ISupportInitialize)(this.simpleGridView)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView simpleGridView;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    }
}
