
namespace Golotip
{
    partial class DataGridForm
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
            this.trainingDataGrid = new System.Windows.Forms.DataGridView();
            this.examingDataGrid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.trainingDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.examingDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // trainingDataGrid
            // 
            this.trainingDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.trainingDataGrid.Location = new System.Drawing.Point(21, 12);
            this.trainingDataGrid.Name = "trainingDataGrid";
            this.trainingDataGrid.Size = new System.Drawing.Size(378, 409);
            this.trainingDataGrid.TabIndex = 0;
            // 
            // examingDataGrid
            // 
            this.examingDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.examingDataGrid.Location = new System.Drawing.Point(442, 12);
            this.examingDataGrid.Name = "examingDataGrid";
            this.examingDataGrid.Size = new System.Drawing.Size(383, 409);
            this.examingDataGrid.TabIndex = 1;
            // 
            // DataGridForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(860, 450);
            this.Controls.Add(this.examingDataGrid);
            this.Controls.Add(this.trainingDataGrid);
            this.Name = "DataGridForm";
            this.Text = "DataGridForm";
            this.Load += new System.EventHandler(this.DataGridForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trainingDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.examingDataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView trainingDataGrid;
        private System.Windows.Forms.DataGridView examingDataGrid;
    }
}