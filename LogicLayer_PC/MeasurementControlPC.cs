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
		public BPCalculator bpCalcObj { get; set; }
		public BPMesDataGUI_DTO BPDTO { get; set; }
		//private BPMesDataGUI_DTO calcValuesDTO;
		public List<double> rawDataListMC = new List<double>();
		//private BPMesDataGUI_DTO bpGUIDTO;

		public MeasurementControlPC(IMeasurementDataAccess ImeasurementDataAccess)
		{
			measurementDataAccessObj = ImeasurementDataAccess;
			bpCalcObj = new BPCalculator();
		}
		public BPMesDataGUI_DTO ReadValues()
		{
			BPMesDataGUI_DTO bpGUIObj = measurementDataAccessObj.ReadSample();
			//BPDTO = measurementDataAccessObj.ReadSample();
			bpCalcObj.saveValues(measurementDataAccessObj.ReadSample());
			

			return bpGUIObj;
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
