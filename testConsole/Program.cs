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
using DataLayer_PC;

namespace testConsole
{
    internal class Program
    {
        public static UDPServer s1;
        static void Main(string[] args)
        {
            //Test til at udskrive tekstfilen
            Console.WriteLine("Hello, World!");

            // Tråde
            BlockingCollection<BPMesDataGUI_DTO> samplesList = new BlockingCollection<BPMesDataGUI_DTO>();

            //Test metoder
            IMeasurementControlPC TestreadSampleControlPC = new TestMeasurementControlPC();

            //Ikke Test metoder
            IMeasurementControlPC readSampleControlPC = new MeasurementControlPC();
            UDPServer server = new UDPServer();

            //Test
            Thread TestReadValues = new Thread(TestreadSampleControlPC.ReadValues);
            Thread TestGetValues = new Thread(TestreadSampleControlPC.GetValues);

            //Ikke Test
            Thread readValues = new Thread(readSampleControlPC.ReadValues);

            Thread serverThread = new Thread(server.StartListener);
            Thread testerServerThread = new Thread(server.testUDPServerThread);


            TestReadValues.Start();
           // readValues.Start();
            serverThread.Start();
            testerServerThread.Start();


            TestReadValues.Join();
            //readValues.Join();
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