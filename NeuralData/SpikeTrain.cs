using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralData
{
    public class SpikeTrain
    {

        public static double[] FiringRates(Neuron[] Neurons)
        {
            double[] r = new double[Neurons.GetLength(0)];
            int c=0;
            foreach (Neuron N in Neurons)
            {
                int tc = 0;
                double[] temp = new double[0];
                for (int i = 0; i < N.ResponseSpace.GetLength(0); i++)
                    if (N.ResponseSpace[i] != null)
                    {
                        Array.Resize(ref temp, tc + 1);
                        temp[tc] = StatisticalTests.FiringRate(N.ResponseSpace[i], 0, 700);
                        tc++;
                    }
                r[c] = StatisticalTests.Mean(temp);
                c++;
            }

            return r;
        }

        public static double[][,] CalculatePSTH(bool[][][] NeuralSpace, int level, int BinSize, int TrialDuration, bool CalculateAveragePSTH, bool CalculateCumulative, bool OnlySignificant)
        {
            int PSTHBins = (int)Math.Ceiling((double)TrialDuration / BinSize);
            double[,] AveragePSTHtemp = new double[PSTHBins, 3];
            int StimulusCount = 0;
            double[][] PSTH = new double[NeuralSpace.GetLength(0)][];
            for (int i = 0; i < NeuralSpace.GetLength(0); i++)  // Stimulus Counter
            {
                if (NeuralSpace[i] != null)
                {
                    StimulusCount++;
                    PSTH[i] = new double[(int)Math.Ceiling((double)TrialDuration / (double)BinSize)];
                    for (int j = 0; j < NeuralSpace[i].GetLength(0); j++)// Repetitions
                        for (int k = 0; k < TrialDuration; k++) // Time
                            if (NeuralSpace[i][j][k])
                                PSTH[i][k / BinSize]++;
                }
            }

            // We define a threshold for the significant responses
            double Threshold = double.MinValue;
            if (OnlySignificant)
            {
                for (int i = 0; i < PSTH.GetLength(0); i++)
                    if (PSTH[i] != null) Threshold = Math.Max(Threshold, StatisticalTests.Mean(StatisticalTests.SubArray(PSTH[i], 7, 10)));
                Threshold /= 5.0d;
            }
            if (CalculateAveragePSTH)
            {
                //double[] T = new double[StimulusCount];
                //for (int i = 0; i < PSTH[0].GetLength(0); i++)
                //{
                //    int c = 0;
                //    for (int j = 0; j < PSTH.GetLength(0); j++)
                //    {
                //        if (PSTH[j] != null)
                //        {
                //            T[c] = PSTH[j][i] * (1000.0d / (double)BinSize)/ (double)StimulusCount;
                //            c++;
                //        }
                //    }
                //    AveragePSTHtemp[i, 0] = StatisticalTests.Mean(T);
                //    AveragePSTHtemp[i, 1] = StatisticalTests.StandardError(T); //(Math.Sqrt(StatisticalTests.Variance(T)) / (T.GetLength(0) - 1.0d)) * (1000.0f / BinSize) / Math.Sqrt((NeuralSpace[i].GetLength(0)));
                //    AveragePSTHtemp[i, 2] = 1;
                //}
            }

            //double BlankCategoryAverage = 0;
            double[][,] r = new double[1][,];
            double[][][] rANOVA = new double[1][][];
            double[][][] seTemp1 = new double[1][][];
            int cc = 0;


            switch (level)
            {
                case 1:
                    #region face nonface
                    cc = 3;
                    r = new double[cc + 1][,];
                    rANOVA = new double[cc][][];
                    seTemp1 = new double[cc][][];
                    for (int i = 0; i < cc; i++)
                    {
                        r[i] = new double[(int)Math.Ceiling((double)TrialDuration / BinSize), 3];
                        rANOVA[i] = new double[(int)Math.Ceiling((double)TrialDuration / BinSize)][];
                        seTemp1[i] = new double[(int)Math.Ceiling((double)TrialDuration / BinSize)][];
                    }

                    //This is loop is used to count the actual number of members inside a category (as we have changed the category member counts after we recorded some neurons)
                    int[] FaceNonFaceCount = new int[Config.FaceNonFaceCount];
                    for (int i = 0; i < PSTH.GetLength(0); i++)
                        if (PSTH[i] != null)
                            for (int j = 0; j < NeuralSpace[i].GetLength(0); j++)
                                if ((Config.MapFaceNonFace(i) > -1) & (StatisticalTests.Mean(StatisticalTests.SubArray(PSTH[i], 7, 10)) > Threshold))
                                    FaceNonFaceCount[Config.MapFaceNonFace(i)]++;

                    for (int i = 0; i < NeuralSpace.GetLength(0); i++)
                        if (PSTH[i] != null)
                            for (int j = 0; j < PSTH[i].Length; j++)
                                if ((Config.MapFaceNonFace(i) > -1) & (StatisticalTests.Mean(StatisticalTests.SubArray(PSTH[i], 7, 10)) > Threshold))
                                {
                                    PSTH[i][j] = PSTH[i][j] * (1000.0d / (double)BinSize);// / (double)NeuralSpace[i].GetLength(0) ;
                                    r[Config.MapFaceNonFace(i)][j, 0] += PSTH[i][j] / (double)FaceNonFaceCount[Config.MapFaceNonFace(i)];
                                    if (rANOVA[Config.MapFaceNonFace(i)][j] == null) rANOVA[Config.MapFaceNonFace(i)][j] = new double[0];
                                    Array.Resize(ref rANOVA[Config.MapFaceNonFace(i)][j], rANOVA[Config.MapFaceNonFace(i)][j].GetLength(0) + 1);
                                    if (seTemp1[Config.MapFaceNonFace(i)][j] == null) seTemp1[Config.MapFaceNonFace(i)][j] = new double[0];
                                    Array.Resize(ref seTemp1[Config.MapFaceNonFace(i)][j], seTemp1[Config.MapFaceNonFace(i)][j].GetLength(0) + 1);
                                    seTemp1[Config.MapFaceNonFace(i)][j][seTemp1[Config.MapFaceNonFace(i)][j].GetLength(0) - 1] = r[Config.MapFaceNonFace(i)][j, 0];
                                }

                    // Standard error of mean 
                    for (int i = 0; i < seTemp1.GetLength(0); i++)
                        for (int j = 0; j < seTemp1[0].GetLength(0); j++)
                            r[i][j, 1] = StatisticalTests.StandardError(seTemp1[i][j]); //Math.Sqrt((seTemp1[i][j] - (Math.Pow(seTemp2[i][j], 2) / (double)FaceNonFaceCount[Config.MapFaceNonFace(i)])) / (FaceNonFaceCount[Config.MapFaceNonFace(i)] - 1.0f))/(double)Math.Sqrt(FaceNonFaceCount[Config.MapFaceNonFace(i)]);
                    #endregion
                    break;

                case 2:
                    #region Supercategories
                    cc = 11;
                    r = new double[cc + 1][,];
                    rANOVA = new double[cc][][];
                    seTemp1 = new double[cc][][];
                    for (int i = 0; i < cc; i++)
                    {
                        r[i] = new double[(int)Math.Ceiling((double)TrialDuration / BinSize), 3];//;new double[NeuralSpace[0][0].GetLength(0) / BinSize, 2];
                        rANOVA[i] = new double[(int)Math.Ceiling((double)TrialDuration / BinSize)][];
                        seTemp1[i] = new double[(int)Math.Ceiling((double)TrialDuration / BinSize)][];
                    }
                    //This is loop is used to count the actual number of members inside a Super category (as we have changed the category member counts after we recorded some neurons)
                    int[] SuperCategoryCount = new int[Config.SuperCategoryCount];
                    for (int i = 0; i < PSTH.GetLength(0); i++)
                        if (NeuralSpace[i] != null)
                            if (StatisticalTests.Mean(StatisticalTests.SubArray(PSTH[i], 7, 10)) > Threshold)
                                for (int j = 0; j < NeuralSpace[i].GetLength(0); j++)
                                    if (Config.MapSuperCategory(i) > -1)
                                        SuperCategoryCount[Config.MapSuperCategory(i)]++;
                    for (int i = 0; i < NeuralSpace.GetLength(0); i++)
                        if (PSTH[i] != null)
                            if (StatisticalTests.Mean(StatisticalTests.SubArray(PSTH[i], 7, 10)) > Threshold)
                                for (int j = 0; j < PSTH[i].Length; j++)
                                    if (Config.MapSuperCategory(i) > -1)
                                    {
                                        PSTH[i][j] = PSTH[i][j] * (1000.0d / (double)BinSize);/// (double)NeuralSpace[i].GetLength(0);
                                        r[Config.MapSuperCategory(i)][j, 0] += PSTH[i][j] / (double)SuperCategoryCount[Config.MapSuperCategory(i)];
                                        if (rANOVA[Config.MapSuperCategory(i)][j] == null) rANOVA[Config.MapSuperCategory(i)][j] = new double[0];
                                        Array.Resize(ref rANOVA[Config.MapSuperCategory(i)][j], rANOVA[Config.MapSuperCategory(i)][j].GetLength(0) + 1);
                                        if (seTemp1[Config.MapSuperCategory(i)][j] == null) seTemp1[Config.MapSuperCategory(i)][j] = new double[0];
                                        Array.Resize(ref seTemp1[Config.MapSuperCategory(i)][j], seTemp1[Config.MapSuperCategory(i)][j].GetLength(0) + 1);
                                        seTemp1[Config.MapSuperCategory(i)][j][seTemp1[Config.MapSuperCategory(i)][j].GetLength(0) - 1] = r[Config.MapSuperCategory(i)][j, 0];
                                    }
                    // Standard error of mean 
                    for (int i = 0; i < seTemp1.GetLength(0); i++)
                        for (int j = 0; j < seTemp1[0].GetLength(0); j++)
                            r[i][j, 1] = StatisticalTests.StandardError(seTemp1[i][j]);//Math.Sqrt((seTemp1[i][j] - (Math.Pow(seTemp2[i][j], 2) / (double)SuperCategoryCount[Config.MapSuperCategory(i)])) / ((double)SuperCategoryCount[Config.MapSuperCategory(i)] - 1.0f)) / (double)Math.Sqrt((double)SuperCategoryCount[Config.MapSuperCategory(i)]);    
                    #endregion
                    break;

                case 3:
                    #region all categories
                    cc = 21;
                    r = new double[cc + 1][,];
                    rANOVA = new double[cc][][];
                    seTemp1 = new double[cc][][];
                    for (int i = 0; i < cc; i++)
                    {
                        r[i] = new double[(int)Math.Ceiling((double)TrialDuration / BinSize), 3];//;new double[NeuralSpace[0][0].GetLength(0) / BinSize, 2];
                        rANOVA[i] = new double[(int)Math.Ceiling((double)TrialDuration / BinSize)][];
                        seTemp1[i] = new double[(int)Math.Ceiling((double)TrialDuration / BinSize)][];
                    }
                    //This is loop is used to count the actual number of members inside a category (as we have changed the category member counts after we recorded some neurons)
                    int[] CategoryCount = new int[Config.CategoryCount];
                    for (int i = 0; i < PSTH.GetLength(0); i++)
                        if (NeuralSpace[i] != null)
                            for (int j = 0; j < NeuralSpace[i].GetLength(0); j++)
                                if ((Config.MapCategory(i) > -1) & (StatisticalTests.Mean(StatisticalTests.SubArray(PSTH[i], 7, 10)) > Threshold))
                                    CategoryCount[Config.MapCategory(i)]++;
                    for (int i = 0; i < PSTH.GetLength(0); i++)
                        if (PSTH[i] != null)
                            for (int j = 0; j < PSTH[i].Length; j++)
                                if ((Config.MapCategory(i) > -1) & (StatisticalTests.Mean(StatisticalTests.SubArray(PSTH[i], 7, 10)) > Threshold))
                                {
                                    PSTH[i][j] = PSTH[i][j] * (1000.0d / (double)BinSize);/// (double)NeuralSpace[i].GetLength(0);
                                    r[Config.MapCategory(i)][j, 0] += PSTH[i][j] / (double)CategoryCount[Config.MapCategory(i)];
                                    if (rANOVA[Config.MapCategory(i)][j] == null) rANOVA[Config.MapCategory(i)][j] = new double[0];
                                    Array.Resize(ref rANOVA[Config.MapCategory(i)][j], rANOVA[Config.MapCategory(i)][j].GetLength(0) + 1);
                                    rANOVA[Config.MapCategory(i)][j][rANOVA[Config.MapCategory(i)][j].GetLength(0) - 1] = PSTH[i][j] / (double)CategoryCount[Config.MapCategory(i)];
                                    if (seTemp1[Config.MapCategory(i)][j] == null) seTemp1[Config.MapCategory(i)][j] = new double[0];
                                    Array.Resize(ref seTemp1[Config.MapCategory(i)][j], seTemp1[Config.MapCategory(i)][j].GetLength(0) + 1);
                                    seTemp1[Config.MapCategory(i)][j][seTemp1[Config.MapCategory(i)][j].GetLength(0) - 1] = r[Config.MapCategory(i)][j, 0];
                                }

                    // Standard error of mean 
                    for (int i = 0; i < seTemp1.GetLength(0); i++)
                        for (int j = 0; j < seTemp1[0].GetLength(0); j++)
                            r[i][j, 1] = StatisticalTests.StandardError(seTemp1[i][j]);//Math.Sqrt((seTemp1[i][j] - (Math.Pow(seTemp2[i][j], 2) / (double)CategoryCount[Config.MapCategory(i)])) / (CategoryCount[Config.MapCategory(i)] - 1.0f)) /(double)Math.Sqrt(CategoryCount[Config.MapCategory(i)]);
                    #endregion
                    break;

                case 4:
                    #region face vs. simple shapes
                    cc = 8;
                    r = new double[cc + 1][,];
                    rANOVA = new double[cc][][];
                    seTemp1 = new double[cc][][];
                    for (int i = 0; i < cc; i++)
                    {
                        r[i] = new double[(int)Math.Ceiling((double)TrialDuration / BinSize), 3];//;new double[NeuralSpace[0][0].GetLength(0) / BinSize, 2];
                        rANOVA[i] = new double[(int)Math.Ceiling((double)TrialDuration / BinSize)][];
                        seTemp1[i] = new double[(int)Math.Ceiling((double)TrialDuration / BinSize)][];
                    }
                    //This is loop is used to count the actual number of members inside a Super category (as we have changed the category member counts after we recorded some neurons)
                    int[] SuperCategoryCount2 = new int[Config.SuperCategoryCount2];
                    for (int i = 0; i < PSTH.GetLength(0); i++)
                        if (NeuralSpace[i] != null)
                            for (int j = 0; j < NeuralSpace[i].GetLength(0); j++)
                                if (Config.MapSuperCategory2(i) > -1)
                                    SuperCategoryCount2[Config.MapSuperCategory2(i)]++;

                    for (int i = 0; i < NeuralSpace.GetLength(0); i++)
                        if (PSTH[i] != null)
                            for (int j = 0; j < PSTH[i].Length; j++)
                                if (Config.MapSuperCategory2(i) > -1)
                                {
                                    PSTH[i][j] = PSTH[i][j] * (1000 / BinSize);// / NeuralSpace[i].GetLength(0);
                                    r[Config.MapSuperCategory2(i)][j, 0] += PSTH[i][j] / (double)SuperCategoryCount2[Config.MapSuperCategory2(i)];
                                    if (rANOVA[Config.MapSuperCategory2(i)][j] == null) rANOVA[Config.MapSuperCategory2(i)][j] = new double[0];
                                    Array.Resize(ref rANOVA[Config.MapSuperCategory2(i)][j], rANOVA[Config.MapSuperCategory2(i)][j].GetLength(0) + 1);
                                    if (seTemp1[Config.MapSuperCategory2(i)][j] == null) seTemp1[Config.MapSuperCategory2(i)][j] = new double[0];
                                    Array.Resize(ref seTemp1[Config.MapSuperCategory2(i)][j], seTemp1[Config.MapSuperCategory2(i)][j].GetLength(0) + 1);
                                    seTemp1[Config.MapSuperCategory2(i)][j][seTemp1[Config.MapSuperCategory2(i)][j].GetLength(0) - 1] = r[Config.MapSuperCategory2(i)][j, 0];
                                }

                    // Standard error of mean 
                    for (int i = 0; i < seTemp1.GetLength(0); i++)
                        for (int j = 0; j < seTemp1[0].GetLength(0); j++)
                            r[i][j, 1] = StatisticalTests.StandardError(seTemp1[i][j]);// Math.Sqrt((seTemp1[i][j] - (Math.Pow(seTemp2[i][j], 2) / (double)Config.MapSuperCategoryCount2(i, ProjectName))) / (Config.MapSuperCategoryCount2(i, ProjectName) - 1.0f)) / (double)Math.Sqrt(Config.MapSuperCategoryCount2(i, ProjectName));
                    #endregion
                    break;
                case 5:
                    #region faces vs. objects without hands
                    cc = 8;
                    r = new double[cc + 1][,];
                    rANOVA = new double[cc][][];
                    seTemp1 = new double[cc][][];
                    for (int i = 0; i < cc; i++)
                    {
                        r[i] = new double[(int)Math.Ceiling((double)TrialDuration / BinSize), 3];
                        rANOVA[i] = new double[(int)Math.Ceiling((double)TrialDuration / BinSize)][];
                        seTemp1[i] = new double[(int)Math.Ceiling((double)TrialDuration / BinSize)][];
                    }
                    //This is loop is used to count the actual number of members inside a category (as we have changed the category member counts after we recorded some neurons)
                    SuperCategoryCount = new int[Config.SuperCategoryCount3];
                    for (int i = 0; i < PSTH.GetLength(0); i++)
                        if (PSTH[i] != null)
                            for (int j = 0; j < PSTH[i].GetLength(0); j++)
                                if (Config.MapSuperCategory3(i) > -1)
                                    SuperCategoryCount[Config.MapSuperCategory3(i)]++;

                    for (int i = 0; i < PSTH.GetLength(0); i++)
                        if (PSTH[i] != null)
                            for (int j = 0; j < PSTH[i].Length; j++)
                                if (Config.MapSuperCategory3(i) > -1)
                                {
                                    PSTH[i][j] = PSTH[i][j] * (1000.0d / (double)BinSize);/// (double)NeuralSpace[i].GetLength(0);
                                    r[Config.MapSuperCategory3(i)][j, 0] += PSTH[i][j] / (double)SuperCategoryCount[Config.MapSuperCategory3(i)];
                                    if (rANOVA[Config.MapSuperCategory3(i)][j] == null) rANOVA[Config.MapSuperCategory3(i)][j] = new double[0];
                                    Array.Resize(ref rANOVA[Config.MapSuperCategory3(i)][j], rANOVA[Config.MapSuperCategory3(i)][j].GetLength(0) + 1);
                                    if (seTemp1[Config.MapSuperCategory3(i)][j] == null) seTemp1[Config.MapSuperCategory3(i)][j] = new double[0];
                                    Array.Resize(ref seTemp1[Config.MapSuperCategory3(i)][j], seTemp1[Config.MapSuperCategory3(i)][j].GetLength(0) + 1);
                                    seTemp1[Config.MapSuperCategory3(i)][j][seTemp1[Config.MapSuperCategory3(i)][j].GetLength(0) - 1] = r[Config.MapSuperCategory3(i)][j, 0];
                                }

                    // Standard error of mean 
                    for (int i = 0; i < seTemp1.GetLength(0); i++)
                        for (int j = 0; j < seTemp1[0].GetLength(0); j++)
                            r[i][j, 1] = StatisticalTests.StandardError(seTemp1[i][j]);// Math.Sqrt((seTemp1[i][j] - (Math.Pow(seTemp2[i][j], 2) / (double)SuperCategoryCount[Config.MapSuperCategory3(i)])) / (SuperCategoryCount[Config.MapSuperCategory3(i)] - 1.0f)) / (double)Math.Sqrt(SuperCategoryCount[Config.MapSuperCategory3(i)]);
                    #endregion
                    break;
                case 6:
                    #region all categories
                    cc = 15;
                    r = new double[cc + 1][,];
                    rANOVA = new double[cc][][];
                    seTemp1 = new double[cc][][];
                    for (int i = 0; i < cc; i++)
                    {
                        r[i] = new double[(int)Math.Ceiling((double)TrialDuration / BinSize), 3];//;new double[NeuralSpace[0][0].GetLength(0) / BinSize, 2];
                        rANOVA[i] = new double[(int)Math.Ceiling((double)TrialDuration / BinSize)][];
                        seTemp1[i] = new double[(int)Math.Ceiling((double)TrialDuration / BinSize)][];
                    }
                    //This is loop is used to count the actual number of members inside a category (as we have changed the category member counts after we recorded some neurons)
                    int[] FacialCategoryCount = new int[Config.FacialCategoryCount];
                    for (int i = 0; i < PSTH.GetLength(0); i++)
                        if (NeuralSpace[i] != null)
                            for (int j = 0; j < NeuralSpace[i].GetLength(0); j++)
                                if (Config.MapFacialCategory(i) > -1)
                                    FacialCategoryCount[Config.MapCategory(i)]++;
                    for (int i = 0; i < PSTH.GetLength(0); i++)
                        if (PSTH[i] != null)
                            for (int j = 0; j < PSTH[i].Length; j++)
                                if (Config.MapFacialCategory(i) > -1)
                                {
                                    PSTH[i][j] = PSTH[i][j] * (1000.0d / (double)BinSize);/// (double)NeuralSpace[i].GetLength(0);
                                    r[Config.MapFacialCategory(i)][j, 0] += PSTH[i][j] / (double)FacialCategoryCount[Config.MapFacialCategory(i)];
                                    if (rANOVA[Config.MapFacialCategory(i)][j] == null) rANOVA[Config.MapFacialCategory(i)][j] = new double[0];
                                    Array.Resize(ref rANOVA[Config.MapFacialCategory(i)][j], rANOVA[Config.MapFacialCategory(i)][j].GetLength(0) + 1);
                                    rANOVA[Config.MapFacialCategory(i)][j][rANOVA[Config.MapFacialCategory(i)][j].GetLength(0) - 1] = PSTH[i][j] / (double)FacialCategoryCount[Config.MapFacialCategory(i)];
                                    if (seTemp1[Config.MapFacialCategory(i)][j] == null) seTemp1[Config.MapFacialCategory(i)][j] = new double[0];
                                    Array.Resize(ref seTemp1[Config.MapFacialCategory(i)][j], seTemp1[Config.MapFacialCategory(i)][j].GetLength(0) + 1);
                                    seTemp1[Config.MapFacialCategory(i)][j][seTemp1[Config.MapFacialCategory(i)][j].GetLength(0) - 1] = r[Config.MapFacialCategory(i)][j, 0];
                                }

                    // Standard error of mean 
                    for (int i = 0; i < seTemp1.GetLength(0); i++)
                        for (int j = 0; j < seTemp1[0].GetLength(0); j++)
                            r[i][j, 1] = StatisticalTests.StandardError(seTemp1[i][j]);//Math.Sqrt((seTemp1[i][j] - (Math.Pow(seTemp2[i][j], 2) / (double)CategoryCount[Config.MapCategory(i)])) / (CategoryCount[Config.MapCategory(i)] - 1.0f)) /(double)Math.Sqrt(CategoryCount[Config.MapCategory(i)]);
                    #endregion
                    break;
                case 7:
                    #region all individual stimuli
                    cc = 525;
                    r = new double[cc + 1][,];
                    rANOVA = new double[cc][][];
                    seTemp1 = new double[cc][][];
                    for (int i = 0; i < cc; i++)
                    {
                        r[i] = new double[(int)Math.Ceiling((double)TrialDuration / BinSize), 3];//;new double[NeuralSpace[0][0].GetLength(0) / BinSize, 2];
                        rANOVA[i] = new double[(int)Math.Ceiling((double)TrialDuration / BinSize)][];
                        seTemp1[i] = new double[(int)Math.Ceiling((double)TrialDuration / BinSize)][];
                    }
                    //This is loop is used to count the actual number of members inside a category (as we have changed the category member counts after we recorded some neurons)
                    CategoryCount = new int[Config.StimuliCount];
                    for (int i = 0; i < PSTH.GetLength(0); i++)
                        if (NeuralSpace[i] != null)
                            for (int j = 0; j < NeuralSpace[i].GetLength(0); j++)
                                if ((i > -1) & (StatisticalTests.Mean(StatisticalTests.SubArray(PSTH[i], 7, 10)) > Threshold))
                                    CategoryCount[i]++;
                    for (int i = 0; i < PSTH.GetLength(0); i++)
                        if (PSTH[i] != null)
                            for (int j = 0; j < PSTH[i].Length; j++)
                                if ((i > -1) & (StatisticalTests.Mean(StatisticalTests.SubArray(PSTH[i], 7, 10)) > Threshold))
                                {
                                    PSTH[i][j] = PSTH[i][j] * (1000.0d / (double)BinSize);/// (double)NeuralSpace[i].GetLength(0);
                                    r[i][j, 0] += PSTH[i][j] / (double)CategoryCount[i];
                                    if (rANOVA[i][j] == null) rANOVA[i][j] = new double[0];
                                    Array.Resize(ref rANOVA[i][j], rANOVA[i][j].GetLength(0) + 1);
                                    rANOVA[i][j][rANOVA[i][j].GetLength(0) - 1] = PSTH[i][j] / (double)CategoryCount[i];
                                    if (seTemp1[i][j] == null) seTemp1[i][j] = new double[0];
                                    Array.Resize(ref seTemp1[i][j], seTemp1[i][j].GetLength(0) + 1);
                                    seTemp1[i][j][seTemp1[i][j].GetLength(0) - 1] = r[i][j, 0];
                                }

                    // Standard error of mean 
                    for (int i = 0; i < seTemp1.GetLength(0); i++)
                        for (int j = 0; j < seTemp1[0].GetLength(0); j++)
                            r[i][j, 1] = StatisticalTests.StandardError(seTemp1[i][j]);//Math.Sqrt((seTemp1[i][j] - (Math.Pow(seTemp2[i][j], 2) / (double)CategoryCount[Config.MapCategory(i)])) / (CategoryCount[Config.MapCategory(i)] - 1.0f)) /(double)Math.Sqrt(CategoryCount[Config.MapCategory(i)]);
                    #endregion
                    break;
            }

            r[r.GetLength(0) - 1] = AveragePSTHtemp;
            for (int i = 0; i < rANOVA.GetLength(0); i++)
            {
                r[i][0,2] = StatisticalTests.ANOVAoneway(rANOVA[i]);
            }
            if (CalculateCumulative)
            {
                for (int i = 0; i < r.GetLength(0); i++)
                    for (int j = 1; j < r[i].GetLength(0); j++)
                    {
                        r[i][j, 0] = r[i][j, 0] + r[i][j - 1, 0];
                        r[i][j, 1] = 0;// r[i][j, 1] + r[i][j - 1, 1];
                    }
            }

            return r;
        }

/// <summary>
/// It uses a sliding window method with the window as large as BinSize, and slides it each time as much as jump.
/// </summary>
/// <param name="NeuralSpace"></param>
/// <param name="level"></param>
/// <param name="BinSize"></param>
/// <param name="TrialDuration"></param>
/// <param name="jump"></param>
/// <returns></returns>
        public static double[][,] CalculateCatOverTime(bool[][][] NeuralSpace, int level, int BinSize, int TrialDuration, int jump)
        {
            int PSTHBins = (int)Math.Ceiling((double)TrialDuration / jump);
            double[,] AveragePSTHtemp = new double[PSTHBins, 3];
            int StimulusCount = 0;
            double[][] PSTH = new double[NeuralSpace.GetLength(0)][];
            for (int i = 0; i < NeuralSpace.GetLength(0); i++)  // Stimulus Counter
            {
                if (NeuralSpace[i] != null)
                {
                    StimulusCount++;
                    PSTH[i] = new double[(int)Math.Ceiling((double)TrialDuration / (double)jump)];
                    for (int j = 0; j < NeuralSpace[i].GetLength(0); j++)// Repetitions
                        for (int k = 0; k < TrialDuration; k++) // Time
                            for(int m = 0; m< BinSize; m++)
                                if (NeuralSpace[i][j][k])
                                    if (((k / jump) + m)<PSTH[i].GetLength(0))
                                        PSTH[i][(k / jump)+m]+=1.0d/BinSize;
                }
            }

            
            double Threshold = double.MinValue;

            //double BlankCategoryAverage = 0;
            double[][,] r = new double[1][,];
            double[][][] rANOVA = new double[1][][];
            double[][][] seTemp1 = new double[1][][];
            int cc = 0;


            switch (level)
            {
                case 1:
                    #region face nonface
                    cc = 3;
                    r = new double[cc][,];
                    rANOVA = new double[cc-1][][];
                    seTemp1 = new double[cc][][];
                    for (int i = 0; i < cc; i++)
                    {
                        r[i] = new double[(int)Math.Ceiling((double)TrialDuration / jump), 3];
                        seTemp1[i] = new double[(int)Math.Ceiling((double)TrialDuration / jump)][];
                        if (i == cc - 1) break;
                        rANOVA[i] = new double[(int)Math.Ceiling((double)TrialDuration / jump)][];
                    }

                    //This is loop is used to count the actual number of members inside a category (as we have changed the category member counts after we recorded some neurons)
                    int[] FaceNonFaceCount = new int[Config.FaceNonFaceCount];
                    for (int i = 0; i < PSTH.GetLength(0); i++)
                        if (PSTH[i] != null)
                            for (int j = 0; j < NeuralSpace[i].GetLength(0); j++)
                                if ((Config.MapFaceNonFace(i) > -1) & (StatisticalTests.Mean(StatisticalTests.SubArray(PSTH[i], 7, 10)) > Threshold))
                                    FaceNonFaceCount[Config.MapFaceNonFace(i)]++;

                    for (int i = 0; i < NeuralSpace.GetLength(0); i++)
                        if (PSTH[i] != null)
                            for (int j = 0; j < PSTH[i].Length; j++)
                                if (((Config.MapFaceNonFace(i) < 2) & (Config.MapFaceNonFace(i) > -1)) & (StatisticalTests.Mean(StatisticalTests.SubArray(PSTH[i], 7, 10)) > Threshold))
                                {
                                    PSTH[i][j] = PSTH[i][j] * (1000.0d / (double)BinSize);// / (double)NeuralSpace[i].GetLength(0) ;
                                    r[Config.MapFaceNonFace(i)][j, 0] += PSTH[i][j] / (double)FaceNonFaceCount[Config.MapFaceNonFace(i)];
                                    if (rANOVA[Config.MapFaceNonFace(i)][j] == null) rANOVA[Config.MapFaceNonFace(i)][j] = new double[0];
                                    Array.Resize(ref rANOVA[Config.MapFaceNonFace(i)][j], rANOVA[Config.MapFaceNonFace(i)][j].GetLength(0) + 1);
                                    if (seTemp1[Config.MapFaceNonFace(i)][j] == null) seTemp1[Config.MapFaceNonFace(i)][j] = new double[0];
                                    Array.Resize(ref seTemp1[Config.MapFaceNonFace(i)][j], seTemp1[Config.MapFaceNonFace(i)][j].GetLength(0) + 1);
                                    seTemp1[Config.MapFaceNonFace(i)][j][seTemp1[Config.MapFaceNonFace(i)][j].GetLength(0) - 1] = r[Config.MapFaceNonFace(i)][j, 0];
                                }

                    // Standard error of mean 
                    for (int i = 0; i < seTemp1.GetLength(0); i++)
                        for (int j = 0; j < seTemp1[0].GetLength(0); j++)
                            r[i][j, 1] = StatisticalTests.StandardError(seTemp1[i][j]); //Math.Sqrt((seTemp1[i][j] - (Math.Pow(seTemp2[i][j], 2) / (double)FaceNonFaceCount[Config.MapFaceNonFace(i)])) / (FaceNonFaceCount[Config.MapFaceNonFace(i)] - 1.0f))/(double)Math.Sqrt(FaceNonFaceCount[Config.MapFaceNonFace(i)]);
                    #endregion
                    break;

                case 2:
                    #region Supercategories
                    cc = 11;
                    r = new double[cc][,];
                    rANOVA = new double[cc][][];
                    seTemp1 = new double[cc][][];
                    for (int i = 0; i < cc; i++)
                    {
                        r[i] = new double[(int)Math.Ceiling((double)TrialDuration / jump), 3];//;new double[NeuralSpace[0][0].GetLength(0) / BinSize, 2];
                        rANOVA[i] = new double[(int)Math.Ceiling((double)TrialDuration / jump)][];
                        seTemp1[i] = new double[(int)Math.Ceiling((double)TrialDuration / jump)][];
                    }
                    //This is loop is used to count the actual number of members inside a Super category (as we have changed the category member counts after we recorded some neurons)
                    int[] SuperCategoryCount = new int[Config.SuperCategoryCount];
                    for (int i = 0; i < PSTH.GetLength(0); i++)
                        if (NeuralSpace[i] != null)
                            if (StatisticalTests.Mean(StatisticalTests.SubArray(PSTH[i], 7, 10)) > Threshold)
                                for (int j = 0; j < NeuralSpace[i].GetLength(0); j++)
                                    if (Config.MapSuperCategory(i) > -1)
                                        SuperCategoryCount[Config.MapSuperCategory(i)]++;
                    for (int i = 0; i < NeuralSpace.GetLength(0); i++)
                        if (PSTH[i] != null)
                            if (StatisticalTests.Mean(StatisticalTests.SubArray(PSTH[i], 7, 10)) > Threshold)
                                for (int j = 0; j < PSTH[i].Length; j++)
                                    if (Config.MapSuperCategory(i) > -1)
                                    {
                                        PSTH[i][j] = PSTH[i][j] * (1000.0d / (double)BinSize);/// (double)NeuralSpace[i].GetLength(0);
                                        r[Config.MapSuperCategory(i)][j, 0] += PSTH[i][j] / (double)SuperCategoryCount[Config.MapSuperCategory(i)];
                                        if (rANOVA[Config.MapSuperCategory(i)][j] == null) rANOVA[Config.MapSuperCategory(i)][j] = new double[0];
                                        Array.Resize(ref rANOVA[Config.MapSuperCategory(i)][j], rANOVA[Config.MapSuperCategory(i)][j].GetLength(0) + 1);
                                        if (seTemp1[Config.MapSuperCategory(i)][j] == null) seTemp1[Config.MapSuperCategory(i)][j] = new double[0];
                                        Array.Resize(ref seTemp1[Config.MapSuperCategory(i)][j], seTemp1[Config.MapSuperCategory(i)][j].GetLength(0) + 1);
                                        seTemp1[Config.MapSuperCategory(i)][j][seTemp1[Config.MapSuperCategory(i)][j].GetLength(0) - 1] = r[Config.MapSuperCategory(i)][j, 0];
                                    }
                    // Standard error of mean 
                    for (int i = 0; i < seTemp1.GetLength(0); i++)
                        for (int j = 0; j < seTemp1[0].GetLength(0); j++)
                            r[i][j, 1] = StatisticalTests.StandardError(seTemp1[i][j]);//Math.Sqrt((seTemp1[i][j] - (Math.Pow(seTemp2[i][j], 2) / (double)SuperCategoryCount[Config.MapSuperCategory(i)])) / ((double)SuperCategoryCount[Config.MapSuperCategory(i)] - 1.0f)) / (double)Math.Sqrt((double)SuperCategoryCount[Config.MapSuperCategory(i)]);    
                    #endregion
                    break;

                case 3:
                    #region all categories
                    cc = 21;
                    r = new double[cc+1][,];
                    rANOVA = new double[cc][][];
                    seTemp1 = new double[cc][][];
                    for (int i = 0; i < cc; i++)
                    {
                        r[i] = new double[(int)Math.Ceiling((double)TrialDuration / jump), 3];//;new double[NeuralSpace[0][0].GetLength(0) / BinSize, 2];
                        rANOVA[i] = new double[(int)Math.Ceiling((double)TrialDuration / jump)][];
                        seTemp1[i] = new double[(int)Math.Ceiling((double)TrialDuration / jump)][];
                    }
                    //This is loop is used to count the actual number of members inside a category (as we have changed the category member counts after we recorded some neurons)
                    int[] CategoryCount = new int[Config.CategoryCount];
                    for (int i = 0; i < PSTH.GetLength(0); i++)
                        if (NeuralSpace[i] != null)
                            for (int j = 0; j < NeuralSpace[i].GetLength(0); j++)
                                if ((Config.MapCategory(i) > -1) & (StatisticalTests.Mean(StatisticalTests.SubArray(PSTH[i], 7, 10)) > Threshold))
                                    CategoryCount[Config.MapCategory(i)]++;
                    for (int i = 0; i < PSTH.GetLength(0); i++)
                        if (PSTH[i] != null)
                            for (int j = 0; j < PSTH[i].Length; j++)
                                if ((Config.MapCategory(i) > -1) & (StatisticalTests.Mean(StatisticalTests.SubArray(PSTH[i], 7, 10)) > Threshold))
                                {
                                    PSTH[i][j] = PSTH[i][j] * (1000.0d / (double)BinSize);/// (double)NeuralSpace[i].GetLength(0);
                                    r[Config.MapCategory(i)][j, 0] += PSTH[i][j] / (double)CategoryCount[Config.MapCategory(i)];
                                    if (rANOVA[Config.MapCategory(i)][j] == null) rANOVA[Config.MapCategory(i)][j] = new double[0];
                                    Array.Resize(ref rANOVA[Config.MapCategory(i)][j], rANOVA[Config.MapCategory(i)][j].GetLength(0) + 1);
                                    rANOVA[Config.MapCategory(i)][j][rANOVA[Config.MapCategory(i)][j].GetLength(0) - 1] = PSTH[i][j] / (double)CategoryCount[Config.MapCategory(i)];
                                    if (seTemp1[Config.MapCategory(i)][j] == null) seTemp1[Config.MapCategory(i)][j] = new double[0];
                                    Array.Resize(ref seTemp1[Config.MapCategory(i)][j], seTemp1[Config.MapCategory(i)][j].GetLength(0) + 1);
                                    seTemp1[Config.MapCategory(i)][j][seTemp1[Config.MapCategory(i)][j].GetLength(0) - 1] = r[Config.MapCategory(i)][j, 0];
                                }

                    // Standard error of mean 
                    for (int i = 0; i < seTemp1.GetLength(0); i++)
                        for (int j = 0; j < seTemp1[0].GetLength(0); j++)
                            r[i][j, 1] = StatisticalTests.StandardError(seTemp1[i][j]);//Math.Sqrt((seTemp1[i][j] - (Math.Pow(seTemp2[i][j], 2) / (double)CategoryCount[Config.MapCategory(i)])) / (CategoryCount[Config.MapCategory(i)] - 1.0f)) /(double)Math.Sqrt(CategoryCount[Config.MapCategory(i)]);
                    #endregion
                    break;

                case 4:
                    #region face vs. simple shapes
                    cc = 8;
                    r = new double[cc][,];
                    rANOVA = new double[cc][][];
                    seTemp1 = new double[cc][][];
                    for (int i = 0; i < cc; i++)
                    {
                        r[i] = new double[(int)Math.Ceiling((double)TrialDuration / jump), 3];//;new double[NeuralSpace[0][0].GetLength(0) / BinSize, 2];
                        rANOVA[i] = new double[(int)Math.Ceiling((double)TrialDuration / jump)][];
                        seTemp1[i] = new double[(int)Math.Ceiling((double)TrialDuration / jump)][];
                    }
                    //This is loop is used to count the actual number of members inside a Super category (as we have changed the category member counts after we recorded some neurons)
                    int[] SuperCategoryCount2 = new int[Config.SuperCategoryCount2];
                    for (int i = 0; i < PSTH.GetLength(0); i++)
                        if (NeuralSpace[i] != null)
                            for (int j = 0; j < NeuralSpace[i].GetLength(0); j++)
                                if (Config.MapSuperCategory2(i) > -1)
                                    SuperCategoryCount2[Config.MapSuperCategory2(i)]++;

                    for (int i = 0; i < NeuralSpace.GetLength(0); i++)
                        if (PSTH[i] != null)
                            for (int j = 0; j < PSTH[i].Length; j++)
                                if (Config.MapSuperCategory2(i) > -1)
                                {
                                    PSTH[i][j] = PSTH[i][j] * (1000 / BinSize);// / NeuralSpace[i].GetLength(0);
                                    r[Config.MapSuperCategory2(i)][j, 0] += PSTH[i][j] / (double)SuperCategoryCount2[Config.MapSuperCategory2(i)];
                                    if (rANOVA[Config.MapSuperCategory2(i)][j] == null) rANOVA[Config.MapSuperCategory2(i)][j] = new double[0];
                                    Array.Resize(ref rANOVA[Config.MapSuperCategory2(i)][j], rANOVA[Config.MapSuperCategory2(i)][j].GetLength(0) + 1);
                                    if (seTemp1[Config.MapSuperCategory2(i)][j] == null) seTemp1[Config.MapSuperCategory2(i)][j] = new double[0];
                                    Array.Resize(ref seTemp1[Config.MapSuperCategory2(i)][j], seTemp1[Config.MapSuperCategory2(i)][j].GetLength(0) + 1);
                                    seTemp1[Config.MapSuperCategory2(i)][j][seTemp1[Config.MapSuperCategory2(i)][j].GetLength(0) - 1] = r[Config.MapSuperCategory2(i)][j, 0];
                                }

                    // Standard error of mean 
                    for (int i = 0; i < seTemp1.GetLength(0); i++)
                        for (int j = 0; j < seTemp1[0].GetLength(0); j++)
                            r[i][j, 1] = StatisticalTests.StandardError(seTemp1[i][j]);// Math.Sqrt((seTemp1[i][j] - (Math.Pow(seTemp2[i][j], 2) / (double)Config.MapSuperCategoryCount2(i, ProjectName))) / (Config.MapSuperCategoryCount2(i, ProjectName) - 1.0f)) / (double)Math.Sqrt(Config.MapSuperCategoryCount2(i, ProjectName));
                    #endregion
                    break;
                case 5:
                    #region faces vs. objects without hands
                    cc = 8;
                    r = new double[cc][,];
                    rANOVA = new double[cc][][];
                    seTemp1 = new double[cc][][];
                    for (int i = 0; i < cc; i++)
                    {
                        r[i] = new double[(int)Math.Ceiling((double)TrialDuration / jump), 3];
                        rANOVA[i] = new double[(int)Math.Ceiling((double)TrialDuration / jump)][];
                        seTemp1[i] = new double[(int)Math.Ceiling((double)TrialDuration / jump)][];
                    }
                    //This is loop is used to count the actual number of members inside a category (as we have changed the category member counts after we recorded some neurons)
                    SuperCategoryCount = new int[Config.SuperCategoryCount3];
                    for (int i = 0; i < PSTH.GetLength(0); i++)
                        if (PSTH[i] != null)
                            for (int j = 0; j < PSTH[i].GetLength(0); j++)
                                if (Config.MapSuperCategory3(i) > -1)
                                    SuperCategoryCount[Config.MapSuperCategory3(i)]++;

                    for (int i = 0; i < PSTH.GetLength(0); i++)
                        if (PSTH[i] != null)
                            for (int j = 0; j < PSTH[i].Length; j++)
                                if (Config.MapSuperCategory3(i) > -1)
                                {
                                    PSTH[i][j] = PSTH[i][j] * (1000.0d / (double)BinSize);/// (double)NeuralSpace[i].GetLength(0);
                                    r[Config.MapSuperCategory3(i)][j, 0] += PSTH[i][j] / (double)SuperCategoryCount[Config.MapSuperCategory3(i)];
                                    if (rANOVA[Config.MapSuperCategory3(i)][j] == null) rANOVA[Config.MapSuperCategory3(i)][j] = new double[0];
                                    Array.Resize(ref rANOVA[Config.MapSuperCategory3(i)][j], rANOVA[Config.MapSuperCategory3(i)][j].GetLength(0) + 1);
                                    if (seTemp1[Config.MapSuperCategory3(i)][j] == null) seTemp1[Config.MapSuperCategory3(i)][j] = new double[0];
                                    Array.Resize(ref seTemp1[Config.MapSuperCategory3(i)][j], seTemp1[Config.MapSuperCategory3(i)][j].GetLength(0) + 1);
                                    seTemp1[Config.MapSuperCategory3(i)][j][seTemp1[Config.MapSuperCategory3(i)][j].GetLength(0) - 1] = r[Config.MapSuperCategory3(i)][j, 0];
                                }

                    // Standard error of mean 
                    for (int i = 0; i < seTemp1.GetLength(0); i++)
                        for (int j = 0; j < seTemp1[0].GetLength(0); j++)
                            r[i][j, 1] = StatisticalTests.StandardError(seTemp1[i][j]);// Math.Sqrt((seTemp1[i][j] - (Math.Pow(seTemp2[i][j], 2) / (double)SuperCategoryCount[Config.MapSuperCategory3(i)])) / (SuperCategoryCount[Config.MapSuperCategory3(i)] - 1.0f)) / (double)Math.Sqrt(SuperCategoryCount[Config.MapSuperCategory3(i)]);
                    #endregion
                    break;
                case 6:
                    #region all categories
                    cc = 15;
                    r = new double[cc][,];
                    rANOVA = new double[cc][][];
                    seTemp1 = new double[cc][][];
                    for (int i = 0; i < cc; i++)
                    {
                        r[i] = new double[(int)Math.Ceiling((double)TrialDuration / jump), 3];//;new double[NeuralSpace[0][0].GetLength(0) / BinSize, 2];
                        rANOVA[i] = new double[(int)Math.Ceiling((double)TrialDuration / jump)][];
                        seTemp1[i] = new double[(int)Math.Ceiling((double)TrialDuration / jump)][];
                    }
                    //This is loop is used to count the actual number of members inside a category (as we have changed the category member counts after we recorded some neurons)
                    int[] FacialCategoryCount = new int[Config.FacialCategoryCount];
                    for (int i = 0; i < PSTH.GetLength(0); i++)
                        if (NeuralSpace[i] != null)
                            for (int j = 0; j < NeuralSpace[i].GetLength(0); j++)
                                if (Config.MapFacialCategory(i) > -1)
                                    FacialCategoryCount[Config.MapCategory(i)]++;
                    for (int i = 0; i < PSTH.GetLength(0); i++)
                        if (PSTH[i] != null)
                            for (int j = 0; j < PSTH[i].Length; j++)
                                if (Config.MapFacialCategory(i) > -1)
                                {
                                    PSTH[i][j] = PSTH[i][j] * (1000.0d / (double)BinSize);/// (double)NeuralSpace[i].GetLength(0);
                                    r[Config.MapFacialCategory(i)][j, 0] += PSTH[i][j] / (double)FacialCategoryCount[Config.MapFacialCategory(i)];
                                    if (rANOVA[Config.MapFacialCategory(i)][j] == null) rANOVA[Config.MapFacialCategory(i)][j] = new double[0];
                                    Array.Resize(ref rANOVA[Config.MapFacialCategory(i)][j], rANOVA[Config.MapFacialCategory(i)][j].GetLength(0) + 1);
                                    rANOVA[Config.MapFacialCategory(i)][j][rANOVA[Config.MapFacialCategory(i)][j].GetLength(0) - 1] = PSTH[i][j] / (double)FacialCategoryCount[Config.MapFacialCategory(i)];
                                    if (seTemp1[Config.MapFacialCategory(i)][j] == null) seTemp1[Config.MapFacialCategory(i)][j] = new double[0];
                                    Array.Resize(ref seTemp1[Config.MapFacialCategory(i)][j], seTemp1[Config.MapFacialCategory(i)][j].GetLength(0) + 1);
                                    seTemp1[Config.MapFacialCategory(i)][j][seTemp1[Config.MapFacialCategory(i)][j].GetLength(0) - 1] = r[Config.MapFacialCategory(i)][j, 0];
                                }

                    // Standard error of mean 
                    for (int i = 0; i < seTemp1.GetLength(0); i++)
                        for (int j = 0; j < seTemp1[0].GetLength(0); j++)
                            r[i][j, 1] = StatisticalTests.StandardError(seTemp1[i][j]);//Math.Sqrt((seTemp1[i][j] - (Math.Pow(seTemp2[i][j], 2) / (double)CategoryCount[Config.MapCategory(i)])) / (CategoryCount[Config.MapCategory(i)] - 1.0f)) /(double)Math.Sqrt(CategoryCount[Config.MapCategory(i)]);
                    #endregion
                    break;

            }

            r[r.GetLength(0) - 1] = AveragePSTHtemp;
            for (int i = 0; i < rANOVA[0].GetLength(0); i++)
            {
                r[i][0,2] = StatisticalTests.ANOVAoneway(rANOVA[i]);
            }
            //if (CalculateCumulative)
            //{
            //    for (int i = 0; i < r.GetLength(0); i++)
            //        for (int j = 1; j < r[i].GetLength(0); j++)
            //        {
            //            r[i][j, 0] = r[i][j, 0] + r[i][j - 1, 0];
            //            r[i][j, 1] = 0;// r[i][j, 1] + r[i][j - 1, 1];
            //        }
            //}

            return r;

        }
    }
}
