using DTO_PC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer_PC
{
    public interface IMeasurementDataAccess
    {
        BPMesDataGUI_DTO ReadSample();
    }

}
