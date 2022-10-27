using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Runtime.InteropServices.ComTypes;
using DTO_PC;
using System.IO;

namespace DataLayer_PC
{
	/// <summary>
	/// Klassen tilgår databasen og gemmer måledata deri
	/// </summary>
	public class MeasurementDataAccess
	{
		//SqlConnection conn;
		public List<MeasurementDataAccess>samplesList = new List<MeasurementDataAccess>();
		public string Path = "samplestest.txt";
		public double second { get; set; }
		public double sampleValue { get; set; }

		public MeasurementDataAccess(double second, double sampleValue)
		{
			//conn = new SqlConnection(@"INDSÆT CONNECTION STRING TIL MEASUREMENT-DATABASE")
			this.second = second;
			this.sampleValue = sampleValue;
		}

		public void StoreData(BPMeasurementData_DTO bpMesData)
		{
			//Indsæt kode til at gemme i databasen her
		}
		//public void GetDataFromPhysionet()
		//{
		//	string inputRecord;
		//	string[] inputFields;

		//	///
		//	//array = File.ReadAllLines()
		//	///
			
		//	//Opretter filestream og format objekt 
		//	FileStream input = new FileStream("samplestest.txt", FileMode.Open, FileAccess.Read);
		//	StreamReader fileReader = new StreamReader(input);

		//	//Så længe der er data i filen lægges det ind i listen sampleList
		//	while((inputRecord = fileReader.ReadLine()) != null)
		//	{
		//		//Splitter dataen i tekstfilen op i kolonner 
		//		inputFields = inputRecord.
		//	}
		
		//}
		public List<MeasurementDataAccess> ReadSamples()
		{
			List<MeasurementDataAccess> samplesList = new List<MeasurementDataAccess>();
			List<string> holder = File.ReadAllLines(Path).ToList();
			foreach (string sample in holder)
			{
				string[] input = sample.Split(' ');
				MeasurementDataAccess s = new MeasurementDataAccess(Convert.ToInt32(input[0]), Convert.ToInt32(input[1]));
				samplesList.Add(s);
                
            }
			return samplesList;
			



        }
    }
}
