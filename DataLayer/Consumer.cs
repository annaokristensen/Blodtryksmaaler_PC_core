using DTO_PC;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer_PC
{
    public class Consumer
    {
        private readonly BlockingCollection<Datacontainer> RawDataBlocking;
        public BPMesDataGUI_DTO rawDataDTOBC;

        public Consumer(BlockingCollection<Datacontainer> RawDataBlocking) 
        {
            this.RawDataBlocking = RawDataBlocking;
        }
        public void Run()
        {
            while (!RawDataBlocking.IsCompleted)
            {
                try
                {
                    var contanier = RawDataBlocking.Take();
                    List<double> RawData = contanier.GetRawDataList();
                }
                catch (InvalidOperationException)
                {
                    Console.WriteLine("Catch start");
                }
            }
            System.Console.WriteLine("No more data excepted");
        }
    }
}
