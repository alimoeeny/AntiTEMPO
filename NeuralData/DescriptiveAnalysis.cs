using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace NeuralData
{
    public class Descriptives
    {
        public static string CategoricalDifferentiation(bool[][][] NeuralSpace, int ResponseOnset, int ResponseOffset)
        {
            string r = "";

#region Is this neuron selective for any stimulus at all
            double[][] AFRStimulus = new double[NeuralSpace.GetLength(0)][];
            double[][] AFRSuperCat = new double[Config.SuperCategoryCount - 1][];//excluding blank
            double[][] AFRCat = new double[Config.CategoryCount - 1][];//excluding blank
            double[][] AFRSuperCat2 = new double[Config.SuperCategoryCount2-1][];//excluding blank
            double[][] AFRSuperCat3 = new double[Config.SuperCategoryCount3-2][];//excluding blank and hand

            for (int i = 0; i < NeuralSpace.GetLength(0); i++)  // Stimulus Counter
            {
                if ((NeuralSpace[i] != null) & (Config.MapSuperCategory(i)>-1) &(Config.MapSuperCategory(i)!=10) ) //to exclude the blank
                {
                    if (!((i>319)&(i<325)) & (AFRStimulus[i]==null)) AFRStimulus[i] = new double[0];
                    if ((Config.MapSuperCategory(i)!=10) & (AFRSuperCat[Config.MapSuperCategory(i)] == null)) AFRSuperCat[Config.MapSuperCategory(i)] = new double[0];
                    if ((Config.MapCategory(i)!=20) & (AFRCat[Config.MapCategory(i)] == null)) AFRCat[Config.MapCategory(i)] = new double[0];
                    if ((Config.MapSuperCategory2(i)!=7) & (AFRSuperCat2[Config.MapSuperCategory2(i)] == null)) AFRSuperCat2[Config.MapSuperCategory2(i)] = new double[0];
                    if (Config.MapSuperCategory3(i)<6) if(AFRSuperCat3[Config.MapSuperCategory3(i)] == null) AFRSuperCat3[Config.MapSuperCategory3(i)] = new double[0];
                    
                    int SupCatOffset = AFRSuperCat[Config.MapSuperCategory(i)].GetLength(0);
                    int CatOffset = AFRCat[Config.MapCategory(i)].GetLength(0);
                    int SupCat2Offset = AFRSuperCat2[Config.MapSuperCategory2(i)].GetLength(0);
                    int SupCat3Offset = AFRSuperCat3[Config.MapSuperCategory3(i)].GetLength(0);
                    
                    for (int j = 0; j < NeuralSpace[i].GetLength(0); j++)// Repetitions
                    {
                        Array.Resize(ref AFRStimulus[i], AFRStimulus[i].GetLength(0) + 1);
                        Array.Resize(ref AFRSuperCat[Config.MapSuperCategory(i)], AFRSuperCat[Config.MapSuperCategory(i)].GetLength(0) + 1);
                        Array.Resize(ref AFRCat[Config.MapCategory(i)], AFRCat[Config.MapCategory(i)].GetLength(0) + 1);
                        Array.Resize(ref AFRSuperCat2[Config.MapSuperCategory2(i)], AFRSuperCat2[Config.MapSuperCategory2(i)].GetLength(0) + 1);
                        Array.Resize(ref AFRSuperCat3[Config.MapSuperCategory3(i)], AFRSuperCat3[Config.MapSuperCategory3(i)].GetLength(0) + 1);
                        for (int k = ResponseOnset; k < ResponseOffset; k++) // Time
                            if (NeuralSpace[i][j][k])
                            {
                                AFRStimulus[i][j]++;
                                AFRSuperCat[Config.MapSuperCategory(i)][j + SupCatOffset]++;
                                AFRCat[Config.MapCategory(i)][j + CatOffset]++;
                                AFRSuperCat2[Config.MapSuperCategory2(i)][j + SupCat2Offset]++;
                                AFRSuperCat3[Config.MapSuperCategory3(i)][j + SupCat3Offset]++;
                            }
                        AFRStimulus[i][j] = AFRStimulus[i][j] * 1000.0d / (double)(ResponseOffset - ResponseOnset);
                        AFRSuperCat[Config.MapSuperCategory(i)][j + SupCatOffset] = AFRSuperCat[Config.MapSuperCategory(i)][j + SupCatOffset] * 1000.0d / (double)(ResponseOffset - ResponseOnset);
                        AFRCat[Config.MapCategory(i)][j + CatOffset] = AFRCat[Config.MapCategory(i)][j + CatOffset] * 1000.0d / (double)(ResponseOffset - ResponseOnset);
                        AFRSuperCat2[Config.MapSuperCategory2(i)][j + SupCat2Offset] = AFRSuperCat2[Config.MapSuperCategory2(i)][j + SupCat2Offset] * 1000.0d / (double)(ResponseOffset - ResponseOnset);
                        AFRSuperCat3[Config.MapSuperCategory3(i)][j + SupCat3Offset] = AFRSuperCat3[Config.MapSuperCategory3(i)][j + SupCat3Offset] * 1000.0d / (double)(ResponseOffset - ResponseOnset);
                    }
                }
            }
            double pValue = StatisticalTests.ANOVAoneway(StatisticalTests.Squeeze(AFRStimulus));
            r+= "The pValue for accepting that there is a difference in reponse of this neuron to different stimuli is : " + pValue.ToString("G3") + "\r\n";
            r += "So this neuron is Stimulus " + ((pValue < 0.05d) ? "selective" : "Non-selective") + "\r\n";
            pValue = StatisticalTests.ANOVAoneway(AFRSuperCat);
            r += "The pValue for accepting that there is a difference in reponse of this neuron to different Super Categories is : " + pValue.ToString("G3") + "\r\n";
            r += "So this neuron is Super Category " + ((pValue < 0.05d) ? "selective" : "Non-selective") + "\r\n";
            pValue = StatisticalTests.ANOVAoneway(StatisticalTests.Squeeze(AFRCat));
            r += "The pValue for accepting that there is a difference in reponse of this neuron to different Categories is : " + pValue.ToString("G3") + "\r\n";
            r += "So this neuron is Category " + ((pValue < 0.05d) ? "selective" : "Non-selective") + "\r\n";
            pValue = StatisticalTests.ANOVAoneway(AFRSuperCat2);
            r += "The pValue for accepting that there is a difference in reponse of this neuron to different Face Categories and simle objects is : " + pValue.ToString("G3") + "\r\n";
            r += "So this neuron is face-simple " + ((pValue < 0.05d) ? "selective" : "Non-selective") + "\r\n";
            pValue = StatisticalTests.ANOVAoneway(AFRSuperCat3);
            r += "The pValue for accepting that there is a difference in reponse of this neuron to different Face Categories and other objects excluding hand is : " + pValue.ToString("G3") + "\r\n";
            r += "So this neuron is Face-object " + ((pValue < 0.05d) ? "selective" : "Non-selective") + "\r\n";
            
#endregion

            #region Is this neuron selective for any supercategory 
        
            #endregion
            return r;
        
        }
    }
}