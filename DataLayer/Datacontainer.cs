using DTO_PC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer_PC
{
    public class Datacontainer
    {
        List<BPMesDataGUI_DTO> bpGUIlist;
        public double rawData;

        public Datacontainer()
        {
            bpGUIlist = new List<BPMesDataGUI_DTO>();
        }
        public double GetRawDataList()
        {
            return rawData;
        }
        public void SetRawData(double value)
        {
            rawData = value;
        }
    }
}
