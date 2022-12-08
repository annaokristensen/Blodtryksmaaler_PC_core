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
        private ICalbrationFileAcess calbrationFileAcess;
		public BPCalculator bpCalcObj { get; set; }
		public BPMesDataGUI_DTO BPDTO { get; set; }
		//private BPMesDataGUI_DTO calcValuesDTO;
		public List<double> rawDataListMC = new List<double>();
		List<BPMesDataGUI_DTO> bpGUIlist;
		private ZeropointControlPC zeropointControl;
        //private BPMesDataGUI_DTO bpGUIDTO;

        public MeasurementControlPC(IMeasurementDataAccess ImeasurementDataAccess)
		{
			measurementDataAccessObj = ImeasurementDataAccess;
			bpCalcObj = new BPCalculator();
            bpGUIlist = new List<BPMesDataGUI_DTO>();
            calbrationFileAcess = new CalibrationFileAcess();
            zeropointControl = new ZeropointControlPC();
        }
		public List<BPMesDataGUI_DTO> ReadValues()
        {
			List<double> dataList = new List<double>();

            measurementDataAccessObj.ReadSample(); 
  
            BPMesDataGUI_DTO dto = measurementDataAccessObj.TakeFromBC();
            rawDataListMC = dto.RawDataList;

            foreach (var value in dto.RawDataList)
            {
				dataList.Add((value - zeropointControl.Zeropoint) * calbrationFileAcess.ReadValue());
            }

            dto.RawDataList = dataList;
			
			//BPDTO = measurementDataAccessObj.ReadSample();
			bpCalcObj.saveValues(bpGUIlist[bpGUIlist.Count-1]);

			return bpGUIlist;
		}
        //public List<BPMesDataGUI_DTO> ReadValues()
        //{
        //    List<double> dataList = new List<double>();

        //    BPMesDataGUI_DTO dto = measurementDataAccessObj.ReadSample();

        //    foreach (var value in dto.RawDataList)
        //    {
        //        dataList.Add((value - zeropointControl.Zeropoint) * calbrationFileAcess.ReadValue());
        //    }

        //    dto.RawDataList = dataList;

        //    //BPDTO = measurementDataAccessObj.ReadSample();
        //    bpCalcObj.saveValues(bpGUIlist[bpGUIlist.Count - 1]);

        //    return bpGUIlist;
        //}
    }
}
