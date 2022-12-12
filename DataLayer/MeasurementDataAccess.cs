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
using System.Threading;

namespace DataLayer_PC
{
	/// <summary>
	/// Klassen tilgår databasen og gemmer måledata deri
	/// </summary>
	public class MeasurementDataAccess : IMeasurementDataAccess
	{
		
        private readonly BlockingCollection<Datacontainer> _dataQueue;
		public BPMesDataGUI_DTO rawDataDTOBC;

        public double Second { get; set; }
		public double SampleValue { get; set; }
		public List<MeasurementDataAccess> LoadedSampleValue;
		private bool shallStop = false;
		public Server udpServerObj;

		public MeasurementDataAccess(double second, double sampleValue)
		{
			//conn = new SqlConnection(@"INDSÆT CONNECTION STRING TIL MEASUREMENT-DATABASE")
			Second = second;
			SampleValue = sampleValue;
		}
		public MeasurementDataAccess() { }
		public MeasurementDataAccess(BlockingCollection<Datacontainer> RawDataBlocking, Server udpServer)
		{
			_dataQueue = RawDataBlocking;
            udpServerObj = udpServer;
        }

		/// <summary>
		/// Læser fra udpPath og gemmer rawData som en List af doubles i DTO-klassen
		/// </summary>
		/// <returns>BPMesDataGUI_DTO List af double rawData</returns>
		public void ReadSample()  
		{
			//Sætter udpPath til at være den string som udpServeren returnerer. Det er deri at data fra rpi står
			string udpPath = udpServerObj.GetBroadcast();
			do
			{
				while (!shallStop)
				{
					List<double> rawDataList = new List<double>();
					List<string> holder = new List<string>();
					holder.Add(udpPath);
					

					foreach (string sample in holder)
					{
						if (sample == null) { continue; } //continue = starter forfra i foreach
						string[] input = sample.Split("\n");
						foreach (var i in sample)
						{
							rawDataList.Add(Convert.ToDouble(i));
						}
					}
                    Datacontainer reading = new Datacontainer();
					reading.SetRawData(rawDataList);
					_dataQueue.Add(reading);

					Thread.Sleep(1000);
				}
			}
			while (true);
		}
		public BPMesDataGUI_DTO TakeFromBC()
		{
			//rawDataDTOBC.RawDataList = _dataQueue.();
			return rawDataDTOBC;
		}
	}
}
