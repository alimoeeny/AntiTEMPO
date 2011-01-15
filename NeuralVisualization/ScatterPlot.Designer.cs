namespace NeuralVisualization
{
    partial class ScatterPlot
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.ResponseOnsetNumericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.ResponseOffsetNumericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ResponseOnsetNumericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ResponseOffsetNumericUpDown2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(220)))));
            this.pictureBox1.Location = new System.Drawing.Point(4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(250, 250);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(260, 192);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 62);
            this.button1.TabIndex = 1;
            this.button1.Text = "Go !";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ResponseOnsetNumericUpDown1
            // 
            this.ResponseOnsetNumericUpDown1.Location = new System.Drawing.Point(260, 22);
            this.ResponseOnsetNumericUpDown1.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.ResponseOnsetNumericUpDown1.Name = "ResponseOnsetNumericUpDown1";
            this.ResponseOnsetNumericUpDown1.Size = new System.Drawing.Size(75, 20);
            this.ResponseOnsetNumericUpDown1.TabIndex = 2;
            this.ResponseOnsetNumericUpDown1.Value = new decimal(new int[] {
            180,
            0,
            0,
            0});
            // 
            // ResponseOffsetNumericUpDown2
            // 
            this.ResponseOffsetNumericUpDown2.Location = new System.Drawing.Point(260, 64);
            this.ResponseOffsetNumericUpDown2.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.ResponseOffsetNumericUpDown2.Name = "ResponseOffsetNumericUpDown2";
            this.ResponseOffsetNumericUpDown2.Size = new System.Drawing.Size(75, 20);
            this.ResponseOffsetNumericUpDown2.TabIndex = 2;
            this.ResponseOffsetNumericUpDown2.Value = new decimal(new int[] {
            420,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(260, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Response Onset";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(257, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Response Offset";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(260, 163);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Export";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(261, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 73);
            this.label3.TabIndex = 5;
            this.label3.Text = "Correlation:";
            // 
            // ScatterPlot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ResponseOffsetNumericUpDown2);
            this.Controls.Add(this.ResponseOnsetNumericUpDown1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "ScatterPlot";
            this.Size = new System.Drawing.Size(400, 300);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ResponseOnsetNumericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ResponseOffsetNumericUpDown2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown ResponseOnsetNumericUpDown1;
        private System.Windows.Forms.NumericUpDown ResponseOffsetNumericUpDown2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label3;
    }
}
