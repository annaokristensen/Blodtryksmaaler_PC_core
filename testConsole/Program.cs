using System;
using System.Collections.Concurrent;
using DataLayer_PC;
using DTO_PC;
using LogicLayer_PC;
using System.Threading;
using LiveCharts.Wpf;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace testConsole
{
    internal class Program
    {
        public static UDPServer s1;
        static void Main(string[] args)
        {
            //Test til at udskrive tekstfilen
            Console.WriteLine("Hello, World!");
            MeasurementControlPC test1 = new MeasurementControlPC();
            UDPServer server = new UDPServer();
            test1.GetSamplesList();

            // Tråde
            BlockingCollection<BPMesDataGUI_DTO> samplesList = new BlockingCollection<BPMesDataGUI_DTO>();


            MeasurementDataAccess test = new MeasurementDataAccess(samplesList);


            Thread testDataObjThread = new Thread(test1.GetSamplesList);
            Thread testThread = new Thread(test1.TestThread);
            Thread serverThread = new Thread(server.StartListener);
            Thread testerServerThread = new Thread(server.testUDPServerThread);


            testDataObjThread.Start();
            testThread.Start();
            serverThread.Start();
            testerServerThread.Start();


            testDataObjThread.Join();
            testThread.Join();
            serverThread.Join();
            testerServerThread.Join();

            Console.ReadKey();

            //UDP 
            s1 = new UDPServer();
            s1.StartListener();

            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPAddress broadcst = IPAddress.Parse("172.20.10.2");




        }
    }
}