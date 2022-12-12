using DTO_PC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Concurrent;
using System.Resources;
using System.Diagnostics.Tracing;

namespace DataLayer_PC
{
	public class TestMeasurementDataAccess : IMeasurementDataAccess
	{
        private readonly BlockingCollection<Datacontainer> RawDataBlocking;
        //private readonly Datacontainer datacontainer;
        public string TestPath = @"testmedtal.txt";
		public string testPath2 = @"udpFormatTestFile.txt";
		private bool shallStop = false;
        BPMesDataGUI_DTO rawDataDTOBC;
        private int countStart = 0;
        private int countEnd = 200;
        public List<string> holder;
        
        public TestMeasurementDataAccess(BlockingCollection<Datacontainer> RawDataBlocking)
        {
            this.RawDataBlocking = RawDataBlocking;
            rawDataDTOBC = new BPMesDataGUI_DTO();
            holder = new List<string>();
        }

        /// <summary>
        /// TEST-METODE til at læse en test-fil som er skrevet på det format, som vi modtager fra rpi
        /// </summary>
        /// <returns></returns>
        public void ReadSample()
        {
            //Sætter udpPath til at være den string som udpServeren returnerer. Det er deri at data fra rpi står
            do
            {
                while(!shallStop)
                {
                    //List<double> rawDataList = new List<double>();
                    holder = File.ReadAllLines(testPath2).ToList();
                    List<double> rawdata = new List<double>();

                    for (int i = countStart; i < countEnd; i++)
                    {
                        rawdata.Add(Convert.ToDouble(holder[i]));
                    }
                    Datacontainer reading = new Datacontainer();
                    reading.SetRawData(rawdata);
                    RawDataBlocking.Add(reading);
                }
            }
            while (countStart < holder.Count);

            RawDataBlocking.CompleteAdding(); //Når den er færdig med at putte data ind, så completer den. 
        }

        public BPMesDataGUI_DTO TakeFromBC()
        {
            while (!RawDataBlocking.IsCompleted) //Tjekker om der er completet i ReadSample
            {
                try
                {
                    //rawDataDTOBC.RawDataList.Add(RawDataBlocking.Take());
                    return rawDataDTOBC;
                }
                catch (InvalidOperationException)
                {
                    return null;
                }
            }
            return null;
        }





        //public BPMesDataGUI_DTO ReadSample()
        //{
        //	//Sætter udpPath til at være den string som udpServeren returnerer. Det er deri at data fra rpi står
        //	do
        //	{
        //		while (!shallStop)
        //		{
        //			List<double> rawDataList = new List<double>();
        //			List<string> holder = File.ReadAllLines(testPath2).ToList();

        //			foreach (string sample in holder)
        //			{
        //				rawDataList.Add(Convert.ToDouble(sample));
        //			}
        //			BPMesDataGUI_DTO dtoObjTest = new BPMesDataGUI_DTO(rawDataList);

        //			return dtoObjTest;
        //		}
        //	}
        //	while (true);
        //}

    }
}
