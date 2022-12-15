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
        private BlockingCollection<Datacontainer> blocking;
        static void Main(string[] args)
        {
            MeasurementDataAccess m1 = new MeasurementDataAccess();
            
            m1.ReadSample();
        }
    }
}