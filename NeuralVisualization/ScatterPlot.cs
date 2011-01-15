using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using NeuralData;
using System.Drawing.Drawing2D;
using System.IO;

namespace NeuralVisualization
{
    public partial class ScatterPlot : UserControl
    {
        public bool[][] XScatterData;
        public bool[][] YScatterData;
        
        public ScatterPlot()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double[,] sdata = CalculateScatterData(XScatterData, YScatterData, (int)ResponseOnsetNumericUpDown1.Value, (int)ResponseOffsetNumericUpDown2.Value);
            double[] cp = StatisticalTests.CorrelationPearsons(sdata);
            Plot(sdata);
            MessageBox.Show("Pearson: [" + cp[0].ToString() + " , " + cp[1].ToString() + "]");
        }

        private double[,] CalculateScatterData(bool[][] XScatterData, bool[][] YScatterData, int ResponseOnset, int ResponseOffset)
        {
            double[,] ScatterData = new double[XScatterData.GetLength(0),2];
            for (int i = 0; i < XScatterData.GetLength(0); i++)
                for (int j = 0; j < XScatterData[i].GetLength(0); j++)
                {
                    if ((j > ResponseOnset) && (j < ResponseOffset))
                    {
                        if (XScatterData[i][j]) ScatterData[i, 0]++;
                        if (YScatterData[i][j]) ScatterData[i, 1]++;
                    }
                }
            
            return ScatterData;
        }

        private void Plot(double[,] ScatterData)
        {
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            float MaxX = (float)StatisticalTests.Max(ScatterData, 0);
            float MaxY = (float)StatisticalTests.Max(ScatterData, 1);
            float MaxXY = Math.Max(MaxX, MaxY);
            float Xscale = (bmp.Width - 20.0f) / MaxXY;
            float Yscale = (bmp.Height - 20.0f) / MaxXY;
            for (int i = 0; i < ScatterData.GetLength(0); i++)
            {
                float x = 10.0f + (float)ScatterData[i, 0] * Xscale;
                float y = (bmp.Height - 10.0f) - (float)ScatterData[i, 1] * Yscale;

                g.FillEllipse(new SolidBrush(Color.Red), x, y, 4.0f, 4.0f);
            }
            g.DrawString("Blank", new Font("Verdana", 8.0f, FontStyle.Regular), new SolidBrush(Color.Black), bmp.Width - 40.0f, bmp.Height - 15.0f);
            g.DrawString("Pre-Blank", new Font("Verdana", 8.0f,FontStyle.Regular), new SolidBrush(Color.Black), 7.0f, 5.0f);
            g.DrawLine(new Pen(new HatchBrush(HatchStyle.SolidDiamond,Color.Gray)),10.0f,bmp.Height-10.0f,10.0f+Xscale*MaxXY,bmp.Height - (Yscale*MaxXY)); 
            
            pictureBox1.Image = bmp;
        }

        
        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(sfd.FileName+".txt", FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                double[,] data = CalculateScatterData(XScatterData, YScatterData, (int)ResponseOnsetNumericUpDown1.Value, (int)ResponseOffsetNumericUpDown2.Value);
                for (int i = 0; i < data.GetLength(0); i++)
                {
                    for (int j = 0; j < data.GetLength(1); j++)
                        sw.Write(data[i, j].ToString("G3") + "\t");
                    sw.WriteLine();
                }
                sw.Close();
                fs.Close();
            }
        }
    }
}
