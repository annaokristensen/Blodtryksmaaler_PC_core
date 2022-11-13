using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_PC;
using DataLayer_PC;

namespace LogicLayer_PC
{
	/// <summary>
	/// Sørger for at få data ned til datalaget, så det kan gemmes i databasen. 
	/// </summary>
	public class StopAndSave
	{
		private SaveMeasurement saveMeasurementObj;
		private MeasurementDataAccess mesDataAccess;

		public StopAndSave()
		{
			saveMeasurementObj = new SaveMeasurement();
			mesDataAccess = new MeasurementDataAccess();
		}
		/// <summary>
		/// Metoden som bliver kaldt, når vi trykker på "Stop og Gem" på GUI'en.
		/// Tager DTO-klasse som parameter, så det kan blive gemt i databasen.
		/// Metoden samler data fra GUI og rpi så de kan blive gemt samlet.
		/// </summary>
		/// <param name="bpMesData"></param>
		public void SaveMeasurement(List<BPMesDataGUI_DTO> bpDTOList, string cpr, DateTime startTime, DateTime stopTime, List<DateTime> alarmTimes)
		{ 
			List<double> pulseList = new List<double>();
			List<double> sysList = new List<double>();
			List<double> diaList = new List<double>();
			List<double> middelList = new List<double>();
			
			
			List<double> rawDataFixLater = new List<double>() { 12, 2, 7, 10 };

			foreach (BPMesDataGUI_DTO dtoObj in bpDTOList)
			{
				pulseList.Add(dtoObj.Pulse);
				sysList.Add(dtoObj.SystoliskValue);
				diaList.Add(dtoObj.DiastoliskValue);
				middelList.Add(dtoObj.MiddelValue);
			}

			BPMesDataDB_DTO bpDB_DTO = new BPMesDataDB_DTO(rawDataFixLater, sysList, diaList, pulseList, middelList,
				startTime, stopTime, alarmTimes);

			saveMeasurementObj.SaveBPMeasurement(bpDB_DTO, cpr);
			/*BPMeasurementData_DTO bpmDataDto = new BPMeasurementData_DTO(bpMesData.MeasurementID, bpMesData.RawData,
				bpMesData.SystoliskValues, bpMesData.DiastoliskValues, bpMesData.PulseValues, bpMesData.MiddelValues,
				bpMesData.StartDateTime, bpMesData.StopDateTime, bpMesData.AlarmDateTimes);*/
			//saveMeasurementObj.setMeasurement(bpMesData, cpr); ???? 
		}
		
	}
}
