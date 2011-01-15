using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using StimulusPSTH;
using System.Collections;
using NeuralData;
using System.Globalization;

namespace StimulusPSTHPanel
{
    public partial class StimulusPSTHPanel : UserControl
    {
        public Color[] TrialTypeFlags = new  Color[] { Color.FromArgb(90,90,90), SystemColors.Control, SystemColors.Control, SystemColors.Control, SystemColors.Control, SystemColors.Control, SystemColors.Control, SystemColors.Control, SystemColors.Control, SystemColors.Control, SystemColors.Control};
        public StimulusPSTHPanel()
        {
            InitializeComponent();
        }

        public class mySorterClassDesc : IComparer
        {
            int IComparer.Compare(Object x, Object y)
            {
                return ((new System.Collections.Comparer(System.Globalization.CultureInfo.CurrentCulture) ).Compare(y, x));
            }
        }

        public class mySorterClassAsc : IComparer
        {
            int IComparer.Compare(Object x, Object y)
            {
                return ((new System.Collections.Comparer(System.Globalization.CultureInfo.CurrentCulture) ).Compare(x, y));
            }
        }

        private int PSTHPerPage = 16;
        public bool[][][] NeuralSpace;
        public int[] TrialTypes;
        public int SortBy=1;
        public int PageNo=1;
        public int BinSize;
        public int StimulusOnset = 0;
        public int StimulusOffset = 0;
        public int ResponseOnset = 0;
        public int ResponseOffset = 0;
        public readonly int BaseLineActivityOffset = 100;
        public int TrialDuration;
        public string StimuliPath;
        public int CheckSignificanceofResponse = 1; // 0 : No, 1: use the overall baseline activity, 2 use the prestimulus average compared to response period
        public double BaseLineFiringRate = 0;

        
        private int CurrentPage = 0;
        private double[][] PSTH = null;
        private double[] Significacy = null;
        private int[] PSTHSortedIndices = null; // Just to keep the Ranks 
        private double[] PSTHAverageFiringRates = null;
        private double[] PSTHReponseSEs = null;
        private double[] PSTHReponseStability = null;// Averagefiringrate / Response Standard Deviation
        private double MaximumFiringRate = 1;
        private double BestStimulusFiringRate = 1;
        private int[] SelectedStimuliIndices;
        private int ActualStimulusCount = 0;
        private int StimulusCount;

