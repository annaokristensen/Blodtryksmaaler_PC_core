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

            
            BlockingCollection<Datacontainer> controllers = new BlockingCollection<Datacontainer>();

            TestMeasurementDataAccess producer = new TestMeasurementDataAccess(controllers);
            MeasurementControlPC consumer = new MeasurementControlPC(controllers);

            Thread producerThread = new Thread(producer.ReadSample);
            Thread consumerThread = new Thread(consumer.Run);

            producerThread.Start();
            consumerThread.Start();

            Thread.Sleep(1000);

            List<BPMesDataGUI_DTO> list = consumer.ReadValues();

            //foreach (var value in list)
            //{
            //    foreach (double val in value.RawDataList)
            //    {
            //        Console.WriteLine(Convert.ToString(val));
            //    }
            //}

            producerThread.Join();
            consumerThread.Join();


            Console.ReadKey();




            // Tråde
            //BlockingCollection<BPMesDataGUI_DTO> samplesList = new BlockingCollection<BPMesDataGUI_DTO>();

            ////Test metoder
            //IMeasurementDataAccess testMeasurementDataAccess= new TestMeasurementDataAccess();
            //IMeasurementControlPC TestreadSampleControlPC = new MeasurementControlPC(testMeasurementDataAccess);

            ////Ikke Test metoder
            //IMeasurementDataAccess MeasurementDataAccess = new MeasurementDataAccess();
            //IMeasurementControlPC readSampleControlPC = new MeasurementControlPC(MeasurementDataAccess);
            //UDPServer server = new UDPServer();

            //Test
            //Thread TestReadValues = new Thread(TestreadSampleControlPC.ReadValues());
       //     Thread TestGetValues = new Thread(TestreadSampleControlPC.GetValues);

            ////Ikke Test
            //Thread readValues = new Thread(readSampleControlPC.ReadValues);
            ////Thread GetValues = new Thread(readSampleControlPC.GetValues);

            //Thread serverThread = new Thread(server.StartListener);
            //Thread testerServerThread = new Thread(server.testUDPServerThread);


            //TestReadValues.Start();
           // readValues.Start();
           // serverThread.Start();
            //testerServerThread.Start();


           // TestReadValues.Join();
            //readValues.Join();
           // serverThread.Join();
           // testerServerThread.Join();

            //Console.ReadKey();

            //////UDP 
            ////s1 = new UDPServer();
            ////s1.StartListener();

            ////Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            ////IPAddress broadcst = IPAddress.Parse("172.20.10.2");




        }
    }
}