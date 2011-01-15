using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Collections;
using NeuralData;

namespace NeuralVisualization
{
    public partial class SelectivityProfile : UserControl
    {
        public SelectivityProfile()
        {
            InitializeComponent();
            ZeroPoint = new Point(20, pictureBox1.Height - 20);
        }

        public bool[][][] NeuralSpace; // Total 321 Stimuli, max 15 times presentation , trial duration sampels
        public string NeuronName = "";
        public string BrainArea = "";
        private int NormalizationMethod = 2; // 1 to categoryBest,  2 to Bests Stimulus

        private double BestStimulusFiringRate = 0;
        private Pen AxesPen = new Pen(Color.Black, 3.0f);
        private Point ZeroPoint;
        private Pen Cat1Pen = new Pen(Color.Red, 1.0f);
        private Color GraphBGColor = Color.FromArgb(240, 240, 240);
        private double MaxData;
        private int[] PSTHSortedIndices = new int[321]; // Just to keep the Ranks 
        private double[] PSTHAverageFiringRates = new double[321];
        public int TrialDuration = 700;
        public int ResponseOnset = 0;
        public int ResponseOffset = 0;

        private void PlotAxes(Graphics g)
        {
            g.DrawLine(AxesPen, ZeroPoint.X, ZeroPoint.Y, ZeroPoint.X, 0);
            g.DrawLine(AxesPen, ZeroPoint.X, ZeroPoint.Y, pictureBox1.Width, ZeroPoint.Y);
            
            for (int i = 1; i < 10; i++)
            {
                g.DrawLine(new Pen(new HatchBrush(HatchStyle.Vertical, Color.DarkRed, GraphBGColor), 0.3f), 
                    20,                         pictureBox1.Height - (20 + (i * (pictureBox1.Height - 40.0f) / 10.0f)), 
                    pictureBox1.Width - 20.0f,  pictureBox1.Height - (20 + (i * (pictureBox1.Height - 40.0f) / 10.0f)) );
                g.DrawString((i * 10).ToString() + @" %", new Font("Verdana", 8), new SolidBrush(Color.DarkBlue), 
                    pictureBox1.Width - 20, pictureBox1.Height - (25 + (i * (pictureBox1.Height - 40.0f) / 10.0f)));
            }
            g.DrawLine(new Pen(new HatchBrush(HatchStyle.Vertical, Color.DarkRed, GraphBGColor), 0.3f), pictureBox1.Width / 2, 20, pictureBox1.Width / 2, pictureBox1.Height - 20);
        }

        private void PlotLineGraph(Graphics g, double[] LineData, Color c)
        {
            Cat1Pen = new Pen(c, 2.0f);
            Point p1, p2;
            p1 = new Point(20, 20);//ZeroPoint;
            if ((p1.Y > 1000) || (p1.Y < 0)) p1.Y = 0;
            for (int i = 0; i < LineData.GetLength(0); i++)
            {
                p2 = p1;
                p1 = new Point((int)(ZeroPoint.X + i * ((pictureBox1.Width - 20.0f) / LineData.GetLength(0))), (pictureBox1.Height - 20) - (int)((pictureBox1.Height -40) * LineData[i] / MaxData));
                if ((p1.Y > 1000) || (p1.Y < 0)) p1.Y = 0;
                g.DrawLine(Cat1Pen, p1, p2);
            }
        }

