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
        //List<BPMesDataGUI_DTO> bpGUIlist;
        private List<double> listrawData;

        //public Datacontainer()
        //{
        //    //bpGUIlist = new List<BPMesDataGUI_DTO>();
        //}
        public List<double> GetRawDataList()
        {
            return listrawData;
        }
        public void SetRawData(List<double> rawlist)
        {
            listrawData = rawlist;
        }
    }
}
