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
		public string TestPath = @"testmedtal.txt";
		public string testPath2 = @"udpFormatTestFile.txt";
		private bool shallStop = false;


		/// <summary>
		/// TESTMETODE TIL FILEN testmedtal.txt
		/// </summary>
		/// <returns></returns>
		//public List<BPMesDataGUI_DTO> ReadSampleTest()
		//{
		//	do
		//	{
		//		while (!shallStop) //Skal kører så længe shallstop er true (skal ændres til start/stop knap på GUI)
		//		{
		//			List<BPMesDataGUI_DTO> samplesList = new List<BPMesDataGUI_DTO>();
		//			List<string> holder = File.ReadAllLines(TestPath).ToList();

  //                  string[] input = holder[0].Split(' ');
              
		//			foreach (string sample in holder)
		//			{
		//				//string[] input = sample.Split(' ');
		//				BPMesDataGUI_DTO s = new BPMesDataGUI_DTO(Convert.ToInt32(input[0]), Convert.ToInt32(input[1]), Convert.ToInt32(input[2]), Convert.ToInt32(input[3])); //gemmer intput 1, 2, 3.. i DTO
						
		//				samplesList.Add(s);
		//			}
		//			return samplesList;
		//		}
		//	}
		//	while (true);
		//}

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
					BPMesDataGUI_DTO dtoObjTest = null;
					List<double> rawDataList = new List<double>();
					List<string> holder = File.ReadAllLines(TestPath).ToList();

					/////////Noget RPi kan skrive, hvis vi laver det om, så vi får data på flere linjer. /////////////
					//string[] test = new string[] { "10", "12", "13" };

					//File.WriteAllLines(testPath2,test);

					/////////////Ny metode som skal bruges hvis dataen kommer i flere linjer.///////
					foreach (string sample in holder)
					{
						//string[] input = sample.Split(' ');

						rawDataList.Add(Convert.ToDouble(sample));
					}

					///////////Ny metode som skal bruges hvis dataen kommer i flere linjer. Split er rykket ud fra forloopet, for at den ikke skal splittes hver gang den kører loopet.
					string[] input = holder[0].Split(' ');
					for (int i = 4; i < input.Length; i++)
					{
						rawDataList.Add(Convert.ToDouble(input[i]));
					}

					foreach (string sample in holder)
					{
						//string[] input = sample.Split(' ');
						dtoObjTest = new BPMesDataGUI_DTO(Convert.ToInt32(input[0]), Convert.ToInt32(input[1]), Convert.ToInt32(input[2]), Convert.ToInt32(input[3]),rawDataList);
					}

					return dtoObjTest;
				}
			}
			while (true);
		}
	}
}