        public void Plot(double[][] SuperCSIndices, int level)
        {
            Bitmap backbuff = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(backbuff);
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.CompositingQuality = CompositingQuality.HighQuality;
            
            g.FillRectangle(new SolidBrush(GraphBGColor), 0, 0, pictureBox1.Width, pictureBox1.Height);
            PlotAxes(g);
            PlotSettings(g);
            MaxData = 1;

            //for (int i = 0; i < SuperCSIndices.GetLength(0); i++)
            //        MaxData = StatisticalTests.Max(SuperCSIndices[i]);
            splitContainer1.Panel2.Controls.Clear();
            for (int i = 0; i < SuperCSIndices.GetLength(0); i++)
            {
                Array.Sort(SuperCSIndices[i],new mySorterClass());
                if (NormalizationMethod==1)
                    PlotLineGraph(g, StatisticalTests.Normalize2Max(SuperCSIndices[i]) , Config.SuperCategoryColors[i]);
                else if (NormalizationMethod == 2)
                    PlotLineGraph(g, StatisticalTests.Normalize(SuperCSIndices[i],BestStimulusFiringRate), ((level>2)?Config.CategoryColors[i]:Config.SuperCategoryColors[i]));
                Label l = new Label();
                l.ForeColor = ((level > 2) ? Config.CategoryColors[i] : Config.SuperCategoryColors[i]);
                l.Font = new Font(l.Font, FontStyle.Bold);
                l.Text = ((level>2)?Config.MapCategoryLabels(i) :Config.MapSuperCategoryLabels(i));
                l.AutoSize = true;
                l.Top = 40 + i * 25;
                splitContainer1.Panel2.Controls.Add(l);

            }

            pictureBox1.Image = backbuff;
        }

        private void PlotSettings(Graphics g)
        {
            Brush sb = new SolidBrush(Color.DarkBlue);
            Font sf = new Font("Verdana", 8);
            g.DrawString(NeuronName, sf, sb, pictureBox1.Width - 200, 20);
            g.DrawString(BrainArea, sf, sb, pictureBox1.Width - 200, 35);
//            g.DrawString("Bin size : " + BinsizeComboBox.Text + " ms", sf, sb, pictureBox1.Width - 100, 50);

        }


        //private double[][] CalculateSuperCategorySelectivity(bool[][][] NeuralSpace,int level)
        //{
        //    PSTHAverageFiringRates= new double[NeuralSpace.GetLength(0)];
        //    double [][] SuperCSIndices = new double[11][];
        //    for (int i = 0; i < NeuralSpace.GetLength(0); i++)  // Stimulus Counter
        //    {
        //        if(NeuralSpace[i]!=null)
        //        for (int j = 0; j < NeuralSpace[i].GetLength(0); j++)// Repetitions
        //        {
        //            for (int k = 0; k < TrialDuration; k++) // Time
        //            {
        //                if (k >= ResponseOnset & k < ResponseOffset)
        //                {
        //                    PSTHAverageFiringRates[i] += Convert.ToInt16(NeuralSpace[i][j][k]);
        //                }
        //            }
        //        }
        //    }

        //    BestStimulusFiringRate = 0;

        //    for (int i = 0; i < NeuralSpace.GetLength(0); i++)
        //        if (NeuralSpace[i] != null)
        //        {
        //            PSTHAverageFiringRates[i] = PSTHAverageFiringRates[i] * (1000 / (ResponseOffset - ResponseOnset)) / NeuralSpace[i].GetLength(0);
        //            BestStimulusFiringRate = Math.Max(BestStimulusFiringRate, PSTHAverageFiringRates[i]);
        //        }

        //    for (int i = 0; i < 321; i++)
        //    {
        //        PSTHSortedIndices[i] = i;
        //    }
        //    //Array.Sort(PSTHAverageFiringRates, PSTHSortedIndices, new mySorterClass());

        //    SuperCSIndices[0] = new double[45];
        //    SuperCSIndices[1] = new double[45];
        //    SuperCSIndices[2] = new double[45];
        //    SuperCSIndices[3] = new double[45];
        //    SuperCSIndices[4] = new double[45];
        //    SuperCSIndices[5] = new double[15];
        //    SuperCSIndices[6] = new double[5];
        //    SuperCSIndices[7] = new double[15];
        //    SuperCSIndices[8] = new double[20];
        //    SuperCSIndices[9] = new double[60];
        //    SuperCSIndices[10] = new double[60]; // it is just the blank

        //    int[] catcount = new int[11];

        //    for (int i = 0; i < PSTHAverageFiringRates.GetLength(0); i++)
        //    {
        //        if (Config.MapSuperCategory(i) > -1)
        //        {
        //            SuperCSIndices[Config.MapSuperCategory(i)][catcount[Config.MapSuperCategory(i)]] = PSTHAverageFiringRates[i];
        //            catcount[Config.MapSuperCategory(i)]++;
        //        }
        //    }

        //    return SuperCSIndices;
        //}