        public void CalculatePSTH() //Neural Space: Total 321 Stimuli, max 15 times presentation , trial duration sampels
        {
            if(TrialTypes==null) TrialTypes=new int[NeuralSpace.GetLength(0)];
            bool[][][] FilteredNeuralSpace = Filter(NeuralSpace);
            BaseLineFiringRate=0;
            int presentationcounts = 0;
            double[][] significancytempResponse = new double[FilteredNeuralSpace.GetLength(0)][];
            double[][] significancytempPrestimulus = new double[FilteredNeuralSpace.GetLength(0)][];
            PSTH = new double[FilteredNeuralSpace.GetLength(0)][];
            PSTHAverageFiringRates = new double[FilteredNeuralSpace.GetLength(0)];
            PSTHReponseSEs = new double[FilteredNeuralSpace.GetLength(0)];
            PSTHReponseStability = new double[FilteredNeuralSpace.GetLength(0)];
            Significacy = new double[FilteredNeuralSpace.GetLength(0)];
            PSTHSortedIndices =new int[FilteredNeuralSpace.GetLength(0)];

            for (int i = 0; i < FilteredNeuralSpace.GetLength(0); i++)  // Stimulus Counter
            {
                if (FilteredNeuralSpace[i] != null)
                {
                    ActualStimulusCount++;
                    PSTH[i] = new double[TrialDuration / BinSize];
                    significancytempResponse[i] = new double[FilteredNeuralSpace[i].GetLength(0)];
                    significancytempPrestimulus[i] = new double[FilteredNeuralSpace[i].GetLength(0)];
                    for (int j = 0; j < FilteredNeuralSpace[i].GetLength(0); j++)// Repetitions
                    {
                        presentationcounts++;
                        for (int k = 0; k < TrialDuration; k++) // Time
                        {
                            if (FilteredNeuralSpace[i][j][k]) PSTH[i][k / BinSize]++;
                            if ((k >= ResponseOnset) && (k < ResponseOffset) && FilteredNeuralSpace[i][j][k])
                            {
                                PSTHAverageFiringRates[i]++;
                                significancytempResponse[i][j]++;
                            }
                            if ((k < BaseLineActivityOffset) && FilteredNeuralSpace[i][j][k])
                            {
                                significancytempPrestimulus[i][j]++;
                                BaseLineFiringRate++;
                            }
                        }
                        significancytempResponse[i][j] = significancytempResponse[i][j] * (1000.0d / (double)(ResponseOffset - ResponseOnset));
                        significancytempPrestimulus[i][j] = significancytempPrestimulus[i][j] * (1000.0d / (double)BaseLineActivityOffset);
                    }
                }
                PSTHReponseSEs[i] = ((significancytempResponse[i]==null)? 0:  Math.Sqrt(StatisticalTests.Variance(significancytempResponse[i])) / Math.Sqrt(significancytempResponse[i].GetLength(0)));
                PSTHReponseStability[i] = (double)PSTHAverageFiringRates[i] / (double)PSTHReponseSEs[i];
            }
            
            BaseLineFiringRate = BaseLineFiringRate * (1000.0d / (double)(ResponseOffset - ResponseOnset)) / (double)(presentationcounts);
            BaselineToolStripLabel.Text="Baseline Firing rate : " + BaseLineFiringRate.ToString("G4") + " sp/sec";

            
            MaximumFiringRate = 1;
            for (int i = 0; i < FilteredNeuralSpace.GetLength(0); i++)
            {
                if (PSTH[i] != null)
                {
                    for (int j = 0; j < PSTH[i].Length; j++)
                    {
                        PSTH[i][j] = PSTH[i][j] * (1000.0f / (double)BinSize) / (double)FilteredNeuralSpace[i].GetLength(0);
                        MaximumFiringRate = Math.Max(MaximumFiringRate, PSTH[i][j]);
                    }
                    PSTHAverageFiringRates[i] = PSTHAverageFiringRates[i] * (1000 / (ResponseOffset - ResponseOnset)) / FilteredNeuralSpace[i].GetLength(0);
                    BestStimulusFiringRate = Math.Max(BestStimulusFiringRate, PSTHAverageFiringRates[i]);
                }
            }

            switch (CheckSignificanceofResponse)
            {
                case 0:
                    for (int i = 0; i < FilteredNeuralSpace.GetLength(0); i++)
                        Significacy[i] = 0;
                    break;
                case 1: // Comparing with mean of firing rate
                    for (int i = 0; i < FilteredNeuralSpace.GetLength(0); i++)
                        if(PSTH[i]!=null) Significacy[i] = StatisticalTests.tTest1Sample(significancytempResponse[i], BaseLineFiringRate, 0.05f, StatisticalTests.StatisticalTestTail.Both);
                    break;
                case 2: // Paired smaple ttest comparing trilas with theri PREstimulus epoch
                    for (int i = 0; i < FilteredNeuralSpace.GetLength(0); i++)
                        if (PSTH[i] != null) 
                            try { Significacy[i] = StatisticalTests.tTestPaired(significancytempResponse[i], significancytempPrestimulus[i], 0.05f, StatisticalTests.StatisticalTestTail.Both); }
                            catch (Exception e) { MessageBox.Show(e.Message + i.ToString()); }
                    break;
                case 3: // K-S test with prestimulus 
                    for (int i = 0; i < FilteredNeuralSpace.GetLength(0); i++)
                    {
                        if (PSTH[i] != null) Significacy[i] = StatisticalTests.KStest2(significancytempResponse[i], significancytempPrestimulus[i], 0.05, StatisticalTests.StatisticalTestTail.Both);
                    }
                    break;
            }


            for (int i = 0; i < FilteredNeuralSpace.GetLength(0); i++)
            {
                PSTHSortedIndices[i] = i;
            }

            switch (SortBy)
            {
                case 1: Array.Sort(PSTHAverageFiringRates,PSTHSortedIndices, new mySorterClassDesc());
                    break;
                case 2: Array.Sort(PSTHReponseSEs, PSTHSortedIndices, new mySorterClassAsc());
                    break;
                case 3: Array.Sort(PSTHReponseSEs, PSTHSortedIndices, new mySorterClassDesc());
                    break;
                case 4: Array.Sort(PSTHReponseStability, PSTHSortedIndices, new mySorterClassDesc());
                    break;
                case 5: Array.Sort(PSTHAverageFiringRates, PSTHSortedIndices, new mySorterClassAsc());
                    break;
            }

            StimulusCount = StatisticalTests.Squeeze(PSTH).GetLength(0);
            
            int TotalFaceCount = 0;
            int FacesgettingResponse = 0;
            int OddFaces = 0;
            int TotalNonFaceCount = 0;
            int NonfacesgettingResponse = 0;
            int OddNonfaces = 0;
            int TotalHumanFaces = 0;
            int HumanFacesgettingResponse = 0;
            int OddHumanefaces = 0;

            for (int i = 0; i < PSTH.GetLength(0); i++)
            {
                switch (Config.MapFaceNonFace(i))
                {
                    case 0: TotalFaceCount++; if(Significacy[i]<0.05) FacesgettingResponse++; break;
                    case 1: TotalNonFaceCount++; if (Significacy[i]<0.05) NonfacesgettingResponse++; break;
                }
                switch (Config.MapCategory(i))
                {
                    case 0: TotalHumanFaces++; if (Significacy[i] < 0.05) HumanFacesgettingResponse++; break;
                }
            }
            OddFaces = TotalFaceCount - FacesgettingResponse;
            OddNonfaces = TotalNonFaceCount - NonfacesgettingResponse;
            OddHumanefaces = TotalHumanFaces - HumanFacesgettingResponse;

            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
            nfi.NumberDecimalDigits = 2;
            nfi.NumberGroupSeparator = "";
            RatiosToolStripLabel.Text = "%hf:" + HumanFacesgettingResponse.ToString() + "/" + TotalHumanFaces.ToString() + "(" + (100.0d * (double)HumanFacesgettingResponse / (double)TotalHumanFaces).ToString("N",nfi) + ") - " +
                                        "%f: " + FacesgettingResponse.ToString() + "/" + TotalFaceCount.ToString() + "(" + (100.0d * (double)FacesgettingResponse / (double)TotalFaceCount).ToString("N", nfi) +
                                        ") - %nf: " + NonfacesgettingResponse.ToString() + "/" + TotalNonFaceCount.ToString() + " (" + (100.0d * (double)NonfacesgettingResponse / (double)TotalNonFaceCount).ToString("N", nfi) + ")" +
                                        " - Odds Ratio: " + (((double)FacesgettingResponse / (double)OddFaces) / ((double)NonfacesgettingResponse / (double)OddNonfaces)).ToString("N", nfi) + 
                                        " - Human Odds Ratio: " + (((double)HumanFacesgettingResponse / (double)OddHumanefaces) / ((double)NonfacesgettingResponse / (double)OddNonfaces)).ToString("N", nfi);
            

        }

