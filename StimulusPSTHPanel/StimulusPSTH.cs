using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace StimulusPSTH
{
    public partial class StimulusPSTH : UserControl
    {
        public string StimulusName;// { get { return StimulusName; } set { StimulusLabel.Text = "Stimulus : " + value; }        }
        public string StimulusFileName;// { get {return StimulusFileName;}  set {LoadStimulus(value);} }
        public int TrialsCount; //{ get {return TrialsCount; } set {TrialsLabel.Text = "Trials : " + TrialsCount.ToString();}}
        public double MaximumFiringRate = 1;
        public double BestStimFiringRate = 1;
        public double[] PSTH;
        public bool[,] Raster;
        public int[] ResponseData;
        public int TrialDuration;
        public int StimulusDuration;
        public int StimulusOnset;
        public int ResponseOffset;
        public int ResponseOnset;
        public double AverageFiringRate;
        public double ResponseSE;
        public double ResponseStability;
        public Single Rank = 0;
        public bool IsSignificantResponse = false;
        public Color TrialTypeFlag { set { BackColor = value; } get { return BackColor; } }
        
        private Image PSTHBG;
        private static Color BGColor = Color.FromArgb(150,150,150);//(10, 14, 86);
        private Pen StimPen = new Pen(new HatchBrush(HatchStyle.DarkHorizontal, Color.Blue, BGColor));
        private Pen RespPen = new Pen(new HatchBrush(HatchStyle.DarkHorizontal, Color.Red ,      BGColor));
        private Brush TextBrush = new SolidBrush(Color.White);
        private Color NonSignificantColor = Color.FromArgb(92,92,151);
        private Color SignificantColor = Color.FromArgb(192, 192, 225);
        private bool Rasterflipflop = false; // false : PSTH, true Raster

        public StimulusPSTH()
        {
            InitializeComponent();
            PSTH = new double[100];
        }

        private void LoadStimulus(string stimulusfilename)
        {
            try
            {
                StimulusPictureBox.Load(stimulusfilename);
            }catch{StimulusPictureBox.Image=null;}
        }

        public void UpdateStimulus()
        {
            LoadStimulus(StimulusFileName);
        }

        public void PlotPSTH()
        {
            PSTHBG = new Bitmap(PSTHPictureBox.Width, PSTHPictureBox.Height);
            Graphics g = Graphics.FromImage(PSTHBG);
            g.FillRectangle(new SolidBrush(BGColor), new Rectangle(0, 0, PSTHPictureBox.Width, PSTHPictureBox.Height));
            PlotAxes(g);
            float SizeFactor = (float)PSTHPictureBox.Width / (float)PSTH.Length;
            //Pen PSTHRPen = new Pen(Color.Red, 0.8f*SizeFactor);
            //Pen PSTHPen = new Pen(Color.Green, 0.8f * SizeFactor);
            Pen PSTHRPen = new Pen(Color.Red, 0.8f*SizeFactor);
            Pen PSTHPen = new Pen(Color.Black, 0.8f * SizeFactor);
            Pen CurrentPen = PSTHPen;
            for (int i = 0; i < PSTH.Length; i++)
            {
                if (IsSignificantResponse && (i > (ResponseOnset * PSTH.Length / 700)) && (i < (ResponseOffset * PSTH.Length / 700)))
                    CurrentPen = PSTHRPen;
                else
                    CurrentPen = PSTHPen;
                g.DrawLine(CurrentPen, i * SizeFactor, (float)PSTHPictureBox.Height, i * SizeFactor, (float)(PSTHPictureBox.Height - (PSTH[i] * PSTHPictureBox.Height / MaximumFiringRate)));
            }
            PSTHPictureBox.Image = PSTHBG;
        }

        private void PlotAxes(Graphics g)
        {
            Single tmp = StimulusOnset * g.VisibleClipBounds.Width / 700;
            Font fnt = new Font("Verdana", 6.0f * g.VisibleClipBounds.Width/250.0f);
            g.DrawLine(StimPen, tmp, 0, tmp, g.VisibleClipBounds.Height);
            g.DrawString("0", fnt, TextBrush, tmp + 2, 3);
            tmp = (StimulusDuration + StimulusOnset) * g.VisibleClipBounds.Width / 700;
            g.DrawLine(StimPen, tmp, 0, tmp, g.VisibleClipBounds.Height);
            g.DrawString(StimulusDuration.ToString(), fnt, TextBrush, tmp + 2, 3);
            
            tmp = ResponseOnset * g.VisibleClipBounds.Width / 700;
            g.DrawLine(RespPen, tmp, 0, tmp, g.VisibleClipBounds.Height);
            g.DrawString((ResponseOnset - StimulusOnset).ToString(), fnt, TextBrush, tmp + 2, 3);
            tmp = ResponseOffset * g.VisibleClipBounds.Width / 700;
            g.DrawLine(RespPen, tmp, 0, tmp, g.VisibleClipBounds.Height);
            g.DrawString((ResponseOffset).ToString(), fnt, TextBrush, tmp + 2, 3);

        }

        public void UpdateLabels()
        {
            //if (IsSignificantResponse) panel1.BackColor = SignificantColor; else panel1.BackColor = NonSignificantColor;
            StimulusLabel.Text = StimulusName;
            TrialsLabel.Text = "Trials : " + TrialsCount.ToString();
            RankLabel.Text = "Rank : " + Rank.ToString();
            MaxmimumFRLabel.Text = MaximumFiringRate.ToString("G3");
            AvgFRLabel.Text = "Avg FR : " + AverageFiringRate.ToString("G3");
            ResponseSDLabel.Text = "R. SE: " + ResponseSE.ToString("G3");
            ResponseStabilityLabel.Text = "R. Stab: " + ResponseStability.ToString("G3");
            RatioLabel.Text = "./Best:" + (AverageFiringRate / BestStimFiringRate).ToString("G2");

        }


        public void Reset()
        {
            StimulusName = "";
            TrialsCount = 0;
            Rank = 0;
            MaximumFiringRate = 0;
            AverageFiringRate = 0;
            StimulusFileName = "";
            PSTH = null;
            ResponseData = null;
            TrialDuration = 0;
            panel1.BackColor = NonSignificantColor;
            StimulusDuration = 0;
            StimulusOnset = 0;
            StimulusPictureBox.Image = new Bitmap(StimulusPictureBox.Width, StimulusPictureBox.Height);
            //StimulusPictureBox.Refresh();
            PSTHPictureBox.Image = new Bitmap(PSTHPictureBox.Width, PSTHPictureBox.Height);
            UpdateLabels();
        }

        private void PSTHPictureBox_DoubleClick(object sender, EventArgs e)
        {
            if (!Rasterflipflop)
                PlotRaster();
            else
                PlotPSTH();
            Rasterflipflop = !Rasterflipflop;
        }

        private void PlotRaster()
        {
            int topoffset = 10;
            Pen rasterpen = new Pen(Color.Black, 1);
            PSTHBG = new Bitmap(PSTHPictureBox.Width, PSTHPictureBox.Height);
            Graphics g = Graphics.FromImage(PSTHBG);
            g.FillRectangle(new SolidBrush(BGColor), new Rectangle(0, 0, PSTHPictureBox.Width, PSTHPictureBox.Height));
            PlotAxes(g);
            Single resizefactor = (Single)PSTHPictureBox.Width / (Single)Raster.GetLength(1);
            for (int i = 0; i < Raster.GetLength(0); i++)
                for (int j = 0; j < Raster.GetLength(1); j++)
                    if (Raster[i, j])
                        g.DrawLine(rasterpen, j * resizefactor, i * 5.0f + topoffset, j * resizefactor, i * 5.0f + 4 + topoffset);
            PSTHPictureBox.Image = PSTHBG;
        }

       

        private void StimulusPictureBox_Click(object sender, EventArgs e)
        {
            int topoffset = 40;
            Pen rasterpen = new Pen(Color.Black, 1);
            RasterForm rf = new RasterForm();
            PSTHBG = new Bitmap(rf.RasterPictureBox.Width, rf.RasterPictureBox.Height);
            Graphics g = Graphics.FromImage(PSTHBG);
            g.FillRectangle(new SolidBrush(BGColor), new Rectangle(0, 0, rf.RasterPictureBox.Width, rf.RasterPictureBox.Height));
            PlotAxes(g);

            /////////  Plot Raster  /////////////
            Single resizefactor = 1; // (Single)PSTHPictureBox.Width / (Single)Raster.GetLength(1);
            for (int i = 0; i < Raster.GetLength(0); i++)
                for (int j = 0; j < Raster.GetLength(1); j++)
                    if (Raster[i, j])
                        g.DrawLine(rasterpen, j * resizefactor, i * 5.0f + topoffset, j * resizefactor, i * 5.0f + 4 + topoffset);

            ///////// Plot PSTH   //////////////
            float SizeFactor = (float)rf.RasterPictureBox.Width / (float)PSTH.Length;
            Pen PSTHRPen = new Pen(Color.Red, 0.8f * SizeFactor);
            Pen PSTHPen = new Pen(Color.Green, 0.8f * SizeFactor);
            Pen CurrentPen = PSTHPen;
            for (int i = 0; i < PSTH.Length; i++)
            {
                if (IsSignificantResponse && (i > (ResponseOnset * PSTH.Length / 700)) && (i < (ResponseOffset * PSTH.Length / 700)))
                    CurrentPen = PSTHRPen;
                else
                    CurrentPen = PSTHPen;
                g.DrawLine(CurrentPen, i * SizeFactor, rf.RasterPictureBox.Height, i * SizeFactor, (float)(rf.RasterPictureBox.Height - (PSTH[i] * (double)rf.RasterPictureBox.Height / MaximumFiringRate)));
            }

            ///////////////////////
            rf.RasterPictureBox.Image = PSTHBG;
            rf.StimulusPictureBox.Load(StimulusFileName);
            rf.ShowDialog();
        }

        private void ResponseSDLabel_Click(object sender, EventArgs e)
        {

        }
    }
}