        private double[][] CalculateSelectivity(bool[][][] NeuralSpace, int level)
        {
            return CalculateSelectivity(NeuralSpace, level, 0, TrialDuration);
        }

        private double[][] CalculateSelectivity(bool[][][] NeuralSpace, int level, int begin, int end)
        {
            PSTHAverageFiringRates = new double[NeuralSpace.GetLength(0)];
            for (int i = 0; i < NeuralSpace.GetLength(0); i++)  // Stimulus Counter
            {
                if (NeuralSpace[i] != null)
                    for (int j = 0; j < NeuralSpace[i].GetLength(0); j++)// Repetitions
                    {
                        for (int k = begin; k < end; k++) // Time
                        {
                            if (k >= begin & k < end & NeuralSpace[i][j][k])
                            {
                                PSTHAverageFiringRates[i] ++;//= Convert.ToInt16(NeuralSpace[i][j][k]);
                            }
                        }
                    }
            }

            BestStimulusFiringRate = 0;

            for (int i = 0; i < NeuralSpace.GetLength(0); i++)
                if (NeuralSpace[i] != null)
                {
                    PSTHAverageFiringRates[i] = PSTHAverageFiringRates[i] * (1000 / (end - begin)) / NeuralSpace[i].GetLength(0);
                    BestStimulusFiringRate = Math.Max(BestStimulusFiringRate, PSTHAverageFiringRates[i]);
                }

            for (int i = 0; i < 321; i++)
            {
                PSTHSortedIndices[i] = i;
            }
            //Array.Sort(PSTHAverageFiringRates, PSTHSortedIndices, new mySorterClass());
            int[] catcount = new int[0];
            double[][] Indices=new double[0][];
            switch (level)
            {
                case 2://super categories
                    Indices = new double[Config.SuperCategoryCount][];
                    for (int i = 0; i < Config.SuperCategoryCount; i++)
                        Indices[i] = new double[0];
                    catcount = new int[Config.SuperCategoryCount];
                    for (int i = 0; i < PSTHAverageFiringRates.GetLength(0); i++)
                        if (Config.MapSuperCategory(i) > -1)
                        {
                            Array.Resize(ref Indices[Config.MapSuperCategory(i)], catcount[Config.MapSuperCategory(i)]+1);
                            Indices[Config.MapSuperCategory(i)][catcount[Config.MapSuperCategory(i)]] = PSTHAverageFiringRates[i];
                            catcount[Config.MapSuperCategory(i)]++;
                        }
                    break;
                case 3: // basic categories
                    Indices = new double[Config.CategoryCount][];
                    for (int i = 0; i < Config.CategoryCount; i++ )
                        Indices[i] = new double[0];
                    catcount = new int[Config.CategoryCount];
                    for (int i = 0; i < PSTHAverageFiringRates.GetLength(0); i++)
                        if (Config.MapCategory(i) > -1)
                        {
                            Array.Resize(ref Indices[Config.MapCategory(i)], catcount[Config.MapCategory(i)]+1);
                            Indices[Config.MapCategory(i)][catcount[Config.MapCategory(i)]] = PSTHAverageFiringRates[i];
                            catcount[Config.MapCategory(i)]++;
                        }
                    break;
            }

            return Indices;
        }

    
    
