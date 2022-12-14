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
		public string testPath2 = @"PhysionetTestFile.txt";
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
        public TestMeasurementDataAccess() { }

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
        public List<double> GetOneSecond()
        {
            List<double> rawdata = new List<double>();
            rawdata.Add(0);
            return rawdata;
        }
    }
}