        private bool[][][] Filter(bool[][][] NeuralSpace)
        {
            if (SelectedStimuliIndices == null)
            {
                SelectedStimuliIndices = new int[NeuralSpace.GetLength(0)];
                for (int i = 0; i < NeuralSpace.GetLength(0); i++)
                    SelectedStimuliIndices[i] = i;
                return NeuralSpace;
            }
            bool[][][] r = new bool[SelectedStimuliIndices.Length][][];
            for (int i = 0; i < SelectedStimuliIndices.Length; i++)
                r[i] = NeuralSpace[SelectedStimuliIndices[i]];
            return r;
        }


        public void ShowPSTHs(int PageNo) 
        {
            CurrentPage = PageNo;
            PageNoToolStripLabel.Text = " Page No : " + (CurrentPage + 1).ToString();
            if (CurrentPage == 0) { PreviousPageToolStripButton.Enabled = false; FirstPageToolStripButton.Enabled = false; } else { PreviousPageToolStripButton.Enabled = true; FirstPageToolStripButton.Enabled = true; }
            if (CurrentPage >= (StimulusCount / PSTHPerPage + 1)) { NextPageToolStripButton.Enabled = false; LastPageToolStripButton.Enabled = false; } else { NextPageToolStripButton.Enabled = true; LastPageToolStripButton.Enabled = true; }
            int t = 0;
            int t1 = 0;
            for (int i = 1; i < PSTHPerPage+1; i++)
            {
                t1 = CurrentPage * PSTHPerPage + i;
                if (t1 < PSTH.GetLength(0)+1)
                {
                    t = SortedIndex(t1); // here and in the image names indices starts from 1 but the psth is zero based and the sorted index returns a zero based index
                    if (PSTH[t] != null)
                        LoadPSTH((this.Controls.Find("stimulusPSTH" + i.ToString(), true)[0] as StimulusPSTH.StimulusPSTH),
                            SelectedStimuliIndices[t],
                            (10500000 + SelectedStimuliIndices[t] + 1).ToString().PadLeft(3, "0".ToCharArray()[0]),
                            PSTH[t],
                            PSTHAverageFiringRates[t1 - 1],
                            PSTHReponseSEs[t1 - 1],
                            PSTHReponseStability[t1 - 1],
                            NeuralSpace[t].GetLength(0),
                            t1,
                            BestStimulusFiringRate,
                            (Significacy[t] > 0.05f ? false : true),
                            Rasterdata(t),
                            TrialTypeFlags[TrialTypes[t1 - 1]]);
                }
                else
                {
                    (this.Controls.Find("stimulusPSTH" + i.ToString(), true)[0] as StimulusPSTH.StimulusPSTH).Reset();
                }

            }
        }

