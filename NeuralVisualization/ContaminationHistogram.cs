using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using NeuralData;

namespace NeuralVisualization
{
    public partial class ContaminationHistogram : UserControl
    {
        public ContaminationHistogram()
        {
            InitializeComponent();
        }

        public bool[][][] NeuralSpace;
        private int[] histdata;
        private void button1_Click(object sender, EventArgs e)
        {
            int c = 0;
            histdata = new int[NeuralSpace.GetLength(0) * NeuralSpace[0].GetLength(0)];
            for (int i = 0; i < NeuralSpace.GetLength(0); i++)
                if(NeuralSpace[i]!=null)
                for (int j = 0; j < NeuralSpace[i].GetLength(0); j++)
                {
                    for (int k = 0; k < 100; k++)//Pre-stimulus
                        if (NeuralSpace[i][j][k]) histdata[c]++;
                    c++;
                }
            PlotHist(histdata,pictureBox1.Width,pictureBox1.Height);
        }

        private Bitmap PlotHist(int[] histdata, int width, int height)
        {
            int[] hist = new int[10];
            double BaselineFiringRate = StatisticalTests.Mean(histdata);
            int max = NeuralData.StatisticalTests.Max(histdata);
            for (int i = 0; i < histdata.Length; i++)
            {
                hist[ histdata[i] * 9 / max ]++;
            }

            Bitmap bmp = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bmp);
            g.FillRectangle(new SolidBrush(Color.Wheat), new Rectangle(0, 0, width, height));
            Pen hp = new Pen(Color.Brown,20.0f);
            Font hf = new Font("Verdana",10.0f,FontStyle.Bold);
            double m = StatisticalTests.Max(hist);
            float h = height - 50.0f;
            for (int i = 0; i < 10; i++)
            {
                float x = i*(width-50)/9 + 25;
                g.DrawLine(hp, x, h + 25, x, h + 25 - ((float)hist[i] * h / (float)m));
                g.DrawString(hist[i].ToString(), hf, new SolidBrush(Color.DarkBlue), x - 10, h + 30 - ((float)hist[i] * h / (float)m) - 30);
                g.DrawString((max*i/9.0f).ToString("G3"), hf, new SolidBrush(Color.DarkBlue), x - 10, h + 30.0f );
            }
            g.DrawLine(new Pen(Color.DarkGray, 3.0f), (float)BaselineFiringRate * (width - 50) / 9 + 25, h + 25, (float)BaselineFiringRate * (width - 50) / 9 + 25, 0);
            g.DrawString("Baseline Firing Rate",hf,new SolidBrush(Color.DarkGray), (float)BaselineFiringRate * (width - 50) / 9 + 25,  10);
            pictureBox1.Image = bmp;
            return bmp;
        }



        public void Reset()
        {
            NeuralSpace = null;
            histdata = null;
        }
    }
}
