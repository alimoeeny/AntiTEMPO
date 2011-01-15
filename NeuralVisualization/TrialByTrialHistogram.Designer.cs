namespace NeuralVisualization
{
    partial class TrialByTrialHistogram
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
            this.TrialByTrialPictureBox = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.BinsizeComboBox = new System.Windows.Forms.ComboBox();
            this.WindowedRadioButton = new System.Windows.Forms.RadioButton();
            this.SmoothedRadioButton = new System.Windows.Forms.RadioButton();
            this.WindowSizeNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.ISIRadioButton = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.TrialByTrialPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WindowSizeNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // TrialByTrialPictureBox
            // 
            this.TrialByTrialPictureBox.Location = new System.Drawing.Point(4, 4);
            this.TrialByTrialPictureBox.Name = "TrialByTrialPictureBox";
            this.TrialByTrialPictureBox.Size = new System.Drawing.Size(300, 225);
            this.TrialByTrialPictureBox.TabIndex = 0;
            this.TrialByTrialPictureBox.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(229, 235);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 62);
            this.button1.TabIndex = 1;
            this.button1.Text = "Go !";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(85, 235);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Bin Size (ms)";
            // 
            // BinsizeComboBox
            // 
            this.BinsizeComboBox.FormattingEnabled = true;
            this.BinsizeComboBox.Items.AddRange(new object[] {
            "7",
            "14",
            "25",
            "28",
            "35",
            "70",
            "100",
            "140",
            "350",
            "700"});
            this.BinsizeComboBox.Location = new System.Drawing.Point(158, 232);
            this.BinsizeComboBox.Name = "BinsizeComboBox";
            this.BinsizeComboBox.Size = new System.Drawing.Size(56, 21);
            this.BinsizeComboBox.TabIndex = 2;
            this.BinsizeComboBox.Text = "35";
            // 
            // WindowedRadioButton
            // 
            this.WindowedRadioButton.AutoSize = true;
            this.WindowedRadioButton.Location = new System.Drawing.Point(4, 233);
            this.WindowedRadioButton.Name = "WindowedRadioButton";
            this.WindowedRadioButton.Size = new System.Drawing.Size(76, 17);
            this.WindowedRadioButton.TabIndex = 4;
            this.WindowedRadioButton.Text = "Windowed";
            this.WindowedRadioButton.UseVisualStyleBackColor = true;
            // 
            // SmoothedRadioButton
            // 
            this.SmoothedRadioButton.AutoSize = true;
            this.SmoothedRadioButton.Checked = true;
            this.SmoothedRadioButton.Location = new System.Drawing.Point(4, 256);
            this.SmoothedRadioButton.Name = "SmoothedRadioButton";
            this.SmoothedRadioButton.Size = new System.Drawing.Size(73, 17);
            this.SmoothedRadioButton.TabIndex = 4;
            this.SmoothedRadioButton.TabStop = true;
            this.SmoothedRadioButton.Text = "Smoothed";
            this.SmoothedRadioButton.UseVisualStyleBackColor = true;
            // 
            // WindowSizeNumericUpDown
            // 
            this.WindowSizeNumericUpDown.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.WindowSizeNumericUpDown.Location = new System.Drawing.Point(158, 256);
            this.WindowSizeNumericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.WindowSizeNumericUpDown.Name = "WindowSizeNumericUpDown";
            this.WindowSizeNumericUpDown.Size = new System.Drawing.Size(56, 20);
            this.WindowSizeNumericUpDown.TabIndex = 5;
            this.WindowSizeNumericUpDown.Value = new decimal(new int[] {
            51,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(85, 258);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Win Size (ms)";
            // 
            // ISIRadioButton
            // 
            this.ISIRadioButton.AutoSize = true;
            this.ISIRadioButton.Location = new System.Drawing.Point(4, 279);
            this.ISIRadioButton.Name = "ISIRadioButton";
            this.ISIRadioButton.Size = new System.Drawing.Size(38, 17);
            this.ISIRadioButton.TabIndex = 6;
            this.ISIRadioButton.TabStop = true;
            this.ISIRadioButton.Text = "ISI";
            this.ISIRadioButton.UseVisualStyleBackColor = true;
            // 
            // TrialByTrialHistogram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ISIRadioButton);
            this.Controls.Add(this.WindowSizeNumericUpDown);
            this.Controls.Add(this.SmoothedRadioButton);
            this.Controls.Add(this.WindowedRadioButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BinsizeComboBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.TrialByTrialPictureBox);
            this.Name = "TrialByTrialHistogram";
            this.Size = new System.Drawing.Size(400, 300);
            ((System.ComponentModel.ISupportInitialize)(this.TrialByTrialPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WindowSizeNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox TrialByTrialPictureBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox BinsizeComboBox;
        private System.Windows.Forms.RadioButton WindowedRadioButton;
        private System.Windows.Forms.RadioButton SmoothedRadioButton;
        private System.Windows.Forms.NumericUpDown WindowSizeNumericUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton ISIRadioButton;
    }
}
