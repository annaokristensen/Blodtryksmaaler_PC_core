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
        private readonly BlockingCollection<Datacontainer> _dataQueue;
        public double Second { get; set; }
		public double SampleValue { get; set; }
		private bool shallStop = false;

		public MeasurementDataAccess(double second, double sampleValue)
		{
			Second = second;
			SampleValue = sampleValue;
		}
		public MeasurementDataAccess() { }
		public MeasurementDataAccess(BlockingCollection<Datacontainer> RawDataBlocking)
		{
			_dataQueue = RawDataBlocking;
        }

		/// <summary>
		/// Læser fra udpPath og gemmer rawData som en List af doubles i rawDataList. Denne liste gemmes i BlockingCollection
		/// </summary>
		/// <returns>BPMesDataGUI_DTO List af double rawData</returns>
		public void ReadSample()  
		{
            UdpClient listener = new UdpClient(listenPort);
            do
			{
				while (!shallStop)
				{            
                    IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);

                    byte[] bytes = listener.Receive(ref groupEP);

                    string tekst = ($"{Encoding.ASCII.GetString(bytes, 0, bytes.Length)}");

                    List<double> rawDataList = new List<double>();
					List<string> holder = new List<string>();

					holder.Add(tekst);
					string[] input = tekst.Split(' ');
                    
                    foreach (var item in input)
					{
                        try
                        {
							Console.WriteLine(item);
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
				}
			}
			while (true);
		}
		//GetOneSecond bruges til nulpunktjustering og kalibrering. Metoden er ikke i en tråd, så den læser kun en værdi for 1 sekund.
		public List<double> GetOneSecond()
		{
            UdpClient listener = new UdpClient(listenPort);

            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);

            byte[] bytes = listener.Receive(ref groupEP); 

            string tekst = ($"{Encoding.ASCII.GetString(bytes, 0, bytes.Length)}");

            List<double> rawDataList = new List<double>();
            List<string> holder = new List<string>();

            holder.Add(tekst);
            string[] input = tekst.Split(' ');
            foreach (var item in input)
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

			return rawDataList;
        }

	}
}
