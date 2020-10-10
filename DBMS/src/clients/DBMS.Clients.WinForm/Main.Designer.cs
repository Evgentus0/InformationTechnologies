namespace DBMS.Clients.WinForm
{
    partial class Main
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
            this.flowLayoutPanelMenu = new System.Windows.Forms.FlowLayoutPanel();
            this.menuStripTop = new System.Windows.Forms.MenuStrip();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.flowLayoutPanelTopMenu = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBoxGrid = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // flowLayoutPanelMenu
            // 
            this.flowLayoutPanelMenu.AutoScroll = true;
            this.flowLayoutPanelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.flowLayoutPanelMenu.Location = new System.Drawing.Point(0, 24);
            this.flowLayoutPanelMenu.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.flowLayoutPanelMenu.Name = "flowLayoutPanelMenu";
            this.flowLayoutPanelMenu.Size = new System.Drawing.Size(230, 719);
            this.flowLayoutPanelMenu.TabIndex = 0;
            // 
            // menuStripTop
            // 
            this.menuStripTop.Location = new System.Drawing.Point(0, 0);
            this.menuStripTop.Name = "menuStripTop";
            this.menuStripTop.Size = new System.Drawing.Size(1033, 24);
            this.menuStripTop.TabIndex = 2;
            this.menuStripTop.Text = "menuStrip1";
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "DbFile";
            this.openFileDialog.Filter = "DataBase file|*.edbf";
            // 
            // flowLayoutPanelTopMenu
            // 
            this.flowLayoutPanelTopMenu.Location = new System.Drawing.Point(235, 7);
            this.flowLayoutPanelTopMenu.Name = "flowLayoutPanelTopMenu";
            this.flowLayoutPanelTopMenu.Size = new System.Drawing.Size(786, 100);
            this.flowLayoutPanelTopMenu.TabIndex = 3;
            // 
            // groupBoxGrid
            // 
            this.groupBoxGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxGrid.Location = new System.Drawing.Point(230, 113);
            this.groupBoxGrid.Name = "groupBoxGrid";
            this.groupBoxGrid.Size = new System.Drawing.Size(803, 630);
            this.groupBoxGrid.TabIndex = 4;
            this.groupBoxGrid.TabStop = false;
            this.groupBoxGrid.Text = "Data";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1033, 743);
            this.Controls.Add(this.groupBoxGrid);
            this.Controls.Add(this.flowLayoutPanelTopMenu);
            this.Controls.Add(this.flowLayoutPanelMenu);
            this.Controls.Add(this.menuStripTop);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MainMenuStrip = this.menuStripTop;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelMenu;
        private System.Windows.Forms.MenuStrip menuStripTop;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelTopMenu;
        private System.Windows.Forms.GroupBox groupBoxGrid;
    }
}