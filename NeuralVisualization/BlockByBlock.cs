using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using NeuralData;
using System.Drawing.Drawing2D;

namespace NeuralVisualization
{
    public partial class BlockByBlock : UserControl
    {
        public BlockByBlock()
        {
            InitializeComponent();
        }

        public bool[][][] NeuralSpace;
        private double[][] BlockData;
        private double[][] BlockMeanSE;

        public int ResponseOffset;
        public int ResponseOnset;

        public void Reset()
        {
            NeuralSpace = null;
            BlockData = null;
            BlockMeanSE = null;
            pictureBox1.Image = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (NeuralSpace == null) {MessageBox.Show("Neural Data not available!"); return;}
            int le = 0;
            for (int i = 0; i < NeuralSpace.GetLength(0); i++)
                if (NeuralSpace[i] != null)
                {
                    le = Math.Max(le, NeuralSpace[i].GetLength(0));
                }
            //BlockData = new double[le][];
            //for (int i = 0; i < BlockData.GetLength(0); i++)
            //    BlockData[i] = new double[NeuralSpace.GetLength(0)];
            ClaculateBlockData(le);
            PlotBlockData();
        }

        private void PlotBlockData()
        {
            Pen p = new Pen(Color.Red,15f);
            Pen p2 = new Pen(Color.Blue, 3.5f);
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            
            float ph = pictureBox1.Height - 40.0f;
            float scale = 0.0f;
            for (int i = 0; i < BlockMeanSE.GetLength(0); i++) scale = Math.Max((float)BlockMeanSE[i][0], scale);
            scale =  (float)(ph) / scale;

            for (int i = 0; i < BlockData.GetLength(0); i++)
            {

                float x1 = 30 + i * 20.0f;
                float y1 = ph + 20;

                g.DrawLine(p,x1,y1 - scale* (float)(BlockMeanSE[i][0]),x1,y1+1.0f);
                g.DrawLine(p2, x1, y1 - scale * (float)(BlockMeanSE[i][0] + BlockMeanSE[i][1] * 2.0f), x1, y1 - scale * (float)(BlockMeanSE[i][0] - BlockMeanSE[i][1] * 2.0f));
            }
            g.DrawString( (1.0f / (scale / ph)).ToString(), 
                new Font("Verdana", 8.0f,FontStyle.Regular), 
                new SolidBrush(Color.Black), 10, 10);
            pictureBox1.Image = bmp;
            pictureBox1.Refresh();
        }

