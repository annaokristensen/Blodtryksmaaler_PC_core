using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer_PC
{
	/// <summary>
	/// Klassen står for kommunikationen med RPi-SW via. UDP. PC'en er Clienten
	/// </summary>
	class UDPClient : IDisposable
	{
		private const int portNo = 11000;

		private UdpClient udpClient = new UdpClient(portNo);


	}
}
