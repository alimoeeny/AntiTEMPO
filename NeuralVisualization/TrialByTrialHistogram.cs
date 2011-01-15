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
    public partial class TrialByTrialHistogram : UserControl
    {
        public bool[][]TrialsData;

        public TrialByTrialHistogram()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PlotTrialByTrial(CalculatePSTHs(TrialsData, Convert.ToInt32(BinsizeComboBox.Text)));
        }

        private void PlotTrialByTrial(double[][] PSTHs)
        {
            Bitmap bmp = new Bitmap(TrialByTrialPictureBox.Width, TrialByTrialPictureBox.Height);
            Graphics g = Graphics.FromImage(bmp);
            float MaxData = 0;
            for (int i = 0; i < PSTHs.GetLength(0); i++)
                MaxData=(float)Math.Max(MaxData, StatisticalTests.Max(PSTHs[i]));
            float yscale = (bmp.Height - 20.0f) / MaxData;
            float xscale = (bmp.Width - 20.0f) / (float)PSTHs[0].GetLength(0);
            for (int i = 0; i < PSTHs.GetLength(0); i++)
            {
                float x1 = 0; float x2 = 0; float y1 = bmp.Height; float y2 = 0;
                Pen p = new Pen(Color.FromArgb((int)decimal.Remainder((PSTHs.GetLength(0)-i) * 10, 255), (int)decimal.Remainder(i * 15, 255), (int)decimal.Remainder(i * 47, 255)));
                if (i == (PSTHs.GetLength(0) - 1)) {p.Color = Color.Black; p.Width = 2.0f;}
                for (int j = 0; j < PSTHs[i].GetLength(0); j++)
                {
                    x2 = j * xscale;
                    y2 = bmp.Height - (float)(PSTHs[i][j] * yscale);
                    g.DrawLine(p, x1, y1, x2, y2);
                    x1 = x2;
                    y1 = y2;
                }
            }
            TrialByTrialPictureBox.Image = bmp;
        }

        private double[][] CalculatePSTHs(bool[][] TrialsData, int BinSize)
        {
            double[][] PSTHs = new double[TrialsData.GetLength(0)+1][];
            if (WindowedRadioButton.Checked)
            {
                for (int i = 0; i < PSTHs.GetLength(0); i++)
                    PSTHs[i] = new double[(int)Math.Ceiling(TrialsData[0].GetLength(0) / (double)BinSize)];
                for (int i = 0; i < TrialsData.GetLength(0); i++)
                    for (int j = 0; j < TrialsData[i].GetLength(0); j++)
                        if (TrialsData[i][j])
                        { 
                            PSTHs[i][j / BinSize]++;
                            PSTHs[PSTHs.GetLength(0) - 1][j / BinSize] += 1.0d / (double)(PSTHs.GetLength(0) - 1.0d);
                        }
            }
            else if (SmoothedRadioButton.Checked)
            {
                for (int i = 0; i < PSTHs.GetLength(0)-1; i++)
                {
                    PSTHs[i] = StatisticalTests.Convolution(TrialsData[i],StatisticalTests.GaussianWindow((int)WindowSizeNumericUpDown.Value));
                }
                for (int i = 0; i < PSTHs.GetLength(0)-1; i++)
                {
                    PSTHs[i] = StatisticalTests.SubArray(PSTHs[i], (int)WindowSizeNumericUpDown.Value/2, 600);
                }
                for (int i = 0; i < PSTHs.GetLength(0) - 1; i++)
                {
                    PSTHs[PSTHs.GetLength(0) - 1] = StatisticalTests.Mean(PSTHs); 
                }
            }
            else if (ISIRadioButton.Checked)
            {
                for (int i = 0; i < PSTHs.GetLength(0); i++)
                    PSTHs[i] = new double[(int)Math.Ceiling(TrialsData[0].GetLength(0) / (double)BinSize)];

                int PreviousSpike = 0;
                double isi = double.MaxValue;
                for (int i = 0; i < TrialsData.GetLength(0); i++)
                    for (int j = 0; j < TrialsData[i].GetLength(0); j++)
                    {
                        if (TrialsData[i][j])
                        {
                            isi = j - PreviousSpike;
                            PreviousSpike = j;
                        }
                        PSTHs[i][j / BinSize] = (1.0d / isi) *1000.0d;
                    }
            }
            return PSTHs;
        }

    }
}
