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
        private readonly BlockingCollection<double> RawDataBlocking;
        public BPMesDataGUI_DTO rawDataDTOBC;

        public Consumer()
        {
            RawDataBlocking = new BlockingCollection<double>();
        }
        public BPMesDataGUI_DTO TakeFromBC()
        {
            while (!RawDataBlocking.IsCompleted) //Tjekker om der er completet i ReadSample
            {
                try
                {
                    rawDataDTOBC.RawDataList.Add(RawDataBlocking.Take());
                    return rawDataDTOBC;
                }
                catch (InvalidOperationException)
                {
                    return null;
                }
            }
            return null;
        }
    }
}
