using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer_PC
{
	/// <summary>
	/// Klassen står for kommunikationen med RPi-SW via. UDP. PC'en er Clienten
	/// </summary>
	public class UDPClient : IDisposable
	{
		private const int portNo = 11000;
		private UdpClient udpClient = new UdpClient(portNo);

		//Tilføj evt. også portNo som parameter til metoden
		public void SendBroadcast(string message)
		{
			byte[] packet = Encoding.ASCII.GetBytes("Hallo?");

			try
			{
				udpClient.Send(packet, packet.Length, IPAddress.Broadcast.ToString(), portNo);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}

		//Tilføj evt. portNo som parameter til metoden
		public void Receive()
		{
			UdpClient client = null;

			try
			{
				client = new UdpClient(portNo);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

			IPEndPoint server = new IPEndPoint(IPAddress.Any, portNo);


			while (true)
			{
				try
				{
					byte[] packet = udpClient.Receive(ref server);
					Console.WriteLine("{0}, {1}", server, Encoding.ASCII.GetString(packet));
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
			}
		}

    }
}
