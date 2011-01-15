namespace StimulusPSTH
{
    partial class StimulusPSTH
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
            this.StimulusPictureBox = new System.Windows.Forms.PictureBox();
            this.PSTHPictureBox = new System.Windows.Forms.PictureBox();
            this.MaxmimumFRLabel = new System.Windows.Forms.Label();
            this.TrialsLabel = new System.Windows.Forms.Label();
            this.StimulusLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.RatioLabel = new System.Windows.Forms.Label();
            this.RankLabel = new System.Windows.Forms.Label();
            this.AvgFRLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.StimulusPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PSTHPictureBox)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // StimulusPictureBox
            // 
            this.StimulusPictureBox.BackColor = System.Drawing.Color.Silver;
            this.StimulusPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StimulusPictureBox.Location = new System.Drawing.Point(2, 4);
            this.StimulusPictureBox.Name = "StimulusPictureBox";
            this.StimulusPictureBox.Size = new System.Drawing.Size(100, 100);
            this.StimulusPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.StimulusPictureBox.TabIndex = 0;
            this.StimulusPictureBox.TabStop = false;
            // 
            // PSTHPictureBox
            // 
            this.PSTHPictureBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.PSTHPictureBox.InitialImage = null;
            this.PSTHPictureBox.Location = new System.Drawing.Point(2, 110);
            this.PSTHPictureBox.Name = "PSTHPictureBox";
            this.PSTHPictureBox.Size = new System.Drawing.Size(250, 80);
            this.PSTHPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PSTHPictureBox.TabIndex = 1;
            this.PSTHPictureBox.TabStop = false;
            this.PSTHPictureBox.DoubleClick += new System.EventHandler(this.PSTHPictureBox_DoubleClick);
            // 
            // MaxmimumFRLabel
            // 
            this.MaxmimumFRLabel.AutoEllipsis = true;
            this.MaxmimumFRLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.MaxmimumFRLabel.Font = new System.Drawing.Font("Verdana", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaxmimumFRLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.MaxmimumFRLabel.Location = new System.Drawing.Point(3, 110);
            this.MaxmimumFRLabel.Name = "MaxmimumFRLabel";
            this.MaxmimumFRLabel.Size = new System.Drawing.Size(32, 13);
            this.MaxmimumFRLabel.TabIndex = 2;
            // 
            // TrialsLabel
            // 
            this.TrialsLabel.AutoEllipsis = true;
            this.TrialsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TrialsLabel.Location = new System.Drawing.Point(3, 51);
            this.TrialsLabel.Name = "TrialsLabel";
            this.TrialsLabel.Size = new System.Drawing.Size(133, 13);
            this.TrialsLabel.TabIndex = 1;
            this.TrialsLabel.Text = "Trials :";
            // 
            // StimulusLabel
            // 
            this.StimulusLabel.AutoEllipsis = true;
            this.StimulusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StimulusLabel.Location = new System.Drawing.Point(3, 34);
            this.StimulusLabel.Name = "StimulusLabel";
            this.StimulusLabel.Size = new System.Drawing.Size(133, 13);
            this.StimulusLabel.TabIndex = 0;
            this.StimulusLabel.Text = "Stimulus: ";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.RatioLabel);
            this.panel1.Controls.Add(this.RankLabel);
            this.panel1.Controls.Add(this.AvgFRLabel);
            this.panel1.Controls.Add(this.TrialsLabel);
            this.panel1.Controls.Add(this.StimulusLabel);
            this.panel1.Location = new System.Drawing.Point(108, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(143, 100);
            this.panel1.TabIndex = 3;
            // 
            // RatioLabel
            // 
            this.RatioLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RatioLabel.Location = new System.Drawing.Point(3, 17);
            this.RatioLabel.Name = "RatioLabel";
            this.RatioLabel.Size = new System.Drawing.Size(133, 13);
            this.RatioLabel.TabIndex = 5;
            this.RatioLabel.Text = ". / Best:";
            // 
            // RankLabel
            // 
            this.RankLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RankLabel.Location = new System.Drawing.Point(3, 0);
            this.RankLabel.Name = "RankLabel";
            this.RankLabel.Size = new System.Drawing.Size(133, 13);
            this.RankLabel.TabIndex = 4;
            this.RankLabel.Text = "Rank :";
            // 
            // AvgFRLabel
            // 
            this.AvgFRLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AvgFRLabel.Location = new System.Drawing.Point(3, 68);
            this.AvgFRLabel.Name = "AvgFRLabel";
            this.AvgFRLabel.Size = new System.Drawing.Size(133, 13);
            this.AvgFRLabel.TabIndex = 3;
            this.AvgFRLabel.Text = "Avg F R :";
            // 
            // StimulusPSTH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MaxmimumFRLabel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.PSTHPictureBox);
            this.Controls.Add(this.StimulusPictureBox);
            this.Name = "StimulusPSTH";
            this.Size = new System.Drawing.Size(254, 192);
            ((System.ComponentModel.ISupportInitialize)(this.StimulusPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PSTHPictureBox)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox StimulusPictureBox;
        private System.Windows.Forms.PictureBox PSTHPictureBox;
        private System.Windows.Forms.Label StimulusLabel;
        private System.Windows.Forms.Label TrialsLabel;
        private System.Windows.Forms.Label MaxmimumFRLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label AvgFRLabel;
        private System.Windows.Forms.Label RankLabel;
        private System.Windows.Forms.Label RatioLabel;
    }
}
