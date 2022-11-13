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
		//SqlConnection conn;
		private readonly BlockingCollection<BPMesDataGUI_DTO> samplesList = new BlockingCollection<BPMesDataGUI_DTO>();
		//public string Path = "samplestest.txt";
		public string Path = @"testmedtal.txt";
		//@"Physionet.txt";
		public double second { get; set; }
		public double sampleValue { get; set; }
		public List<MeasurementDataAccess> LoadedSampleValue;
		private bool shallStop = false;
       
        public MeasurementDataAccess()
		{
			
		}
		public MeasurementDataAccess(double second, double sampleValue)
		{
			//conn = new SqlConnection(@"INDSÆT CONNECTION STRING TIL MEASUREMENT-DATABASE")
			this.second = second;
			this.sampleValue = sampleValue;
		}
		public MeasurementDataAccess(BlockingCollection<BPMesDataGUI_DTO> samplesList)
		{
			this.samplesList = samplesList;
		}

		//Vi skal have delt metoden ReadSamples op, så klassen MeasurementDataAccess kun har ansvaret for at indlæse data. 
		//Dataen skal gemmes/opbevares i DTO klassen, som alle klasser kan tilgå.
		public void StoreData(BPMesDataGUI_DTO bpMesData)
        {
            //Indsæt kode til at gemme i databasen her
        }
		public List<BPMesDataGUI_DTO> ReadSample()
		{
			do
			{
				while(!shallStop) //Skal kører så længe shallstop er true (skal ændres til start/stop knap på GUI)
				{
					List<BPMesDataGUI_DTO> samplesList = new List<BPMesDataGUI_DTO>();
                    List<string> holder = File.ReadAllLines(Path).ToList();
                    foreach (string sample in holder)
                    {
                        string[] input = sample.Split(' ');
						BPMesDataGUI_DTO s = new BPMesDataGUI_DTO(input[0], Convert.ToInt32(input[1]), Convert.ToInt32(input[2]), Convert.ToInt32(input[3]), Convert.ToInt32(input[4])); //gemmer intput 1, 2, 3.. i DTO
                        samplesList.Add(s);
                    }
                    return samplesList;
                }
            }
			while(true);
		}


	}
}
