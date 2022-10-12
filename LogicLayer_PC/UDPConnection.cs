using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using DataLayer_PC;

namespace LogicLayer_PC
{
	class UDPConnection
	{
		private UDPClient udpClient = new UDPClient();
		
		public void StartConnecting(string message)
		{
			udpClient.SendBroadcast(message);
		}
	}
}