        private void ClaculateBlockData(int le)
        {
            BlockData = new double[le][];
            for (int i = 0; i < BlockData.GetLength(0); i++)
                BlockData[i] = new double[NeuralSpace.GetLength(0)];
         
            for (int i = 0; i < NeuralSpace.GetLength(0); i++)
            if(NeuralSpace[i]!=null)
                if(true)
            {
                for (int j = 0; j < NeuralSpace[i].GetLength(0); j++)
                {
                    //if (BlockData[j] == null) BlockData[j] = new double[0];
                    //Array.Resize(ref BlockData[j], BlockData[j].GetLength(0) + 1);
                    for (int k = ResponseOnset; k < ResponseOffset; k++)
                    {
                        if (NeuralSpace[i][j][k])
                        {
                            if(BlockData[j][i]==null) BlockData[j][i] = new double();
                            BlockData[j][i] += 1.0d;
                        }
                    }
                }
            }
            BlockMeanSE = new double[BlockData.GetLength(0)][];
            for (int i = 0; i < BlockData.GetLength(0); i++)
            {
                BlockMeanSE[i] = new double[2];
                BlockMeanSE[i][0] = (1000.0d / (double)(ResponseOffset-ResponseOnset)) * StatisticalTests.Mean(BlockData[i]);
                BlockMeanSE[i][1] = (1000.0d / (double)(ResponseOffset-ResponseOnset)) * ( Math.Sqrt(StatisticalTests.Variance(BlockData[i])))/(double)Math.Sqrt(BlockData[i].GetLength(0)) ;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (NeuralSpace == null) { MessageBox.Show("Neural Data not available!"); return; }
            int le = 0;
            for (int i = 0; i < NeuralSpace.GetLength(0); i++)
                if (NeuralSpace[i] != null)
                {
                    le = Math.Max(le, NeuralSpace[i].GetLength(0));
                }
            BlockData = new double[le][];
            for (int i = 0; i < BlockData.GetLength(0); i++)
                BlockData[i] = new double[NeuralSpace.GetLength(0)];
            ClaculateFaceBlockData();
            PlotBlockData();
        }

        private void ClaculateFaceBlockData()
        {
            for (int i = 0; i < NeuralSpace.GetLength(0); i++)
                if (NeuralSpace[i] != null)
                    if (Config.MapFaceNonFace(i) == 0)
                    {
                        for (int j = 0; j < NeuralSpace[i].GetLength(0); j++)
                        {
                            //if (BlockData[j] == null) BlockData[j] = new double[0];
                            //Array.Resize(ref BlockData[j], BlockData[j].GetLength(0) + 1);
                            for (int k = ResponseOnset; k < ResponseOffset; k++)
                            {
                                if (NeuralSpace[i][j][k])
                                    BlockData[j][i] += 1.0d;
                            }
                        }
                    }
            BlockMeanSE = new double[BlockData.GetLength(0)][];
            for (int i = 0; i < BlockData.GetLength(0); i++)
            {
                BlockMeanSE[i] = new double[2];
                BlockMeanSE[i][0] = (1000.0d / (double)(ResponseOffset - ResponseOnset)) * StatisticalTests.Mean(BlockData[i]);
                BlockMeanSE[i][1] = (1000.0d / (double)(ResponseOffset - ResponseOnset)) * (Math.Sqrt(StatisticalTests.Variance(BlockData[i]))) / (double)Math.Sqrt(BlockData[i].GetLength(0));
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (NeuralSpace == null) { MessageBox.Show("Neural Data not available!"); return; }
            int le = 0;
            for (int i = 0; i < NeuralSpace.GetLength(0); i++)
                if (NeuralSpace[i] != null)
                {
                    le = Math.Max(le, NeuralSpace[i].GetLength(0));
                }
            BlockData = new double[le][];
            for (int i = 0; i < BlockData.GetLength(0); i++)
                BlockData[i] = new double[NeuralSpace.GetLength(0)];
            ClaculateHumanFaceBlockData();
            PlotBlockData();
        }

        private void ClaculateHumanFaceBlockData()
        {

            for (int i = 0; i < NeuralSpace.GetLength(0); i++)
                if (NeuralSpace[i] != null)
                    if (Config.MapCategory(i) == 0)
                    {
                        for (int j = 0; j < NeuralSpace[i].GetLength(0); j++)
                        {
                            //if (BlockData[j] == null) BlockData[j] = new double[0];
                            //Array.Resize(ref BlockData[j], BlockData[j].GetLength(0) + 1);
                            for (int k = ResponseOnset; k < ResponseOffset; k++)
                            {
                                if (NeuralSpace[i][j][k])
                                    BlockData[j][i] += 1.0d;
                            }
                        }
                    }
            BlockMeanSE = new double[BlockData.GetLength(0)][];
            for (int i = 0; i < BlockData.GetLength(0); i++)
            {
                BlockMeanSE[i] = new double[2];
                BlockMeanSE[i][0] = (1000.0d / (double)(ResponseOffset - ResponseOnset)) * StatisticalTests.Mean(BlockData[i]);
                BlockMeanSE[i][1] = (1000.0d / (double)(ResponseOffset - ResponseOnset)) * (Math.Sqrt(StatisticalTests.Variance(BlockData[i]))) / (double)Math.Sqrt(BlockData[i].GetLength(0));
            }
        }
    }
}
