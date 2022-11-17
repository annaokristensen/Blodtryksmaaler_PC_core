using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Runtime.InteropServices.ComTypes;
using DTO_PC;
using System.IO;
using System.Collections.Concurrent;

namespace DataLayer_PC
{
	/// <summary>
	/// Klassen tilgår databasen og gemmer måledata deri
	/// </summary>
	public class MeasurementDataAccess
	{
		private readonly BlockingCollection<BPMesDataGUI_DTO> samplesList = new BlockingCollection<BPMesDataGUI_DTO>();
		public string TestPath = @"testmedtal.txt";
		public string testPath2 = @"udpFormatTestFile.txt";
		public double Second { get; set; }
		public double SampleValue { get; set; }
		public List<MeasurementDataAccess> LoadedSampleValue;
		private bool shallStop = false;
		public UDPServer udpServerObj = new UDPServer();

		public MeasurementDataAccess()
		{
			
		}
		public MeasurementDataAccess(double second, double sampleValue)
		{
			//conn = new SqlConnection(@"INDSÆT CONNECTION STRING TIL MEASUREMENT-DATABASE")
			Second = second;
			SampleValue = sampleValue;
		}
		public MeasurementDataAccess(BlockingCollection<BPMesDataGUI_DTO> samplesList)
		{
			this.samplesList = samplesList;
		}

		//Vi returnerer ét objekt af en DTO i stedet for at returnere en liste af DTO'er. Så skal vi kalde metoden kontinuerligt
		public BPMesDataGUI_DTO ReadValues()
		{
			//Sætter udpPath til at være den string som udpServeren returnerer. Det er deri at data fra rpi står
			string udpPath = udpServerObj.GetBroadcast();
			do
			{
				while (!shallStop)
				{
					BPMesDataGUI_DTO dtoObj;
					List<double> rawDataList = new List<double>();
					List<string> holder = File.ReadAllLines(udpPath).ToList();

					foreach (string sample in holder)
					{
						string[] input = sample.Split(' ');

							rawDataList.Add(Convert.ToDouble(sample));
					}

					for (int i = 0; i < 4; i++)
					{
						rawDataList.Remove(i);
					}

					foreach (string sample in holder)
					{
						string[] input = sample.Split(' ');
						dtoObj = new BPMesDataGUI_DTO(Convert.ToInt32(input[0]), Convert.ToInt32(input[1]), Convert.ToInt32(input[2]), Convert.ToInt32(input[3]), rawDataList);
					}

					return dtoObj;
				}
			}
			while (true);
		}

		/// <summary>
		/// TESTMETODE TIL FILEN testmedtal.txt
		/// </summary>
		/// <returns></returns>
		public List<BPMesDataGUI_DTO> ReadSampleTest()
		{
			do
			{
				while (!shallStop) //Skal kører så længe shallstop er true (skal ændres til start/stop knap på GUI)
				{
					List<BPMesDataGUI_DTO> samplesList = new List<BPMesDataGUI_DTO>();
					List<string> holder = File.ReadAllLines(TestPath).ToList();
					foreach (string sample in holder)
					{
						string[] input = sample.Split(' ');
						BPMesDataGUI_DTO s = new BPMesDataGUI_DTO(Convert.ToInt32(input[0]), Convert.ToInt32(input[1]), Convert.ToInt32(input[2]), Convert.ToInt32(input[3])); //gemmer intput 1, 2, 3.. i DTO
						samplesList.Add(s);
					}
					return samplesList;
				}
			}
			while (true);
		}

		public BPMesDataGUI_DTO TestReadValues()
		{
			//Sætter udpPath til at være den string som udpServeren returnerer. Det er deri at data fra rpi står
			
			do
			{
				while (!shallStop)
				{
					BPMesDataGUI_DTO dtoObjTest = null;
					List<double> rawDataList = new List<double>();
					List<string> holder = File.ReadAllLines(testPath2).ToList();

					/////////Noget RPi kan skrive, hvis vi laver det om, så vi får data på flere linjer. /////////////
					//string[] test = new string[] { "10", "12", "13" };

     //               File.WriteAllLines(testPath2,test);


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

					/////////Denne metode kan vi slette også bruge den ovenståënde///////////
					//for (int i = 0; i < 4; i++)
					//{
					//	rawDataList.Remove(i);
					//}

					foreach (string sample in holder)
					{
						//string[] input = sample.Split(' ');
						dtoObjTest = new BPMesDataGUI_DTO(Convert.ToInt32(input[0]), Convert.ToInt32(input[1]), Convert.ToInt32(input[2]), Convert.ToInt32(input[3]), rawDataList);
					}

					return dtoObjTest;
				}
			}
			while (true);
		}


	}
}
