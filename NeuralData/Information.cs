using System;

namespace NeuralData
{
    public static class Information
    {
        public static double ShannonsEntropy(bool[][][] NeuralSpace, int ResponseOnset, int ResponseOffset)
        {
            double r=0;
            int c = 0;
            double[] rates = new double[1];
            for (int i = 0; i < NeuralSpace.GetLength(0); i++)
                if(NeuralSpace[i]!=null)
                for (int j = 0; j < NeuralSpace[i].GetLength(0); j++)
                {
                    if (rates.Length<c+1) Array.Resize(ref rates, c+1);
                    for (int k = 0; k < NeuralSpace[i][j].GetLength(0); k++)
                        if (NeuralSpace[i][j][k] && (k >= ResponseOnset) && (k < ResponseOffset))
                            rates[c]+=1.0d*(1000/(ResponseOffset-ResponseOnset));
                    c++;
                }
            double[] PureRates = StatisticalTests.RemoveRepititions(rates);
            for (int i = 0; i < PureRates.Length; i++)
            {
                double p = StatisticalTests.Probability(rates, rates[i]);
                r += -(p * Math.Log(p, 2.0d));
            }
                return r;
        }

        public static double ShannonsEntropy(bool[][] NeuralResponses, int ResponseOnset, int ResponseOffset)
        {
            bool[][][] NSpace = new bool[1][][];
            NSpace[0] = NeuralResponses;
            return ShannonsEntropy(NSpace, ResponseOnset, ResponseOffset);
        }

        public static double NoiseEntropy(bool[][][] NeuralSpace, int ResponseOnset, int ResponseOffset)
        {
            double r = 0;
            int c = 0;
            for (int i = 0; i < NeuralSpace/*[0]*/.GetLength(0); i++)
            {
                double ps = 1.0d / NeuralSpace.GetLength(0); // Approximate Probability of Stimulus
                double[] rates = new double[1];
                if(NeuralSpace[i]!=null)
                    for (int j = 0; j < NeuralSpace[i].GetLength(0); j++)
                    {
                        if (rates.Length<c+1) Array.Resize(ref rates, c+1);
                        for (int k = 0; k < NeuralSpace[i][j].GetLength(0); k++)
                            if (NeuralSpace[i][j][k] && (k >= ResponseOnset) && (k < ResponseOffset))
                                rates[c]+=1.0d*(1000/(ResponseOffset-ResponseOnset));
                        c++;
                    }
                double[] PureRates = StatisticalTests.RemoveRepititions(rates);
                for(int l = 0; l<PureRates.Length;l++)
                {
                    double prs = StatisticalTests.Probability(rates, rates[l])*ps;
                    r += -(ps* prs * Math.Log(prs, 2.0d));
                }
                
            }
            return r;
            

            
            //bool [][][] tempspace = new bool[1][][];
            //for (int i = 0; i < NeuralSpace.GetLength(0); i++)
            //if(NeuralSpace[i]!=null)
            //    for (int j = 0; j < NeuralSpace[i].GetLength(0); j++)
            //    {
                    
            //        tempspace[0] = NeuralSpace[i];
            //        r += ps * StatisticalTests.Probability()//ShannonsEntropy(tempspace, ResponseOnset, ResponseOffset);
            //    }
            

            //return r;
        }
        
        public static double MutualInformation(bool[][][] NeuralSpace, int ResponseOnset, int ResponseOffset)
        {
            return (ShannonsEntropy(NeuralSpace,ResponseOnset,ResponseOffset) - NoiseEntropy(NeuralSpace,ResponseOnset,ResponseOffset));
        }

        public static double[][][] InfoClustMini(bool[][][] NeuralSpace, ref int[][] Clusters, int ResponseOnset, int ResponseOffset, int ClustersCount, int iterations)
        {
            Clusters = new int[ClustersCount][];
            
            double[][][] InfoLog = new double[ClustersCount][][];
            for (int i = 0; i < ClustersCount; i++)
            {
                InfoLog[i] = new double[iterations][];
                for (int j = 0; j < iterations; j++)
                    InfoLog[i][j] = new double[Convert.ToInt32(Math.Ceiling((double)NeuralSpace.GetLength(0)))];// / ClustersCount) + 45.0d)];
            }
            bool Satisfied = false;
            int iterationcount = 0;
            while (!Satisfied)
            {
                for(int i = 0; i<NeuralSpace.GetLength(0); i++)
                {
                    int clust = 0;
                    double shannonstemp = 0;
                    double minshannon = double.MaxValue;
                    for (int j = 0; j < ClustersCount; j++)
                    {
                        bool[][][] ClusterSpace = new bool[1][][];
                        if (Clusters[j] != null)
                        {
                            ClusterSpace = new bool[Clusters[j].GetLength(0) + 1][][];
                            for (int k = 0; k < Clusters[j].GetLength(0); k++)
                                ClusterSpace[k] = NeuralSpace[Clusters[j][k]];
                            ClusterSpace[Clusters[j].GetLength(0)] = NeuralSpace[i];
                        }
                        else { ClusterSpace[0] = NeuralSpace[i]; }
                        shannonstemp = ShannonsEntropy(ClusterSpace, ResponseOnset, ResponseOffset);
                        if (shannonstemp < minshannon)
                        {
                            clust = j;
                            minshannon = shannonstemp;
                        }
                    }
                    if(Clusters[clust]!=null)
                        Array.Resize(ref Clusters[clust], Clusters[clust].GetLength(0)+1);
                    else 
                        Clusters[clust] = new int[1];// Array.Resize(ref Clusters[clust], 1);
                    for (int jj = 0; jj < ClustersCount; jj++)
                        StatisticalTests.Remove(ref Clusters[jj], i);
                    if ((Clusters[clust] == null) || Clusters[clust].GetLength(0)==0) Clusters[clust] = new int[1];
                    Clusters[clust][Clusters[clust].GetLength(0) - 1] = i;
                    InfoLog[clust][iterationcount][Clusters[clust].GetLength(0) - 1] = minshannon;
                    //movecount++;
                }
                iterationcount++;
                if (iterationcount >= iterations) Satisfied = true;
            }
            return /*StatisticalTests.Squeeze(*/InfoLog;
        }
    }
}