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
        public Single[] PSTH;
        public bool[,] Raster;
        public int[] ResponseData;
        public int TrialDuration;
        public int StimulusDuration;
        public int StimulusOnset;
        public int ResponseOffset;
        public int ResponseOnset;
        public double AverageFiringRate;
        public Single Rank = 0;
        public bool IsSignificantResponse = false;
        
        private Image PSTHBG;
        private static Color BGColor = Color.FromArgb(200,200,200);//(10, 14, 86);
        private Pen StimPen = new Pen(new HatchBrush(HatchStyle.DarkHorizontal, Color.Blue, BGColor));
        private Pen RespPen = new Pen(new HatchBrush(HatchStyle.DarkHorizontal, Color.Red ,      BGColor));
        private Brush TextBrush = new SolidBrush(Color.White);
        private Color NonSignificantColor = Color.FromArgb(92,92,151);
        private Color SignificantColor = Color.FromArgb(192, 192, 225);

        public StimulusPSTH()
        {
            InitializeComponent();
            PSTH = new Single[100];
        }

        private void LoadStimulus(string stimulusfilename)
        {
            StimulusPictureBox.Load(stimulusfilename);  
        }

        public void UpdateStimulus()
        {
            LoadStimulus(StimulusFileName);
        }

        public void PlotPSTH()
        {
            //PSTHBG = new Bitmap(@"D:\Rasanic\AntiTEMPO\StimulusPSTH\bin\Debug\PSTH.jpg");
            PSTHBG = new Bitmap(PSTHPictureBox.Width, PSTHPictureBox.Height);
            Graphics g = Graphics.FromImage(PSTHBG);
            g.FillRectangle(new SolidBrush(BGColor), new Rectangle(0, 0, PSTHPictureBox.Width, PSTHPictureBox.Height));
            PlotAxes(g);
            float SizeFactor = (float)PSTHPictureBox.Width / (float)PSTH.Length;
            Pen PSTHRPen = new Pen(Color.Red, 0.8f*SizeFactor);
            Pen PSTHPen = new Pen(Color.Green, 0.8f * SizeFactor);
            Pen CurrentPen = PSTHPen;
            for (int i = 0; i < PSTH.Length; i++)
            {
                if (IsSignificantResponse && (i > (ResponseOnset * PSTH.Length / 700)) && (i < (ResponseOffset * PSTH.Length / 700)))
                    CurrentPen = PSTHRPen;
                else
                    CurrentPen = PSTHPen;
                g.DrawLine(CurrentPen, i * SizeFactor, PSTHPictureBox.Height, i * SizeFactor, (float)(PSTHPictureBox.Height - (PSTH[i] * PSTHPictureBox.Height / MaximumFiringRate)));
            }
            PSTHPictureBox.Image = PSTHBG;
        }

        private void PlotAxes(Graphics g)
        {
            Single tmp = StimulusOnset * PSTHPictureBox.Width / 700;
            Font fnt = new Font("Verdana", 6);
            g.DrawLine(StimPen, tmp, 0, tmp, PSTHPictureBox.Height);
            g.DrawString("0", fnt, TextBrush, tmp + 2, 3);
            tmp = (StimulusOnset + StimulusDuration) * PSTHPictureBox.Width / 700;
            g.DrawLine(StimPen, tmp, 0, tmp, PSTHPictureBox.Height);
            g.DrawString(StimulusDuration.ToString(), fnt, TextBrush, tmp + 2, 3);
            
            tmp = ResponseOnset * PSTHPictureBox.Width / 700;
            g.DrawLine(RespPen, tmp, 0, tmp, PSTHPictureBox.Height);
            g.DrawString((ResponseOnset - StimulusOnset).ToString(), fnt, TextBrush, tmp + 2, 3);
            tmp = ResponseOffset * PSTHPictureBox.Width / 700;
            g.DrawLine(RespPen, tmp, 0, tmp, PSTHPictureBox.Height);
            g.DrawString((ResponseOffset -StimulusOnset).ToString(), fnt, TextBrush, tmp + 2, 3);

        }

        public void UpdateLabels()
        {
            if (IsSignificantResponse) panel1.BackColor = SignificantColor; else panel1.BackColor = NonSignificantColor;
            StimulusLabel.Text = "Stimulus : " + StimulusName;
            TrialsLabel.Text = "Trials : " + TrialsCount.ToString();
            RankLabel.Text = "Rank : " + Rank.ToString();
            MaxmimumFRLabel.Text = MaximumFiringRate.ToString("G3");
            AvgFRLabel.Text = "Avg FR : " + AverageFiringRate.ToString("G3");
            RatioLabel.Text = "./Best: " + (AverageFiringRate / BestStimFiringRate).ToString("G3");
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

        }
    }
}