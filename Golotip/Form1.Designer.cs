
namespace Golotip
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dataGraph = new ZedGraph.ZedGraphControl();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileDialog = new System.Windows.Forms.OpenFileDialog();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGraph
            // 
            this.dataGraph.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGraph.Location = new System.Drawing.Point(0, 27);
            this.dataGraph.Name = "dataGraph";
            this.dataGraph.ScrollGrace = 0D;
            this.dataGraph.ScrollMaxX = 0D;
            this.dataGraph.ScrollMaxY = 0D;
            this.dataGraph.ScrollMaxY2 = 0D;
            this.dataGraph.ScrollMinX = 0D;
            this.dataGraph.ScrollMinY = 0D;
            this.dataGraph.ScrollMinY2 = 0D;
            this.dataGraph.Size = new System.Drawing.Size(1139, 622);
            this.dataGraph.TabIndex = 0;
            this.dataGraph.UseExtendedPrintDialog = true;
            // 
            // menu
            // 
            this.menu.BackColor = System.Drawing.Color.CornflowerBlue;
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.dataToolStripMenuItem,
            this.propStripMenuItem,
            this.helpToolStripMenuItem});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(1132, 25);
            this.menu.TabIndex = 1;
            this.menu.Text = "menu";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.createToolStripMenuItem});
            this.fileToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 10F);
            this.fileToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(38, 21);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // createToolStripMenuItem
            // 
            this.createToolStripMenuItem.Name = "createToolStripMenuItem";
            this.createToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.createToolStripMenuItem.Text = "Create";
            this.createToolStripMenuItem.Click += new System.EventHandler(this.createToolStripMenuItem_Click);
            // 
            // dataToolStripMenuItem
            // 
            this.dataToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 10F);
            this.dataToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataToolStripMenuItem.Name = "dataToolStripMenuItem";
            this.dataToolStripMenuItem.Size = new System.Drawing.Size(49, 21);
            this.dataToolStripMenuItem.Text = "Data";
            this.dataToolStripMenuItem.Click += new System.EventHandler(this.dataToolStripMenuItem_Click);
            // 
            // propStripMenuItem
            // 
            this.propStripMenuItem.Font = new System.Drawing.Font("Tahoma", 10F);
            this.propStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.propStripMenuItem.Name = "propStripMenuItem";
            this.propStripMenuItem.Size = new System.Drawing.Size(81, 21);
            this.propStripMenuItem.Text = "Properties";
            this.propStripMenuItem.Click += new System.EventHandler(this.propStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 10F);
            this.helpToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(46, 21);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // fileDialog
            // 
            this.fileDialog.FileName = "fileDialog";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1132, 649);
            this.Controls.Add(this.dataGraph);
            this.Controls.Add(this.menu);
            this.MainMenuStrip = this.menu;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ZedGraph.ZedGraphControl dataGraph;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog fileDialog;
        private System.Windows.Forms.ToolStripMenuItem propStripMenuItem;
    }
}

