namespace StimulusPSTH
{
    partial class RasterForm
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
            this.RasterPictureBox = new System.Windows.Forms.PictureBox();
            this.StimulusPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.RasterPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StimulusPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // RasterPictureBox
            // 
            this.RasterPictureBox.Location = new System.Drawing.Point(3, 322);
            this.RasterPictureBox.Name = "RasterPictureBox";
            this.RasterPictureBox.Size = new System.Drawing.Size(700, 250);
            this.RasterPictureBox.TabIndex = 0;
            this.RasterPictureBox.TabStop = false;
            // 
            // StimulusPictureBox
            // 
            this.StimulusPictureBox.BackColor = System.Drawing.Color.Silver;
            this.StimulusPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StimulusPictureBox.Location = new System.Drawing.Point(3, 3);
            this.StimulusPictureBox.Name = "StimulusPictureBox";
            this.StimulusPictureBox.Size = new System.Drawing.Size(313, 313);
            this.StimulusPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.StimulusPictureBox.TabIndex = 1;
            this.StimulusPictureBox.TabStop = false;
            // 
            // RasterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(707, 576);
            this.Controls.Add(this.StimulusPictureBox);
            this.Controls.Add(this.RasterPictureBox);
            this.Name = "RasterForm";
            this.Text = "RasterForm";
            ((System.ComponentModel.ISupportInitialize)(this.RasterPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StimulusPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PictureBox RasterPictureBox;
        public System.Windows.Forms.PictureBox StimulusPictureBox;

    }
}