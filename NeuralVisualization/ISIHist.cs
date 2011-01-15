using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace NeuralVisualization
{
    public partial class ISIHist : UserControl
    {
        public ISIHist()
        {
            InitializeComponent();
        }

        public bool[][][] NeuralSpace;
        
        private void button1_Click(object sender, EventArgs e)
        {
            PlotISIHist(CalculateISI(NeuralSpace));
        }


        private double[] CalculateISI(bool[][][] NeuralSpace)
        {
            double[] ISIs = new double[0]; 
            for (int i = 0; i < NeuralSpace.GetLength(0); i++)
            {
                for (int j = 0; j < NeuralSpace[i].GetLength(0); j++)
                {
                    for (int k = 0; k < NeuralSpace[i][j].GetLength(0); k++)
                    {
                        int tk=0;
                        if (NeuralSpace[i][j][k])
                        {
                            Array.Resize(ref ISIs, ISIs.GetLength(0) + 1);
                            ISIs[ISIs.GetLength(0) - 1] = k - tk;
                            tk = k;
                        }
                    }
                }
            }
            return ISIs;
        }

        private void PlotISIHist(double[] ISIs)
        {
        }

        public void Reset()
        {
            NeuralSpace = null;
        }
    }
}
