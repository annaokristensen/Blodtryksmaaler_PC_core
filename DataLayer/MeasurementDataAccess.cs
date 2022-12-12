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
using System.Net.Sockets;
using System.Net;

namespace DataLayer_PC
{
	/// <summary>
	/// Klassen tilgår databasen og gemmer måledata deri
	/// </summary>
	public class MeasurementDataAccess : IMeasurementDataAccess
	{
        public const int listenPort = 12000;
        //public float svar;
        public string DataFromRPi;

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
		public MeasurementDataAccess(BlockingCollection<Datacontainer> RawDataBlocking/*, Server udpServer*/)
		{
			_dataQueue = RawDataBlocking;
            //udpServerObj = udpServer;
        }

		/// <summary>
		/// Læser fra udpPath og gemmer rawData som en List af doubles i DTO-klassen
		/// </summary>
		/// <returns>BPMesDataGUI_DTO List af double rawData</returns>
		public void ReadSample()  
		{
            UdpClient listener = new UdpClient(listenPort);
            //Sætter udpPath til at være den string som udpServeren returnerer. Det er deri at data fra rpi står
            //string udpPath = udpServerObj.GetBroadcast();
            do
			{
				while (!shallStop)
				{
                    
                    IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);

                    byte[] bytes = listener.Receive(ref groupEP); //Det der modtages lægges ind i bytes

                    string tekst = ($"{Encoding.ASCII.GetString(bytes, 0, bytes.Length)}");


                    List<double> rawDataList = new List<double>();
					List<string> holder = new List<string>();

					holder.Add(tekst);
					string[] hej = tekst.Split(' ');
					foreach (var item in hej)
					{
                        try
                        {
                            rawDataList.Add(Convert.ToDouble(item));
                        }
                        catch
                        {
                            continue;
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
			return rawDataDTOBC;
		}
	}
}
