namespace Xibeiwind.JsonViewer
{
    partial class GridVisualizer
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
            this.lvGrid = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // lvGrid
            // 
            this.lvGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvGrid.FullRowSelect = true;
            this.lvGrid.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvGrid.HideSelection = false;
            this.lvGrid.Location = new System.Drawing.Point(0, 0);
            this.lvGrid.Name = "lvGrid";
            this.lvGrid.Size = new System.Drawing.Size(150, 150);
            this.lvGrid.TabIndex = 0;
            this.lvGrid.UseCompatibleStateImageBehavior = false;
            this.lvGrid.View = System.Windows.Forms.View.Details;
            // 
            // GridVisualizer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvGrid);
            this.Name = "GridVisualizer";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvGrid;
    }
}
