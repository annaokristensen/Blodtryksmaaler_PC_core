using System;
using System.Collections.Concurrent;
using DataLayer_PC;
using DTO_PC;
using LogicLayer_PC;
using System.Threading;

namespace testConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Test til at udskrive tekstfilen
            Console.WriteLine("Hello, World!");
            MeasurementControlPC test1 = new MeasurementControlPC();
            test1.GetSamplesList();

            // Tråde
            BlockingCollection<BPMeasurementData_DTO> samplesList = new BlockingCollection<BPMeasurementData_DTO>();


            MeasurementDataAccess test = new MeasurementDataAccess(samplesList);


            Thread testDataObjThread = new Thread(test1.GetSamplesList);
            Thread testThread = new Thread(test1.TestThread);


            testDataObjThread.Start();
            testThread.Start();

            testDataObjThread.Join();
            testThread.Join();

            Console.ReadKey();


        }
    }
}