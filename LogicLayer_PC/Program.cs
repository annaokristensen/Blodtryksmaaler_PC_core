using DataLayer_PC;
using LogicLayer_PC;

namespace ConsoleApp1
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            TestMeasurementControlPC test1 = new TestMeasurementControlPC();
            test1.GetSamplesList();


            
        }
    }
}