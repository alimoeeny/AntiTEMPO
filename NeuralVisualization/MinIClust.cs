using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using NeuralData;
using System.Collections;

namespace NeuralVisualization
{
    public partial class MinIClust : UserControl
    {
        public MinIClust()
        {
            InitializeComponent();
        }

        public bool[][][] NeuralSpace;
        public int ResponseOnset = 0;
        public int ResponseOffset = 0;

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;
                FileStream fs = new FileStream(sfd.FileName, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                int[][] r = new int[1][];
                double[][][] InfoLog = Information.InfoClustMini(NeuralSpace, ref r, ResponseOnset, ResponseOffset, (int)ClusterCountNumericUpDown.Value, (int)IterationsNumericUpDown.Value);

                for (int i = 0; i < r.GetLength(0); i++)
                {
                    sw.Write(i.ToString() + "\t");
                    for (int j = 0; j < r[i].GetLength(0); j++)
                    {
                        sw.Write(r[i][j].ToString() + "\t");
                    }
                    sw.WriteLine();
                }
                sw.WriteLine("Info Log");
                for (int i = 0; i < InfoLog.GetLength(0); i++)
                {
                    sw.WriteLine("Cluster no:" + i.ToString());
                    for (int j = 0; j < InfoLog[i].GetLength(0); j++)
                    {
                        sw.Write(j.ToString()+"\t");
                        for (int k = 0; k < InfoLog[i][j].GetLength(0); k++)
                            sw.Write(InfoLog[i][j][k].ToString() + "\t");
                        sw.WriteLine();
                    }
                    sw.WriteLine();
                }
                sw.Close();
                fs.Close();
                this.Cursor = Cursors.Default;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;
                double[,] treeinfo = StatisticalTests.MinInfoClusterTree(NeuralSpace, ResponseOnset, ResponseOffset);
                FileStream fs = new FileStream(sfd.FileName, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                for (int i = 0; i < treeinfo.GetLength(0); i++)
                {
                    sw.WriteLine(treeinfo[i, 0].ToString() + "\t" + treeinfo[i, 1].ToString() + "\t" + treeinfo[i, 2].ToString());
                }
                sw.Close();
                fs.Close();

                for (int i = 0; i < treeinfo.GetLength(0); i++)
                {
                    treeView1.Nodes.Add(treeinfo[i, 2].ToString());
                }
                this.Cursor = Cursors.Default;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;
                double[,] treeinfo = StatisticalTests.MaxInfoClusterTree(NeuralSpace, ResponseOnset, ResponseOffset);
                FileStream fs = new FileStream(sfd.FileName, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                for (int i = 0; i < treeinfo.GetLength(0); i++)
                {
                    sw.WriteLine(treeinfo[i, 0].ToString() + "\t" + treeinfo[i, 1].ToString() + "\t" + treeinfo[i, 2].ToString());
                }
                sw.Close();
                fs.Close();

                for (int i = 0; i < treeinfo.GetLength(0); i++)
                {
                    treeView1.Nodes.Add(treeinfo[i, 2].ToString());
                }
                this.Cursor = Cursors.Default;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;
                Array clusterinfo = StatisticalTests.GroupsofKMinInfoClustering(NeuralSpace, ResponseOnset, ResponseOffset, (int) KUpToNumericUpDown.Value);
                FileStream fs = new FileStream(sfd.FileName, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                for (int i = 0; i < clusterinfo.GetLength(0); i++)
                {
                    sw.WriteLine((clusterinfo.GetValue(i) as Array).GetValue(0).ToString() + "\t" +(clusterinfo.GetValue(i) as Array).GetValue(1).ToString() + "\t" +(clusterinfo.GetValue(i) as Array).GetValue(2).ToString());// + "\t" + treeinfo[i, 1].ToString() + "\t" + treeinfo[i, 2].ToString());
                }

                
                sw.Close();
                fs.Close();
                this.Cursor = Cursors.Default;
            }

        }
    }
}
