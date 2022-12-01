using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer_PC;
using DTO_PC;

namespace LogicLayer_PC
{
	public class MeasurementControlPC
	{
		private MeasurementDataAccess measurementDataAccessObj;
		private BPCalculator bpCalcObj;
		private BPMesDataGUI_DTO BPDTO;
		//private BPMesDataGUI_DTO calcValuesDTO;
		private List<double> rawDataListMC = new List<double>();


		public MeasurementControlPC()
		{
			measurementDataAccessObj = new MeasurementDataAccess();
			bpCalcObj = new BPCalculator();
		}

		public BPMesDataGUI_DTO GetBPValues()
		{
			BPDTO = measurementDataAccessObj.ReadRawData();
			bpCalcObj.saveValues(BPDTO);


			return BPDTO;
		}
		

	}
}