        private void button1_Click_1(object sender, EventArgs e)
        {
            if(radioButton1.Checked)
                Plot(CalculateSelectivity(NeuralSpace,1),1);
            else if(radioButton2.Checked)
                Plot(CalculateSelectivity(NeuralSpace,2), 2);
            else if(radioButton3.Checked)
                Plot(CalculateSelectivity(NeuralSpace,3), 3);

            label1.Text = "Sparseness Index: " + StatisticalTests.SparsenessIndex(NeuralSpace,ResponseOnset, ResponseOffset).ToString("G4") + " Excluding Blank\r\n";
            label1.Text += "Categorical Sp. Index: " + StatisticalTests.SparsenessIndexCategorical(NeuralSpace, 3,ResponseOnset, ResponseOffset).ToString("G4") + "\r\n";
            label1.Text += "SuperCategorical Sp. Index: " + StatisticalTests.SparsenessIndexCategorical(NeuralSpace, 2,ResponseOnset, ResponseOffset).ToString("G4") + "\r\n";

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image.Save(saveFileDialog1.FileName);
            }
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            NormalizationMethod = 2;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            NormalizationMethod = 1;
        }

        
        public void Reset()
        {
            NeuralSpace = null;
            Bitmap backbuff = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(backbuff);
            g.FillRectangle(new SolidBrush(GraphBGColor), 0, 0, pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = backbuff;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                PlotTemporalSelectivity(CalculateTemporalSelectivity(NeuralSpace, 1));
            if (radioButton2.Checked)
                PlotTemporalSelectivity(CalculateTemporalSelectivity(NeuralSpace, 2));
            if (radioButton3.Checked)
                PlotTemporalSelectivity(CalculateTemporalSelectivity(NeuralSpace, 3));
        }

        private void PlotTemporalSelectivity(double[][] TempSel)
        {
            Bitmap backbuff = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(backbuff);
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.CompositingQuality = CompositingQuality.HighQuality;

            g.FillRectangle(new SolidBrush(GraphBGColor), 0, 0, pictureBox1.Width, pictureBox1.Height);
            PlotAxes(g);
            PlotSettings(g);
            MaxData = 1;
            for (int i = 0; i < TempSel.GetLength(0); i++)
                    MaxData = Math.Max(StatisticalTests.Max(TempSel[i]),MaxData);
            float scale = (float)pictureBox1.Height / (float)MaxData;
            float binwidth = (float)pictureBox1.Width / (float)TempSel.GetLength(0);
        
            for (int i = 0; i < TempSel.GetLength(0); i++)
            {
                for (int j = 0; j < TempSel[i].GetLength(0); j++)
                {
                    g.DrawLine(new Pen(Config.CategoryColors[j],3.0f), (float)(i * binwidth + 25), (float)TempSel[i][j] * scale, (i + 1) * binwidth + 25, (float)TempSel[i][j] * scale);
                }
            }


            //for (int i = 0; i < SuperCSIndices.GetLength(0); i++)
            //        MaxData = StatisticalTests.Max(SuperCSIndices[i]);
            //splitContainer1.Panel2.Controls.Clear();
            //for (int i = 0; i < SuperCSIndices.GetLength(0); i++)
            //{
            //    Array.Sort(SuperCSIndices[i], new mySorterClass());
            //    if (NormalizationMethod == 1)
            //        PlotLineGraph(g, StatisticalTests.Normalize2Max(SuperCSIndices[i]), Config.SuperCategoryColors[i]);
            //    else if (NormalizationMethod == 2)
            //        PlotLineGraph(g, StatisticalTests.Normalize(SuperCSIndices[i], BestStimulusFiringRate), ((level > 2) ? Config.CategoryColors[i] : Config.SuperCategoryColors[i]));
            //    Label l = new Label();
            //    l.ForeColor = ((level > 2) ? Config.CategoryColors[i] : Config.SuperCategoryColors[i]);
            //    l.Font = new Font(l.Font, FontStyle.Bold);
            //    l.Text = ((level > 2) ? Config.MapCategoryLabels(i) : Config.MapSuperCategoryLabels(i));
            //    l.AutoSize = true;
            //    l.Top = 40 + i * 25;
            //    splitContainer1.Panel2.Controls.Add(l);

            //}

            pictureBox1.Image = backbuff;
        }

        private double[][] CalculateTemporalSelectivity(bool[][][] NeuralSpace, int level)
        {
            int BinSize = Convert.ToInt32(BinsizeComboBox.Text);
            double[][] TempSel = new double[(int)Math.Ceiling((double)TrialDuration / BinSize)][];

            for (int i = 0; i < TempSel.GetLength(0); i++)
            {
                TempSel[i]= StatisticalTests.Mean(CalculateSelectivity(NeuralSpace, level, i*BinSize, (i+1)*BinSize),0);
            }


            return TempSel;
        }

 
    }
    public class mySorterClass : IComparer
    {
        int IComparer.Compare(Object x, Object y)
        {
            return ((new System.Collections.Comparer(System.Globalization.CultureInfo.CurrentCulture)).Compare(y, x));
        }

    }

}