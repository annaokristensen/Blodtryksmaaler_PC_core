using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DTO_PC;
using System.Collections.Concurrent;

namespace DataLayer_PC
{
    public class Server
    {
        public const int listenPort = 12000;
        public string DataFromRPi;

        public void StartListener()   //Lytter efter porten på PC
        {
	        UdpClient listener = new UdpClient(listenPort);
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);    //læser porten fra IPadressen

            try
            {
                while (true)
                {
                    Console.WriteLine("Waiting for broadcast"); //venter på at PC sender noget
                    byte[] bytes = listener.Receive(ref groupEP); //Det der modtages lægges ind i bytes

                    Console.WriteLine($"Received broadcast from {groupEP} :");
                    Console.WriteLine($"{Encoding.ASCII.GetString(bytes, 0, bytes.Length)}");

                    DataFromRPi =  $"{Encoding.ASCII.GetString(bytes, 0, bytes.Length)}"; //tager string og laver den om til Ascii værdi

                }
            }
            catch (SocketException e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                listener.Close();
            }
        }
        public string GetBroadcast()
        {
            return DataFromRPi;
        }
    }
}
