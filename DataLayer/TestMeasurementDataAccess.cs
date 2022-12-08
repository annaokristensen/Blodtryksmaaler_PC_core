using DTO_PC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Concurrent;
using System.Resources;

namespace DataLayer_PC
{
	public class TestMeasurementDataAccess : IMeasurementDataAccess
	{
        private readonly BlockingCollection<double> RawDataBlocking;
        private readonly Datacontainer datacontainer;
        public string TestPath = @"testmedtal.txt";
		public string testPath2 = @"udpFormatTestFile.txt";
		private bool shallStop = false;

        public TestMeasurementDataAccess()
        {
            RawDataBlocking = new BlockingCollection<double>();
        }

        /// <summary>
        /// TEST-METODE til at læse en test-fil som er skrevet på det format, som vi modtager fra rpi
        /// </summary>
        /// <returns></returns>
        public BPMesDataGUI_DTO ReadSample()
        {
            //Sætter udpPath til at være den string som udpServeren returnerer. Det er deri at data fra rpi står
            do
            {
                while (!shallStop)
                {
                    //List<double> rawDataList = new List<double>();
                    List<string> holder = File.ReadAllLines(testPath2).ToList();

                    foreach (string sample in holder)
                    {
                        RawDataBlocking.Add(Convert.ToDouble(sample));
                    }
                   // return RawDataBlocking;
                }
            }
            while (true);
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