        //private void LoadPSTH(StimulusPSTH.StimulusPSTH stimulusPSTH, int StimulusId, string StimulusFileName, double[] psth, double AverageFiringRate, double ResponseSE, double ResponseStability, int TrialsCount, int Rank, double BestStimulusFiringRate, bool IsSignificant, bool[,] RasterData, Color TrialTypeFlag)
        //{
        //    LoadPSTH(stimulusPSTH, StimulusId, StimulusFileName, psth, AverageFiringRate, ResponseSE, ResponseStability, TrialsCount, Rank, BestStimulusFiringRate, IsSignificant, RasterData);
        //}

        private bool[,] Rasterdata(int i)
        {
            bool[,] r = new bool[NeuralSpace[i].GetLength(0), TrialDuration];
            for (int j = 0; j < NeuralSpace[i].GetLength(0); j++)// Repetitions
                for (int k = 0; k < TrialDuration; k++) // Time
                    r[j,k] = NeuralSpace[i][j][k];
            return r;

        }

        private int SortedIndex(int t)
        {
            return PSTHSortedIndices[t-1];
        }

        private void LoadPSTH(StimulusPSTH.StimulusPSTH stPSTH, int StimulusId, string StimulusFileName, double[] psth, double AverageFiringRate, double ResponseSE, double ResponseStability, int TrialsCount, int Rank, double BestStimulusFiringRate, bool IsSignificant, bool[,] Rasterdata, Color TrialTypeFlag)
        {
            stPSTH.Raster = Rasterdata;
            stPSTH.StimulusFileName = StimuliPath + StimulusFileName + ".jpg";
            stPSTH.Rank = Rank;
            stPSTH.IsSignificantResponse = IsSignificant;
            stPSTH.StimulusName = (10500000 + StimulusId + 1 ).ToString();
            stPSTH.PSTH = psth;
            stPSTH.MaximumFiringRate = MaximumFiringRate;
            stPSTH.AverageFiringRate = AverageFiringRate;
            stPSTH.ResponseSE = ResponseSE;
            stPSTH.ResponseStability = ResponseStability;
            stPSTH.TrialsCount = TrialsCount;
            stPSTH.BestStimFiringRate = BestStimulusFiringRate;
            stPSTH.StimulusOnset = StimulusOnset;
            stPSTH.StimulusDuration = StimulusOffset - StimulusOnset;
            stPSTH.ResponseOffset = ResponseOffset;
            stPSTH.ResponseOnset =  ResponseOnset;
            stPSTH.UpdateLabels();
            stPSTH.UpdateStimulus();
            stPSTH.PlotPSTH();
            stPSTH.TrialTypeFlag = TrialTypeFlag;
        }

