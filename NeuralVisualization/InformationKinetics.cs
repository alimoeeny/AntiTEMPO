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
    public partial class InformationKinetics : UserControl
    {
        public InformationKinetics()
        {
            InitializeComponent();
        }

        public bool[][][] NeuralSpace;
        private Color BGColor = Color.Khaki;
        private Pen[] LinePens = new Pen[3] { new Pen(Color.Red,2.0f), new Pen(Color.Blue,3.0f), new Pen(Color.Magenta,2.0f)};

        private double[] CalculateInformationData(bool[][][] NeuralSpace,int TrialDuration, int BinSize)
        {
            double[] r;
            int BinCount = (int)Math.Ceiling((double)TrialDuration / BinSize);
            //r = new double[2][];
            //r[0] = new double[BinCount]; // Shannon's Entropy for this neuron
            //r[0] = new double[BinCount]; // Noise Entropy for this neuron
            r = new double[BinCount]; // Mutual Information
         
            for (int i = 0; i < BinCount; i++)
            {
            //   r[0][i] = Information.ShannonsEntropy(NeuralSpace, i * BinSize, (i + 1) * BinSize);
            //    r[0][i] = Information.NoiseEntropy(NeuralSpace, i * BinSize, (i + 1) * BinSize);
            //    r[1][i] = Information.ShannonsEntropy(NeuralSpace, i * BinSize, (i + 1) * BinSize) - r[0][i]; // this is the Mutual information, just to save some CPU cycles and not calculate the noise entropy agian!
                r[i] = Information.MutualInformation(NeuralSpace, i * BinSize, (i + 1) * BinSize);
            }
            return r;
        }

        private void Plot(double[][] Data)
        {
            Point p1 = new Point(0, 0);
            Point p2 = p1;
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.FillRectangle(new SolidBrush(BGColor), new Rectangle(0, 0, bmp.Width, bmp.Height));
            //double max = Math.Max(Math.Max(StatisticalTests.Max(Data[0]),StatisticalTests.Max(Data[1])),StatisticalTests.Max(Data[2]));
            double max = StatisticalTests.Max(Data[0]);
            if(Data.Length>1)
                max = Math.Max(StatisticalTests.Max(Data[0]), StatisticalTests.Max(Data[1]));
            


            for (int i = 0; i < Data.GetLength(0); i++) // Lines
            {
                p1 = new Point(0, bmp.Height);
                for (int j = 0; j < Data[i].GetLength(0); j++)
                {
                    p2 = p1;
                    p1.X = j * bmp.Width / Data[i].GetLength(0);
                    p1.Y = (int)(bmp.Height - Data[i][j] * bmp.Height / max );
                    g.DrawLine(LinePens[i], p1, p2);
                }
            }
            pictureBox1.Image = bmp;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if (radioButton1.Checked)
            {
                Plot(new double[][] { CalculateInformationData(NeuralSpace, 700, Convert.ToInt32(BinsizeComboBox.Text)) });
            }
            else if (radioButton2.Checked)
            { }
            else if (radioButton3.Checked)
            {
                double[][] temp = new double[Config.SuperCategoryColors.Length][];
                int c = 0;
                for (int i = 0; i < Config.SuperCategoryColors.Length; i++)
                {
                    bool[][][] tempSpace = new bool[Config.MapSuperCategoryCount(i)][][];
                    for (int j = 0; j < Config.MapSuperCategoryCount(i); j++)
                    {
                        tempSpace[j] = NeuralSpace[c + j];
                    }
                    c += Config.MapSuperCategoryCount(i);
                    temp[i] = CalculateInformationData(tempSpace, 700, Convert.ToInt32(BinsizeComboBox.Text));
                }
            }
            this.Cursor = Cursors.Default;
        }

        public void Reset()
        {
            NeuralSpace = null;
            pictureBox1.Image = null;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                int TrialDuration = 700;
                int BinSize = Convert.ToInt32(BinsizeComboBox.Text);
                FileStream fs = new FileStream(sfd.FileName, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                int BinCount = (int)Math.Ceiling((double)TrialDuration / BinSize);
                for (int i = 0; i < NeuralSpace.GetLength(0); i++) 
                {
                    sw.Write(Convert.ToString(i) + "\t");
                    for(int j = 0; j< BinCount; j++)
                    {
                        sw.Write(Information.ShannonsEntropy(NeuralSpace[i], j * BinSize, (j + 1) * BinSize).ToString() + "\t");
                    }
                    sw.WriteLine();
                }

                sw.Close();
                fs.Close();
            }
        }

    }
}
