using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer_PC;
using DTO_PC;

namespace LogicLayer_PC
{
	public class MeasurementControlPC : IMeasurementControlPC
	{

		private IMeasurementDataAccess measurementDataAccessObj;
		private BPCalculator bpCalcObj;
		private BPMesDataGUI_DTO BPDTO;
		//private BPMesDataGUI_DTO calcValuesDTO;
		private List<double> rawDataListMC = new List<double>();

		public MeasurementControlPC(IMeasurementDataAccess ImeasurementDataAccess)
		{
			measurementDataAccessObj = ImeasurementDataAccess;
			bpCalcObj = new BPCalculator();
		}
		public void ReadValues()
		{
			BPDTO = measurementDataAccessObj.ReadSample();
			bpCalcObj.saveValues(BPDTO);			
		}
		//public BPMesDataGUI_DTO GetValues()
		//{
  //          return BPDTO;
  //      }
        public void GetValues(out BPMesDataGUI_DTO kim) //Tjek op på out 
        {
			kim = BPDTO;
        }
    }
}
