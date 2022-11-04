using DataLayer_PC;
using LogicLayer_PC;

namespace ConsoleApp1
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            MeasurementControlPC test1 = new MeasurementControlPC();
            test1.GetSamplesList();


            
        }
    }
}