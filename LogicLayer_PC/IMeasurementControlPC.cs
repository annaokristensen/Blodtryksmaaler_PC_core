using DTO_PC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer_PC
{
    public interface IMeasurementControlPC
    {
        void ReadValues();
        void GetValues(out BPMesDataGUI_DTO kim);
    }
}
