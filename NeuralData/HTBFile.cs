/*********************************************************************
*
*	This header file defines basic structures and classes needed to
*	utilize HTB databases.
*
*	Copyright Roozbeh Kiani, BCS-Lab,	October-17-2001
*
*********************************************************************/
/*********************************************************************
*
*	This CPP file defines basic functions needed to
*	utilize HTB databases. See (int)HTB.H for more description.
*
*	Copyright Roozbeh Kiani, BCS-Lab,	October-17-2001
*
*********************************************************************/

// Rewritten in C# by Ali Moeeny Jan-16-2006, Professor Esteky's Lab
 
using System;
using System.IO;
using System.Security.AccessControl;

enum HFUNC 
{
    //8 database types of tempo
 SUM	= 0,		//signed, 16 bit, averaged 8 bit analog
 APP	= 1,		//signed, 8 bits, appended 8 bit analog
 USUM = 2,		//unsigned, 16 bits, averaged counts
 UAPP = 3,		//unsigned, 16 bits, appended counts
 ESUM = 4,		//unsigned, 16 bits, averaged events
 EAPP = 5,		//unsigned, 16 bits, appended events
 XSUM = 6,		//signed, 16 bits, averaged 12-16 bit analog
 XAPP = 7	//signed, 16 bits, appended 12-16 bit analog
}
    //Tempo's databases may contain 8 or 16 bit data, signed or unsigned,
   //as defined below


public class htbHeader //tagHtbHeader
{
    public char[] date = new char[26];				//time/date string (formatted by C's ctime function
    public Int32 /*long*/ ldate;					//binary time/date (by C's time function)
    public char[] cfg_file = new char[14]; 		//configuration file name
    public char[] pro_file = new char[14];			//protocol file name
    public char[] unused0 = new char[52];			//reserved
    public UInt32 speed;					//acquisition rate of ring buffer in speed_units,
                                            //acquisition rate in Hz = speed_units / speed
                                            //ring buffer sample set duration (ms) = 1000*speed / speed_units
                                            //database sample set duration (ms) = 1000*speed*(skip+1) / speed_units 
    public UInt32 alloc;					//number of bytes in file occupied by this database
                                            //including HTB header
    public Int32 /*long*/ offset;					//the offset of begining of epoch from trigger (in bins)
    public UInt32 period;				//length of database epoch (in bins)
    public UInt32 extension;			//cancel sensitivity time after epoch (in bins)
    public UInt16 skip;					//number of ring buffer bins - 1 per data base bins(TPB-1).
                                        //if acqusition rate of ring buffer is SPEED accumulation
                                        //of database happens at rate SPEED/(SKIP+1)
    public UInt16 first_channel; 	//first ring buffer channel to accumulate ( [0 ...] )
    public UInt16 nchannels;			//number of channels in a channel set ( [1 2 ...] )
    public UInt16 sweep_limit;		//maximum number of epochs to accumulate
    public UInt32 cancel_override;	//cancel bits to ignore for artifact rejection
    public Byte func;					//type of database
    public char unused;					//reserved
    public UInt16 tag;					//trigger tag for this database
    public UInt16 npages;				//reserved
    public UInt32 nsamples;				//reserved
    public UInt16 samples_per_page; //reserved
    public UInt16 sweep;				//number of accumulated epochs
    public UInt16 next_page;			//reserved
    public UInt16 next_off;			//reserved
    public char[] title = new char[80];				//database title
    public UInt32 speed_units;			//smallest representable unit of time
    public char[] filler = new char[268];			//reserved
    public int size()
    {
        return
            26 +
            sizeof(Int32 /*long*/) +
            14 +
            14 +
            52 +
            sizeof(UInt32) +
            sizeof(UInt32) +
           sizeof(Int32 /*long*/) +
           sizeof(UInt32) +
           sizeof(UInt32) +
           sizeof(UInt16) +
           sizeof(UInt16) +
           sizeof(UInt16) +
           sizeof(UInt16) +
           sizeof(UInt32) +
           sizeof(Byte) +
           sizeof(char) +
           sizeof(UInt16) + // tag;					//trigger tag for this database
           sizeof(UInt16) + // npages;				//reserved
           sizeof(UInt32) + // nsamples;				//reserved
           sizeof(UInt16) + // samples_per_page; //reserved
           sizeof(UInt16) + // sweep;				//number of accumulated epochs
           sizeof(UInt16) + // next_page;			//reserved
           sizeof(UInt16) + // next_off;			//reserved
           80 +				//database title
           sizeof(UInt32) + // speed_units;			//smallest representable unit of time
           268;			//reserved

    }
}

