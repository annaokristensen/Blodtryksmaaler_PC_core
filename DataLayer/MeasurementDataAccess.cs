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
		public double Second { get; set; }
		public double SampleValue { get; set; }
		public List<MeasurementDataAccess> LoadedSampleValue;
		private bool shallStop = false;
       
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

		
		/// <summary>
		/// Test-metode til at læse fra vores egen testfilt
		/// </summary>
		/// <returns></returns>
		public List<BPMesDataGUI_DTO> ReadSampleTest()
		{
			do
			{
				while(!shallStop) //Skal kører så længe shallstop er true (skal ændres til start/stop knap på GUI)
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
			while(true);
		}

		//TODO: Path er ikke en tekstfil - i stedet læser vi fra bytes
		public string udpPath = @"testmedtal.txt";

		//TODO: Skal laves om til at returnere 1 DTO i stedet for en liste af DTO'er. DTO'en returneres så kontinuerligt
		public List<BPMesDataGUI_DTO> ReadCalculatedValues()
		{
			do
			{
				while (!shallStop)
				{
					List<BPMesDataGUI_DTO> samplesList = new List<BPMesDataGUI_DTO>();
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
						BPMesDataGUI_DTO s = new BPMesDataGUI_DTO(Convert.ToInt32(input[0]), Convert.ToInt32(input[1]), Convert.ToInt32(input[2]), Convert.ToInt32(input[3]), rawDataList);
						samplesList.Add(s);
					}
					return samplesList;
				}
			}
			while (true);
		}

		//DROPPET fordi vi bare indlæser listen af rawData ovenover
		/*public List<BPMesDataGUI_DTO> ReadRawData()
		{
			do
			{
				while (!shallStop)
				{
					List<BPMesDataGUI_DTO> rawDataList = new List<BPMesDataGUI_DTO>();
					List<string> holder = File.ReadAllLines(udpPath).ToList();
					double rawData;
					foreach (string sample in holder)
					{
						string[] input = sample.Split(' ');

						for (int i = 4; i < holder.Count(); i++)
						{
							BPMesDataGUI_DTO rd = new BPMesDataGUI_DTO(Convert.ToDouble(input[i]));

							rawDataList.Add(rd);
						}
					}
					return rawDataList;
				}
			}
			while (true);
		}*/


	}
}
