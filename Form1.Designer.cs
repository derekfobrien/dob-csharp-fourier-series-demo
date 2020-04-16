namespace FourierSeriesDemo
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
            this.label1 = new System.Windows.Forms.Label();
            this.TxtPeriod = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.BtnPlot = new System.Windows.Forms.Button();
            this.PicDisplay = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.BtnOpen = new System.Windows.Forms.Button();
            this.LblIters = new System.Windows.Forms.Label();
            this.LisCurve = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.PicDisplay)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Half-Period";
            // 
            // TxtPeriod
            // 
            this.TxtPeriod.Location = new System.Drawing.Point(12, 29);
            this.TxtPeriod.Name = "TxtPeriod";
            this.TxtPeriod.Size = new System.Drawing.Size(206, 25);
            this.TxtPeriod.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 17);
            this.label6.TabIndex = 6;
            this.label6.Text = "Iterations";
            // 
            // BtnPlot
            // 
            this.BtnPlot.Enabled = false;
            this.BtnPlot.Location = new System.Drawing.Point(12, 474);
            this.BtnPlot.Name = "BtnPlot";
            this.BtnPlot.Size = new System.Drawing.Size(206, 37);
            this.BtnPlot.TabIndex = 7;
            this.BtnPlot.Text = "One More Iteration";
            this.BtnPlot.UseVisualStyleBackColor = true;
            this.BtnPlot.Click += new System.EventHandler(this.BtnPlot_Click);
            // 
            // PicDisplay
            // 
            this.PicDisplay.BackColor = System.Drawing.Color.White;
            this.PicDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PicDisplay.Location = new System.Drawing.Point(224, 9);
            this.PicDisplay.Name = "PicDisplay";
            this.PicDisplay.Size = new System.Drawing.Size(924, 781);
            this.PicDisplay.TabIndex = 9;
            this.PicDisplay.TabStop = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // BtnOpen
            // 
            this.BtnOpen.Location = new System.Drawing.Point(12, 431);
            this.BtnOpen.Name = "BtnOpen";
            this.BtnOpen.Size = new System.Drawing.Size(206, 37);
            this.BtnOpen.TabIndex = 10;
            this.BtnOpen.Text = "Open File";
            this.BtnOpen.UseVisualStyleBackColor = true;
            this.BtnOpen.Click += new System.EventHandler(this.BtnOpen_Click);
            // 
            // LblIters
            // 
            this.LblIters.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.LblIters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblIters.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblIters.Location = new System.Drawing.Point(12, 78);
            this.LblIters.Name = "LblIters";
            this.LblIters.Size = new System.Drawing.Size(206, 39);
            this.LblIters.TabIndex = 12;
            this.LblIters.Text = "N";
            this.LblIters.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LisCurve
            // 
            this.LisCurve.FormattingEnabled = true;
            this.LisCurve.ItemHeight = 17;
            this.LisCurve.Location = new System.Drawing.Point(12, 127);
            this.LisCurve.Name = "LisCurve";
            this.LisCurve.Size = new System.Drawing.Size(205, 276);
            this.LisCurve.TabIndex = 13;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1160, 802);
            this.Controls.Add(this.LisCurve);
            this.Controls.Add(this.LblIters);
            this.Controls.Add(this.BtnOpen);
            this.Controls.Add(this.PicDisplay);
            this.Controls.Add(this.BtnPlot);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.TxtPeriod);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "Fourier Series Demonstration";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.PicDisplay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtPeriod;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button BtnPlot;
        private System.Windows.Forms.PictureBox PicDisplay;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button BtnOpen;
        private System.Windows.Forms.Label LblIters;
        private System.Windows.Forms.ListBox LisCurve;
    }
}