enum HTB
{
    SUCCESS			=0x00000000,		//HTB error values, To see corresponding error messages
    INVALIDNAME		=0x00000001,		//use <htbFile::errorString> function
    INVALIDNUMBER	=0x00000002,
    INVALIDHEADER	=0x00000003,
    MEMALLOCFAILED	=0x00000004,
    FILEREADFAILED	=0x00000005,
    UNKNOWNERROR	=0x00000255 //0xFFFFFFFF
}
public class htbFile
{
    string fileName;								//Name of file that contains HTB
    //protected HANDLE fileHandle;
    int dbCount;									//Number f databases stored in HTB file
    public readonly htbHeader[] dbHeader;							//htbHeader structure of all databases in the HTB file
    protected Int32[] dbOffset;								//offset of each htbHeader relative to file begining
    protected Int32 errorValue;

    //protected int extractDbCount();						//protected function called by Constructor, finds and number of DBs in HTB file and assigns this value to dbCount
    //protected void extractDbHeaders();					//protected function called by Constructor, loads all htbHeader into dbHeader structure array and also fills dbOffset with proper values


    //In all following functions DB numbers as well as EPOCH numbers
    //starts from ZERO
    public htbFile(string name)		//Constructor
    {
        fileName = name;
        dbCount = extractDbCount();								//find db number
        dbOffset = new Int32[dbCount];
        for (int i = 0; i < dbCount; i ++ ) dbOffset[i] = 0; 
        dbHeader = new htbHeader[dbCount];					//allocate memory for dbHeader and dbOffset
        extractDbHeaders();											//fill dbHeader and dbOffset with appropriate values
        errorValue = (int)HTB.SUCCESS;
    }
    //  ~htbFile ();                           //Destructor
//    public int getHtbCount() { return (anyError() ? (-1) : dbCount); }
    //public long getHtbOffset(int db) { return (anyError() || db >= dbCount ? (-1) : dbOffset[db]); }
    //public long getHtbSize(int db) { return (anyError() || db >= dbCount ? (-1) : dbHeader[db].alloc - sizeof(htbHeader)); }
    //public char getFileName(char str);		//returns file name containing this database this
    //note that enough memory is allocated to str pointer
    //and caller is responsible for freeing this memory
    //         htbHeader &getHtbHeader ( htbHeader &header , int db );	//returns htbHeader of requested database
    //         const htbHeader &getHtbHeader ( int db )	{ return dbHeader[db];	};	//overloaded function
    //void getHtb(int db);					//returns data included in a database, the caller is
    //responsible for freeing the allocated memory
    //Example : getHtb(0) returns first database in the file
    //void getEpoch(int db, int epoch);	//returns data includede in a database epoch, for average
    //databases this is equal to htbFile::getHtb
    //The caller is responsible for freeing allocated memory
    //Example : getEpoch(0,0) returns first Epoch of the first database
    //				(int)HTB.getEpoch ( 3 , (int)HTB.getHtbHeader(3).sweep-1 ) returns
    //					the last epoch of 4th database number
    //char errorString(Int32 errorNum);	//returns error string associated with each error value
    public int extractDbCount()
    {
        Int32 nBytes;
        htbHeader htbheader = new htbHeader();
        
        int num = 0;
        Int32 offset = 0;
        FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
        BinaryReader br = new BinaryReader(fileStream);
        try
        {
            while (true)
            {
                try
                {
                    br.BaseStream.Position = offset + 114;
                    int n = br.ReadInt32();
                    offset += n;
                    num++;
                }
                catch { break;}
            }
        }
        finally { br.Close(); fileStream.Close(); }
        return num;
    }

