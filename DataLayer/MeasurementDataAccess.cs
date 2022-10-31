﻿using System;
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
		public readonly BlockingCollection<BPMeasurementData_DTO> samplesList = new BlockingCollection<BPMeasurementData_DTO>();
		//public string Path = "samplestest.txt";
		public string Path = "C:\\Users\\rikke\\OneDrive\\Dokumenter\\3. semester\\Semesterprojekt\\BlodtryksmålerPC_\\DataLayer\\bin\\Debug\\net5.0\\testmedtal.txt";
		public double second { get; set; }
		public double sampleValue { get; set; }
       
        public MeasurementDataAccess()
		{
			
		}
		public MeasurementDataAccess(double second, double sampleValue)
		{
			//conn = new SqlConnection(@"INDSÆT CONNECTION STRING TIL MEASUREMENT-DATABASE")
			this.second = second;
			this.sampleValue = sampleValue;
		}
		public MeasurementDataAccess(BlockingCollection<BPMeasurementData_DTO> samplesList)
		{
			this.samplesList = samplesList;
		}

		//Vi skal have delt metoden ReadSamples op, så klassen MeasurementDataAccess kun har ansvaret for at indlæse data. 
		//Dataen skal gemmes/opbevares i DTO klassen, som alle klasser kan tilgå. 
		//public BlockingCollection<BPMeasurementData_DTO> ReadSample()
		//{
		//	List<BPMeasurementData_DTO> samplesList = new List<BPMeasurementData_DTO>();
		//	List<string> holder = File.ReadAllLines(Path).ToList();
		//	foreach (string sample in holder)
		//	{
		//		string[] input = sample.Split(' ');
		//		BPMeasurementData_DTO s = new BPMeasurementData_DTO(Convert.ToInt32(input[0]), Convert.ToInt32(input[1]));
		//		samplesList.Add(s);
		//	}
		//	return samplesList;
		//}
		public void StoreData(BPMeasurementData_DTO bpMesData)
        {
            //Indsæt kode til at gemme i databasen her
        }
		public BlockingCollection<BPMeasurementData_DTO> ReadSample()
		{
            BlockingCollection<BPMeasurementData_DTO> samplesList = new BlockingCollection<BPMeasurementData_DTO>();
			List<string> holder = File.ReadAllLines(Path).ToList();
			foreach (string sample in holder)
			{
				string[] input = sample.Split(' ');
				BPMeasurementData_DTO s = new BPMeasurementData_DTO(Convert.ToInt32(input[0]), Convert.ToInt32(input[1]));
				samplesList.Add(s);
			}
			return samplesList;
		}
	}
}
