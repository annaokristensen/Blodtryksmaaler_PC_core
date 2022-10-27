using System;
using LogicLayer_PC;

namespace testConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            MeasurementControlPC test1 = new MeasurementControlPC();
            test1.getSamplesList();
            

        }
    }
}