    public void extractDbHeaders()
    {
        Int32 nBytes;
        //dbOffset = 0;
        FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
        BinaryReader br = new BinaryReader(fileStream);
        try
        {
            for (int db = 0; db < dbCount; db++)
            {
                dbHeader[db] = new htbHeader();
                dbHeader[db].date           = br.ReadChars(26);
                dbHeader[db].ldate          = br.ReadInt32();//offset + 26);
                dbHeader[db].cfg_file       = br.ReadChars(14);
                dbHeader[db].pro_file = br.ReadChars(14);
                dbHeader[db].unused0 = br.ReadChars(52);
                dbHeader[db].speed = br.ReadUInt32();// offset + 110);
                dbHeader[db].alloc = br.ReadUInt32();
                dbHeader[db].offset = br.ReadInt32(); // offset + 118);
                dbHeader[db].period = br.ReadUInt32(); // GetULONG(fid, offset + 122);
                dbHeader[db].extension = br.ReadUInt32(); // GetULONG(fid, offset + 126);
                dbHeader[db].skip = br.ReadUInt16();// GetUSHORT(fid, offset + 130);
                dbHeader[db].first_channel = br.ReadUInt16();//  GetUSHORT(fid, offset + 132);
                dbHeader[db].nchannels = br.ReadUInt16();// GetUSHORT(fid, offset + 134);
                dbHeader[db].sweep_limit = br.ReadUInt16();// GetUSHORT(fid, offset + 136);
                dbHeader[db].cancel_override = br.ReadUInt32();// GetULONG(fid, offset + 138);
                dbHeader[db].func = br.ReadByte(); // GetUCHAR(fid, offset + 142);
                br.ReadByte();
                dbHeader[db].tag = br.ReadUInt16(); // GetUSHORT(fid, offset + 144);
                //br.ReadByte();
                dbHeader[db].npages = br.ReadUInt16(); //GetUSHORT(fid, offset + 146);
                dbHeader[db].nsamples = br.ReadUInt32(); //GetULONG(fid, offset + 148);
                dbHeader[db].samples_per_page = br.ReadUInt16(); //GetUSHORT(fid, offset + 152);
                dbHeader[db].sweep = br.ReadUInt16();// GetUSHORT(fid, offset + 154);
                dbHeader[db].next_page = br.ReadUInt16();// GetUSHORT(fid, offset + 156);
                dbHeader[db].next_off = br.ReadUInt16();// GetUSHORT(fid, offset + 158);
                dbHeader[db].title = br.ReadChars(80); // GetString(fid, offset + 160, 80);
                dbHeader[db].speed_units = br.ReadUInt32(); // GetULONG(fid, offset + 240);
                dbHeader[db].filler = br.ReadChars(268);

                dbOffset[0] = 0;
                for (int i = 1; i < db+1; i++)
                {
                    dbOffset[db] = dbOffset[db] + (int)dbHeader[i - 1].alloc;
                }
                br.BaseStream.Position += dbHeader[db].alloc - dbHeader[db].size() + 1;
            }
        }
        finally { br.Close(); fileStream.Close(); }
   }


    public short[] getHtb(int db)
    {
        Int32 nBytes;

        short[] buffer = new short[dbHeader[db].alloc - dbHeader[db].size()];
        FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
        BinaryReader br = new BinaryReader(fileStream);
        br.BaseStream.Position = dbOffset[db] + dbHeader[db].size();
        try
        {
            for (int i = 0; i < buffer.Length; i++)
                buffer[i] = br.ReadInt16();
        }
        finally { br.Close(); fileStream.Close(); }
    return buffer;
    }



    public ushort[,] getEpoch(int db, int epoch)
    {
      Int32 nBytes , stepsToRead = 0;
      ushort[,] buffer;
      
      switch ( dbHeader[db].func )
       { case (int)HFUNC.SUM :
         case (int)HFUNC.XSUM :
         case (int)HFUNC.XAPP :
             stepsToRead = (int)(dbHeader[db].period * dbHeader[db].nchannels * sizeof(Int16));
             break;
         case (int)HFUNC.APP :
             stepsToRead = (int)(dbHeader[db].period * dbHeader[db].nchannels * sizeof(SByte));
			    break;
         case (int)HFUNC.USUM :
         case (int)HFUNC.UAPP :
         case (int)HFUNC.ESUM :
         case (int)HFUNC.EAPP :
     		    stepsToRead = (int)(dbHeader[db].period * dbHeader[db].nchannels * sizeof(UInt16)) / (dbHeader[db].nchannels * 2/*16 bit each*/);
             break;
         }
      switch ( dbHeader[db].func )
       { case (int)HFUNC.SUM :
         case (int)HFUNC.XSUM :
         case (int)HFUNC.USUM :
         case (int)HFUNC.ESUM :	epoch = 0;	break;		}

        buffer = new ushort[stepsToRead, dbHeader[db].nchannels];
   
//      SetFilePointer ( fileHandle , dbOffset[db]+sizeof(htbHeader)+bytesToRead*epoch , 0 , FILE_BEGIN );
      FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
      BinaryReader br = new BinaryReader(fileStream);
      try
      {
          br.BaseStream.Position = dbOffset[db] + dbHeader[db].size() + (stepsToRead * epoch * dbHeader[db].nchannels * 2) - 1;
          for (int i = 0; i < stepsToRead; i++)
          {
              for (int j = 0; j < dbHeader[db].nchannels; j++)
                  buffer[i, j] = br.ReadUInt16();
          }
      //if ( !ReadFile(fileHandle,buffer,bytesToRead,&nBytes,0) ||
  }
  finally { br.Close(); fileStream.Close(); }
  return buffer;
    }

}



//typedef int16 HTYPE_SUM;
//typedef SByte HTYPE_APP;
//typedef unint16 HTYPE_USUM;
//typedef unint16 HTYPE_UAPP;
//typedef unint16 HTYPE_ESUM;
//typedef unint16 HTYPE_EAPP;
//typedef int16 HTYPE_XSUM;
//typedef int16 HTYPE_XAPP; 




