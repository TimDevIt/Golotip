
namespace Golotip
{
    partial class PropForm
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
            this.trackBarWeightA = new System.Windows.Forms.TrackBar();
            this.trackBarWeightB = new System.Windows.Forms.TrackBar();
            this.labelWeightA = new System.Windows.Forms.Label();
            this.labelWeightB = new System.Windows.Forms.Label();
            this.tbDataWeightA = new System.Windows.Forms.TextBox();
            this.tbDataWeightB = new System.Windows.Forms.TextBox();
            this.lableLimit = new System.Windows.Forms.Label();
            this.tbLimit = new System.Windows.Forms.TextBox();
            this.checkBoxLimit = new System.Windows.Forms.CheckBox();
            this.btnTraining = new System.Windows.Forms.Button();
            this.btnExaming = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarWeightA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarWeightB)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBarWeightA
            // 
            this.trackBarWeightA.Location = new System.Drawing.Point(111, 24);
            this.trackBarWeightA.Name = "trackBarWeightA";
            this.trackBarWeightA.Size = new System.Drawing.Size(104, 45);
            this.trackBarWeightA.TabIndex = 0;
            this.trackBarWeightA.Scroll += new System.EventHandler(this.trackBarWeightA_Scroll);
            // 
            // trackBarWeightB
            // 
            this.trackBarWeightB.Location = new System.Drawing.Point(111, 75);
            this.trackBarWeightB.Name = "trackBarWeightB";
            this.trackBarWeightB.Size = new System.Drawing.Size(104, 45);
            this.trackBarWeightB.TabIndex = 1;
            this.trackBarWeightB.Scroll += new System.EventHandler(this.trackBarWeightB_Scroll);
            // 
            // labelWeightA
            // 
            this.labelWeightA.AutoSize = true;
            this.labelWeightA.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelWeightA.Location = new System.Drawing.Point(12, 24);
            this.labelWeightA.Name = "labelWeightA";
            this.labelWeightA.Size = new System.Drawing.Size(93, 25);
            this.labelWeightA.TabIndex = 2;
            this.labelWeightA.Text = "WeightA";
            // 
            // labelWeightB
            // 
            this.labelWeightB.AutoSize = true;
            this.labelWeightB.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelWeightB.Location = new System.Drawing.Point(12, 75);
            this.labelWeightB.Name = "labelWeightB";
            this.labelWeightB.Size = new System.Drawing.Size(93, 25);
            this.labelWeightB.TabIndex = 3;
            this.labelWeightB.Text = "WeightB";
            // 
            // tbDataWeightA
            // 
            this.tbDataWeightA.Location = new System.Drawing.Point(221, 29);
            this.tbDataWeightA.Name = "tbDataWeightA";
            this.tbDataWeightA.Size = new System.Drawing.Size(42, 20);
            this.tbDataWeightA.TabIndex = 4;
            this.tbDataWeightA.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbDataWeightA_KeyUp);
            // 
            // tbDataWeightB
            // 
            this.tbDataWeightB.Location = new System.Drawing.Point(221, 75);
            this.tbDataWeightB.Name = "tbDataWeightB";
            this.tbDataWeightB.Size = new System.Drawing.Size(42, 20);
            this.tbDataWeightB.TabIndex = 5;
            this.tbDataWeightB.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbDataWeightB_KeyUp);
            // 
            // lableLimit
            // 
            this.lableLimit.AutoSize = true;
            this.lableLimit.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lableLimit.Location = new System.Drawing.Point(12, 119);
            this.lableLimit.Name = "lableLimit";
            this.lableLimit.Size = new System.Drawing.Size(57, 25);
            this.lableLimit.TabIndex = 6;
            this.lableLimit.Text = "Limit";
            // 
            // tbLimit
            // 
            this.tbLimit.Location = new System.Drawing.Point(17, 147);
            this.tbLimit.Name = "tbLimit";
            this.tbLimit.Size = new System.Drawing.Size(76, 20);
            this.tbLimit.TabIndex = 7;
            // 
            // checkBoxLimit
            // 
            this.checkBoxLimit.AutoSize = true;
            this.checkBoxLimit.Location = new System.Drawing.Point(99, 150);
            this.checkBoxLimit.Name = "checkBoxLimit";
            this.checkBoxLimit.Size = new System.Drawing.Size(65, 17);
            this.checkBoxLimit.TabIndex = 8;
            this.checkBoxLimit.Text = "Activate";
            this.checkBoxLimit.UseVisualStyleBackColor = true;
            this.checkBoxLimit.CheckedChanged += new System.EventHandler(this.checkBoxLimit_CheckedChanged);
            // 
            // btnTraining
            // 
            this.btnTraining.Location = new System.Drawing.Point(17, 188);
            this.btnTraining.Name = "btnTraining";
            this.btnTraining.Size = new System.Drawing.Size(110, 29);
            this.btnTraining.TabIndex = 9;
            this.btnTraining.Text = "Training";
            this.btnTraining.UseVisualStyleBackColor = true;
            this.btnTraining.Click += new System.EventHandler(this.btnTraining_Click);
            // 
            // btnExaming
            // 
            this.btnExaming.Location = new System.Drawing.Point(133, 188);
            this.btnExaming.Name = "btnExaming";
            this.btnExaming.Size = new System.Drawing.Size(110, 29);
            this.btnExaming.TabIndex = 10;
            this.btnExaming.Text = "Examing";
            this.btnExaming.UseVisualStyleBackColor = true;
            this.btnExaming.Click += new System.EventHandler(this.btnExaming_Click);
            // 
            // PropForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 302);
            this.Controls.Add(this.btnExaming);
            this.Controls.Add(this.btnTraining);
            this.Controls.Add(this.checkBoxLimit);
            this.Controls.Add(this.tbLimit);
            this.Controls.Add(this.lableLimit);
            this.Controls.Add(this.tbDataWeightB);
            this.Controls.Add(this.tbDataWeightA);
            this.Controls.Add(this.labelWeightB);
            this.Controls.Add(this.labelWeightA);
            this.Controls.Add(this.trackBarWeightB);
            this.Controls.Add(this.trackBarWeightA);
            this.Name = "PropForm";
            this.Text = "PropForm";
            this.Load += new System.EventHandler(this.PropForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarWeightA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarWeightB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBarWeightA;
        private System.Windows.Forms.TrackBar trackBarWeightB;
        private System.Windows.Forms.Label labelWeightA;
        private System.Windows.Forms.Label labelWeightB;
        private System.Windows.Forms.TextBox tbDataWeightA;
        private System.Windows.Forms.TextBox tbDataWeightB;
        private System.Windows.Forms.Label lableLimit;
        private System.Windows.Forms.TextBox tbLimit;
        private System.Windows.Forms.CheckBox checkBoxLimit;
        private System.Windows.Forms.Button btnTraining;
        private System.Windows.Forms.Button btnExaming;
    }
}