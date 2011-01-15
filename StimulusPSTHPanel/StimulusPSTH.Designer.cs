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
            this.components = new System.ComponentModel.Container();
            this.StimulusPictureBox = new System.Windows.Forms.PictureBox();
            this.ExportContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exportDescriptivesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportTrialsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportRasterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PSTHPictureBox = new System.Windows.Forms.PictureBox();
            this.MaxmimumFRLabel = new System.Windows.Forms.Label();
            this.TrialsLabel = new System.Windows.Forms.Label();
            this.StimulusLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CategoryRatioLabel = new System.Windows.Forms.Label();
            this.RatioLabel = new System.Windows.Forms.Label();
            this.ResponseStabilityLabel = new System.Windows.Forms.Label();
            this.ResponseSDLabel = new System.Windows.Forms.Label();
            this.RankLabel = new System.Windows.Forms.Label();
            this.AvgFRLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.StimulusPictureBox)).BeginInit();
            this.ExportContextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PSTHPictureBox)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // StimulusPictureBox
            // 
            this.StimulusPictureBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.StimulusPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StimulusPictureBox.ContextMenuStrip = this.ExportContextMenuStrip;
            this.StimulusPictureBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.StimulusPictureBox.Location = new System.Drawing.Point(5, 75);
            this.StimulusPictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.StimulusPictureBox.Name = "StimulusPictureBox";
            this.StimulusPictureBox.Size = new System.Drawing.Size(70, 70);
            this.StimulusPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.StimulusPictureBox.TabIndex = 0;
            this.StimulusPictureBox.TabStop = false;
            this.StimulusPictureBox.Click += new System.EventHandler(this.StimulusPictureBox_Click);
            // 
            // ExportContextMenuStrip
            // 
            this.ExportContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportDescriptivesToolStripMenuItem,
            this.exportTrialsToolStripMenuItem,
            this.exportRasterToolStripMenuItem});
            this.ExportContextMenuStrip.Name = "ExportContextMenuStrip";
            this.ExportContextMenuStrip.Size = new System.Drawing.Size(171, 70);
            // 
            // exportDescriptivesToolStripMenuItem
            // 
            this.exportDescriptivesToolStripMenuItem.Name = "exportDescriptivesToolStripMenuItem";
            this.exportDescriptivesToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.exportDescriptivesToolStripMenuItem.Text = "Export Descriptives";
            // 
            // exportTrialsToolStripMenuItem
            // 
            this.exportTrialsToolStripMenuItem.Name = "exportTrialsToolStripMenuItem";
            this.exportTrialsToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.exportTrialsToolStripMenuItem.Text = "Export Trials (PSTH)";
            // 
            // exportRasterToolStripMenuItem
            // 
            this.exportRasterToolStripMenuItem.Name = "exportRasterToolStripMenuItem";
            this.exportRasterToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.exportRasterToolStripMenuItem.Text = "Export Raster";
            // 
            // PSTHPictureBox
            // 
            this.PSTHPictureBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.PSTHPictureBox.InitialImage = null;
            this.PSTHPictureBox.Location = new System.Drawing.Point(5, 3);
            this.PSTHPictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.PSTHPictureBox.Name = "PSTHPictureBox";
            this.PSTHPictureBox.Size = new System.Drawing.Size(230, 70);
            this.PSTHPictureBox.TabIndex = 1;
            this.PSTHPictureBox.TabStop = false;
            this.PSTHPictureBox.DoubleClick += new System.EventHandler(this.PSTHPictureBox_DoubleClick);
            // 
            // MaxmimumFRLabel
            // 
            this.MaxmimumFRLabel.AutoEllipsis = true;
            this.MaxmimumFRLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.MaxmimumFRLabel.Font = new System.Drawing.Font("Verdana", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaxmimumFRLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.MaxmimumFRLabel.Location = new System.Drawing.Point(5, 3);
            this.MaxmimumFRLabel.Name = "MaxmimumFRLabel";
            this.MaxmimumFRLabel.Size = new System.Drawing.Size(32, 13);
            this.MaxmimumFRLabel.TabIndex = 2;
            // 
            // TrialsLabel
            // 
            this.TrialsLabel.AutoEllipsis = true;
            this.TrialsLabel.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TrialsLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.TrialsLabel.Location = new System.Drawing.Point(1, 54);
            this.TrialsLabel.Name = "TrialsLabel";
            this.TrialsLabel.Size = new System.Drawing.Size(69, 13);
            this.TrialsLabel.TabIndex = 1;
            this.TrialsLabel.Text = "Trials :";
            // 
            // StimulusLabel
            // 
            this.StimulusLabel.AutoEllipsis = true;
            this.StimulusLabel.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StimulusLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.StimulusLabel.Location = new System.Drawing.Point(1, -1);
            this.StimulusLabel.Name = "StimulusLabel";
            this.StimulusLabel.Size = new System.Drawing.Size(151, 13);
            this.StimulusLabel.TabIndex = 0;
            this.StimulusLabel.Text = "Stimulus: ";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.CategoryRatioLabel);
            this.panel1.Controls.Add(this.RatioLabel);
            this.panel1.Controls.Add(this.ResponseStabilityLabel);
            this.panel1.Controls.Add(this.ResponseSDLabel);
            this.panel1.Controls.Add(this.RankLabel);
            this.panel1.Controls.Add(this.AvgFRLabel);
            this.panel1.Controls.Add(this.TrialsLabel);
            this.panel1.Controls.Add(this.StimulusLabel);
            this.panel1.Location = new System.Drawing.Point(78, 75);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(157, 70);
            this.panel1.TabIndex = 3;
            // 
            // CategoryRatioLabel
            // 
            this.CategoryRatioLabel.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CategoryRatioLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.CategoryRatioLabel.Location = new System.Drawing.Point(1, 38);
            this.CategoryRatioLabel.Name = "CategoryRatioLabel";
            this.CategoryRatioLabel.Size = new System.Drawing.Size(151, 13);
            this.CategoryRatioLabel.TabIndex = 6;
            this.CategoryRatioLabel.Text = ". / Cat. Best:";
            // 
            // RatioLabel
            // 
            this.RatioLabel.AutoSize = true;
            this.RatioLabel.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RatioLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.RatioLabel.Location = new System.Drawing.Point(1, 25);
            this.RatioLabel.Name = "RatioLabel";
            this.RatioLabel.Size = new System.Drawing.Size(51, 12);
            this.RatioLabel.TabIndex = 5;
            this.RatioLabel.Text = ". / Best:";
            // 
            // ResponseStabilityLabel
            // 
            this.ResponseStabilityLabel.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ResponseStabilityLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ResponseStabilityLabel.Location = new System.Drawing.Point(76, 25);
            this.ResponseStabilityLabel.Name = "ResponseStabilityLabel";
            this.ResponseStabilityLabel.Size = new System.Drawing.Size(80, 13);
            this.ResponseStabilityLabel.TabIndex = 4;
            this.ResponseStabilityLabel.Text = "R. Stab:";
            // 
            // ResponseSDLabel
            // 
            this.ResponseSDLabel.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ResponseSDLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ResponseSDLabel.Location = new System.Drawing.Point(76, 12);
            this.ResponseSDLabel.Name = "ResponseSDLabel";
            this.ResponseSDLabel.Size = new System.Drawing.Size(80, 13);
            this.ResponseSDLabel.TabIndex = 4;
            this.ResponseSDLabel.Text = "R. SD:";
            this.ResponseSDLabel.Click += new System.EventHandler(this.ResponseSDLabel_Click);
            // 
            // RankLabel
            // 
            this.RankLabel.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RankLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.RankLabel.Location = new System.Drawing.Point(1, 12);
            this.RankLabel.Name = "RankLabel";
            this.RankLabel.Size = new System.Drawing.Size(69, 13);
            this.RankLabel.TabIndex = 4;
            this.RankLabel.Text = "Rank :";
            // 
            // AvgFRLabel
            // 
            this.AvgFRLabel.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AvgFRLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.AvgFRLabel.Location = new System.Drawing.Point(64, 53);
            this.AvgFRLabel.Name = "AvgFRLabel";
            this.AvgFRLabel.Size = new System.Drawing.Size(92, 14);
            this.AvgFRLabel.TabIndex = 3;
            this.AvgFRLabel.Text = "Avg F R :";
            // 
            // StimulusPSTH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.MaxmimumFRLabel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.PSTHPictureBox);
            this.Controls.Add(this.StimulusPictureBox);
            this.Name = "StimulusPSTH";
            this.Size = new System.Drawing.Size(240, 150);
            ((System.ComponentModel.ISupportInitialize)(this.StimulusPictureBox)).EndInit();
            this.ExportContextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PSTHPictureBox)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private System.Windows.Forms.Label CategoryRatioLabel;
        private System.Windows.Forms.Label ResponseSDLabel;
        private System.Windows.Forms.Label ResponseStabilityLabel;
        private System.Windows.Forms.ContextMenuStrip ExportContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem exportDescriptivesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportTrialsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportRasterToolStripMenuItem;
    }
}
