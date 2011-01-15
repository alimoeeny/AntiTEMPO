using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.ComponentModel;

namespace NeuralData
{
    public enum NeuronIsolationState {
    Lost = 1, Poor = 2, Medium = 3, GoodEnough = 4, ExceedsExpectations = 5
    }

    public class Neuron
    {
        public Guid NeuronGUID;
        public int NeuronNumber;
        public string FileNameBase;
        public int RecordingChannel;
        public int BlocksDone;
        public int EpocheDuration;
        public int IsolationState;
        public string Monkey;
        public DateTime RecordingDate;
        public string Region;
        public string Grid;
        public string Chamber;
        public int GuideDepth;
        public int ElectrodeInitDepth;
        public int MicroDriveDepth;
        public int ElectImpedance;
        public string Comment;
        public string SessionNeuralDataFilename;

        public bool[][][] ResponseSpace;
        public int[,] Trials;

        public Neuron() 
        { 
        }

        public void Save(string NeuronsPath)
        {
            StreamWriter sw = File.CreateText(NeuronsPath + Path.GetFileNameWithoutExtension(SessionNeuralDataFilename) + ".nrn");
            sw.WriteLine(NeuronGUID.ToString());
            sw.WriteLine(NeuronNumber.ToString());
            sw.WriteLine(FileNameBase);
            sw.WriteLine(RecordingChannel);
            sw.WriteLine(IsolationState.ToString());
            sw.WriteLine(SessionNeuralDataFilename);
            sw.WriteLine(Comment);
            
            
            //sw.WriteLine(EpocheDuration);
            //sw.WriteLine(BlocksDone.ToString());
            //sw.WriteLine(Monkey);
            //sw.WriteLine(RecordingDate.ToString());
            //sw.WriteLine(Region);
            //sw.WriteLine(Grid);
            //sw.WriteLine(Chamber);
            //sw.WriteLine(GuideDepth);
            //sw.WriteLine(ElectrodeInitDepth.ToString());
            //sw.WriteLine(MicroDriveDepth.ToString());
            //sw.WriteLine(ElectImpedance.ToString());
            
            sw.Close();
        }

        public void Load(string filename)
        {
            StreamReader sr = File.OpenText(filename);
            NeuronGUID = new Guid(sr.ReadLine());
            NeuronNumber = Convert.ToInt32(sr.ReadLine());
            FileNameBase = sr.ReadLine(); 
            RecordingChannel = Convert.ToInt32(sr.ReadLine());
            IsolationState = Convert.ToInt32(sr.ReadLine());
            SessionNeuralDataFilename = sr.ReadLine();
            Comment = sr.ReadLine();
            
            
            //EpocheDuration = Convert.ToInt32(sr.ReadLine());
            //BlocksDone = Convert.ToInt32(sr.ReadLine());
            //Monkey = sr.ReadLine();
            //RecordingDate = Convert.ToDateTime(sr.ReadLine());
            //Region = sr.ReadLine();
            //Grid = sr.ReadLine();
            //Chamber = sr.ReadLine();
            //GuideDepth = Convert.ToInt32(sr.ReadLine());
            //ElectrodeInitDepth = Convert.ToInt32(sr.ReadLine());
            //MicroDriveDepth = Convert.ToInt32(sr.ReadLine());
            //ElectImpedance = Convert.ToInt32(sr.ReadLine());
            sr.Close();
        }

    
        public static bool TEMPONoiseReduction(int a)
        {
            if (a == 0) return false;
            else if (a < 65535) return true;
            else return false;
        }


        


    }
}