        private void RefreshPSTHs()
        {
            CalculatePSTH();
            ShowPSTHs(0);
        }


        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            CurrentPage++;
            ShowPSTHs(CurrentPage);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            CurrentPage--;
            ShowPSTHs(CurrentPage);
        }


        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            ShowPSTHs(CurrentPage);
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            CurrentPage = 321 / 16 ;
            ShowPSTHs(CurrentPage);
        }

        private void compareToPerstimulusActivityForEachStimulusToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CheckSignificanceofResponse = 2;
            compaerToOverallBaselineToolStripMenuItem.Checked = false;
            compareToPerstimulusActivityForEachStimulusToolStripMenuItem1.Checked = true;
            kSTestWithEachTrialBaslineToolStripMenuItem.Checked = false; CalculatePSTH();
            ShowPSTHs(CurrentPage);
        }

        private void compaerToOverallBaselineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckSignificanceofResponse = 1;
            compaerToOverallBaselineToolStripMenuItem.Checked = true;
            compareToPerstimulusActivityForEachStimulusToolStripMenuItem1.Checked = false;
            kSTestWithEachTrialBaslineToolStripMenuItem.Checked = false;
            CalculatePSTH();
            ShowPSTHs(CurrentPage);
        }

        private void UnCheckAll()
        {
            justBlankToolStripMenuItem.Checked = false;

            primateToolStripMenuItem.Checked = false;
            reptileToolStripMenuItem.Checked = false;
            farmeAnimalsToolStripMenuItem.Checked = false;
            birdsToolStripMenuItem.Checked = false;
            rodentsToolStripMenuItem.Checked = false;

            carsToolStripMenuItem.Checked = false;
            CarsToolStripMenuItem1.Checked = false;
            handsToolStripMenuItem.Checked = false;
            handsToolStripMenuItem1.Checked = false;
            housesToolStripMenuItem.Checked = false;
            housesToolStripMenuItem1.Checked = false;
            objectsToolStripMenuItem.Checked = false;
            objectsToolStripMenuItem1.Checked = false;
            simpleShapesToolStripMenuItem.Checked = false;
            simpleShapesToolStripMenuItem1.Checked = false;

            humanFacesToolStripMenuItem.Checked = false;
            rhesusFacesToolStripMenuItem.Checked = false;
            orangutanFacesToolStripMenuItem.Checked = false;

            snakeFacesToolStripMenuItem.Checked = false;
            turttleFacesToolStripMenuItem.Checked = false;
            alligatorFaToolStripMenuItem.Checked = false;

            dogFacesToolStripMenuItem.Checked = false;
            horseFacesToolStripMenuItem.Checked = false;
            cowFacesToolStripMenuItem.Checked = false;

            duckFacesToolStripMenuItem.Checked = false;
            eagleFacesToolStripMenuItem.Checked = false;
            parrotFacesToolStripMenuItem.Checked = false;

            mouseFacesToolStripMenuItem.Checked = false;
            rabbitFacesToolStripMenuItem.Checked = false;
            squirrleFacesToolStripMenuItem.Checked = false;


        }

        private void justBlankToolStripMenuItem_Click(object sender, EventArgs e)
        {
            justBlankToolStripMenuItem.Checked = !justBlankToolStripMenuItem.Checked;
            if (justBlankToolStripMenuItem.Checked)
            {
                toolStripSplitButton1.Text = "Show Just Blank";
                toolStripSplitButton1.ForeColor = Color.Red;
                toolStripSplitButton1.Font = new Font(toolStripDropDownButton1.Font, FontStyle.Bold);
                UnCheckAll();
                justBlankToolStripMenuItem.Checked = true;
                SelectedStimuliIndices = null;
                SelectedStimuliIndices = new int[1] { 320 };
                RefreshPSTHs();
            }
        }


        private void facesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            facesToolStripMenuItem.Checked = !facesToolStripMenuItem.Checked;
            if (facesToolStripMenuItem.Checked)
            {
                toolStripSplitButton1.Text = "Show Faces";
                toolStripSplitButton1.ForeColor = Color.Red;
                toolStripSplitButton1.Font = new Font(toolStripDropDownButton1.Font, FontStyle.Bold);
                UnCheckAll();
                facesToolStripMenuItem.Checked = true;
                SelectedStimuliIndices = null;
                SelectedStimuliIndices = new int[225];
                for (int i = 0; i < 225; i++)
                    SelectedStimuliIndices[i] = i;
                RefreshPSTHs();
            }
        }

        private void noneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UnCheckAll();
            SelectedStimuliIndices = new int[NeuralSpace.GetLength(0)];
            toolStripSplitButton1.Text = "Filter None";
            toolStripSplitButton1.ForeColor = Color.Black;
            //toolStripSplitButton1.Font = new Font(toolStripSplitButton1.Font, );
            for (int i = 0; i < NeuralSpace.GetLength(0); i++)
                SelectedStimuliIndices[i] = i;
            RefreshPSTHs();
        }

        private void nonFacesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nonFacesToolStripMenuItem.Checked = !nonFacesToolStripMenuItem.Checked;
            if (nonFacesToolStripMenuItem.Checked)
            {
                toolStripSplitButton1.Text = "Show Non faces";
                toolStripSplitButton1.ForeColor = Color.Red;
                toolStripSplitButton1.Font = new Font(toolStripDropDownButton1.Font, FontStyle.Bold);
                UnCheckAll();
                nonFacesToolStripMenuItem.Checked = true;
                SelectedStimuliIndices = null;
                SelectedStimuliIndices = new int[96];
                for (int i = 0; i < 96; i++)
                    SelectedStimuliIndices[i] = 225 + i;
                RefreshPSTHs();
            }
        }

        private void primateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            primateToolStripMenuItem.Checked = !primateToolStripMenuItem.Checked;
            if (primateToolStripMenuItem.Checked)
            {
                toolStripSplitButton1.Text = "Show Primates";
                toolStripSplitButton1.ForeColor = Color.Red;
                toolStripSplitButton1.Font = new Font(toolStripDropDownButton1.Font, FontStyle.Bold);
                UnCheckAll();
                primateToolStripMenuItem.Checked = true;
                SelectedStimuliIndices = null;
                SelectedStimuliIndices = new int[45];
                for (int i = 0; i < 45; i++)
                    SelectedStimuliIndices[i] = i;
                RefreshPSTHs();
            }

        }

        private void reptileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reptileToolStripMenuItem.Checked = !reptileToolStripMenuItem.Checked;
            if (reptileToolStripMenuItem.Checked)
            {
                toolStripSplitButton1.Text = "Show Reptile";
                toolStripSplitButton1.ForeColor = Color.Red;
                toolStripSplitButton1.Font = new Font(toolStripDropDownButton1.Font, FontStyle.Bold);
                UnCheckAll();
                reptileToolStripMenuItem.Checked = true;
                SelectedStimuliIndices = null;
                SelectedStimuliIndices = new int[45];
                for (int i = 0; i < 15; i++)
                    SelectedStimuliIndices[i] = i + 45;
                for (int i = 15; i < 30; i++)
                    SelectedStimuliIndices[i] = i + 180 - 15;
                for (int i = 30; i < 45; i++)
                    SelectedStimuliIndices[i] = i + 210 - 30;
                RefreshPSTHs();
            }
        }

        private void farmeAnimalsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            farmeAnimalsToolStripMenuItem.Checked = !farmeAnimalsToolStripMenuItem.Checked;
            if (farmeAnimalsToolStripMenuItem.Checked)
            {
                toolStripSplitButton1.Text = "Show Farme Animals";
                toolStripSplitButton1.ForeColor = Color.Red;
                toolStripSplitButton1.Font = new Font(toolStripDropDownButton1.Font, FontStyle.Bold);
                UnCheckAll();
                farmeAnimalsToolStripMenuItem.Checked = true;
                SelectedStimuliIndices = null;
                SelectedStimuliIndices = new int[45];
                for (int i = 0; i < 15; i++)
                    SelectedStimuliIndices[i] = i + 60;
                for (int i = 15; i < 30; i++)
                    SelectedStimuliIndices[i] = i + 75 - 15;
                for (int i = 30; i < 45; i++)
                    SelectedStimuliIndices[i] = i + 120 - 30;
                RefreshPSTHs();
            }
        }

        private void birdsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            birdsToolStripMenuItem.Checked = !birdsToolStripMenuItem.Checked;
            if (birdsToolStripMenuItem.Checked)
            {
                toolStripSplitButton1.Text = "Show Birds";
                toolStripSplitButton1.ForeColor = Color.Red;
                toolStripSplitButton1.Font = new Font(toolStripDropDownButton1.Font, FontStyle.Bold);
                UnCheckAll();
                birdsToolStripMenuItem.Checked = true;
                SelectedStimuliIndices = null;
                SelectedStimuliIndices = new int[45];
                for (int i = 0; i < 15; i++)
                    SelectedStimuliIndices[i] = i + 90;
                for (int i = 15; i < 30; i++)
                    SelectedStimuliIndices[i] = i + 105 - 15;
                for (int i = 30; i < 45; i++)
                    SelectedStimuliIndices[i] = i + 150 - 30;
                RefreshPSTHs();
            }
        }

        private void rodentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rodentsToolStripMenuItem.Checked = !rodentsToolStripMenuItem.Checked;
            if (rodentsToolStripMenuItem.Checked)
            {
                toolStripSplitButton1.Text = "Show Rodents";
                toolStripSplitButton1.ForeColor = Color.Red;
                toolStripSplitButton1.Font = new Font(toolStripDropDownButton1.Font, FontStyle.Bold);
                UnCheckAll();
                rodentsToolStripMenuItem.Checked = true;
                SelectedStimuliIndices = null;
                SelectedStimuliIndices = new int[45];
                for (int i = 0; i < 15; i++)
                    SelectedStimuliIndices[i] = i + 135;
                for (int i = 15; i < 30; i++)
                    SelectedStimuliIndices[i] = i + 165 - 15;
                for (int i = 30; i < 45; i++)
                    SelectedStimuliIndices[i] = i + 195 - 30;
                RefreshPSTHs();
            }
        }

        private void carsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            carsToolStripMenuItem.Checked = !carsToolStripMenuItem.Checked;
            if (carsToolStripMenuItem.Checked)
            {
                toolStripSplitButton1.Text = "Show Cars";
                toolStripSplitButton1.ForeColor = Color.Red;
                toolStripSplitButton1.Font = new Font(toolStripDropDownButton1.Font, FontStyle.Bold);
                UnCheckAll();
                carsToolStripMenuItem.Checked = true;
                SelectedStimuliIndices = null;
                SelectedStimuliIndices = new int[5];
                for (int i = 0; i < 5; i++)
                    SelectedStimuliIndices[i] = i + 225;
                RefreshPSTHs();
            }
        }

        private void handsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            handsToolStripMenuItem.Checked = !handsToolStripMenuItem.Checked;
            if (handsToolStripMenuItem.Checked)
            {
                toolStripSplitButton1.Text = "Show Hands";
                toolStripSplitButton1.ForeColor = Color.Red;
                toolStripSplitButton1.Font = new Font(toolStripDropDownButton1.Font, FontStyle.Bold);
                UnCheckAll();
                handsToolStripMenuItem.Checked = true;
                SelectedStimuliIndices = null;
                SelectedStimuliIndices = new int[5];
                for (int i = 0; i < 5; i++)
                    SelectedStimuliIndices[i] = i + 230;
                RefreshPSTHs();
            }

        }

        private void housesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            housesToolStripMenuItem.Checked = !housesToolStripMenuItem.Checked;
            if (housesToolStripMenuItem.Checked)
            {
                toolStripSplitButton1.Text = "Show Houses";
                toolStripSplitButton1.ForeColor = Color.Red;
                toolStripSplitButton1.Font = new Font(toolStripDropDownButton1.Font, FontStyle.Bold);
                UnCheckAll();
                housesToolStripMenuItem.Checked = true;
                SelectedStimuliIndices = null;
                SelectedStimuliIndices = new int[5];
                for (int i = 0; i < 5; i++)
                    SelectedStimuliIndices[i] = i + 235;
                RefreshPSTHs();
            }
        }

        private void simpleShapesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            simpleShapesToolStripMenuItem.Checked = !simpleShapesToolStripMenuItem.Checked;
            if (simpleShapesToolStripMenuItem.Checked)
            {
                toolStripSplitButton1.Text = "Show Simple Shapes";
                toolStripSplitButton1.ForeColor = Color.Red;
                toolStripSplitButton1.Font = new Font(toolStripDropDownButton1.Font, FontStyle.Bold);
                UnCheckAll();
                simpleShapesToolStripMenuItem.Checked = true;
                SelectedStimuliIndices = null;
                SelectedStimuliIndices = new int[20];
                for (int i = 0; i < 20; i++)
                    SelectedStimuliIndices[i] = i + 240;
                RefreshPSTHs();
            }
        }

        private void objectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            objectsToolStripMenuItem.Checked = !objectsToolStripMenuItem.Checked;
            if (objectsToolStripMenuItem.Checked)
            {
                toolStripSplitButton1.Text = "Show objects";
                toolStripSplitButton1.ForeColor = Color.Red;
                toolStripSplitButton1.Font = new Font(toolStripDropDownButton1.Font, FontStyle.Bold);
                UnCheckAll();
                objectsToolStripMenuItem.Checked = true;
                SelectedStimuliIndices = null;
                SelectedStimuliIndices = new int[60];
                for (int i = 0; i < 60; i++)
                    SelectedStimuliIndices[i] = i + 260;
                RefreshPSTHs();
            }
        }

        private void averageFiringRateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SortBy = 1;
            CalculatePSTH();
            ShowPSTHs(0);
            averageFiringRateToolStripMenuItem.Checked = true;
            responseSDToolStripMenuItem.Checked = false;
            responseSDDescToolStripMenuItem.Checked = false;
            responseStabilityDescToolStripMenuItem.Checked = false;
            averageFiringRateAscToolStripMenuItem.Checked = false;
        }

        private void responseSDAscToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SortBy = 2;
            CalculatePSTH();
            ShowPSTHs(0);
            averageFiringRateToolStripMenuItem.Checked = false;
            responseSDToolStripMenuItem.Checked = true;
            responseSDDescToolStripMenuItem.Checked = false;
            responseStabilityDescToolStripMenuItem.Checked = false;
            averageFiringRateAscToolStripMenuItem.Checked = false;
        }

        private void responseSDDescToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SortBy = 3;
            CalculatePSTH();
            ShowPSTHs(0);
            averageFiringRateToolStripMenuItem.Checked = false;
            responseSDToolStripMenuItem.Checked = false;
            responseSDDescToolStripMenuItem.Checked = true;
            responseStabilityDescToolStripMenuItem.Checked = false;
            averageFiringRateAscToolStripMenuItem.Checked = false;
        }

        private void responseStabilityDescToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SortBy = 4;
            CalculatePSTH();
            ShowPSTHs(0);
            averageFiringRateToolStripMenuItem.Checked = false;
            responseSDToolStripMenuItem.Checked = false;
            responseSDDescToolStripMenuItem.Checked = false;
            responseStabilityDescToolStripMenuItem.Checked = true;
            averageFiringRateAscToolStripMenuItem.Checked = false;
        }

        private void averageFiringRateAscToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SortBy = 5;
            CalculatePSTH();
            ShowPSTHs(0);
            averageFiringRateToolStripMenuItem.Checked = false;
            responseSDToolStripMenuItem.Checked = false;
            responseSDDescToolStripMenuItem.Checked = false;
            responseStabilityDescToolStripMenuItem.Checked = false;
            averageFiringRateAscToolStripMenuItem.Checked = true;
        }

        private void kSTestWithEachTrialBaslineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckSignificanceofResponse = 3;
            compareToPerstimulusActivityForEachStimulusToolStripMenuItem1.Checked = false;
            kSTestWithEachTrialBaslineToolStripMenuItem.Checked = true;
            CalculatePSTH();
            ShowPSTHs(CurrentPage);
        }





        public void Reset()
        {
            SelectedStimuliIndices = null;
            MaximumFiringRate = 1;
            BestStimulusFiringRate = 1;
            BaseLineFiringRate = 0;
            CurrentPage = 0;
            ActualStimulusCount = 0;
        }
    }
}