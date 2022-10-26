using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_PC;

namespace LogicLayer_PC
{
	/// <summary>
	/// Sørger for at få data ned til datalaget, så det kan gemmes i databasen. 
	/// </summary>
	class StopAndSave
	{
		/// <summary>
		/// Metoden som bliver kaldt, når vi trykker på "Stop og Gem" på GUI'en.
		/// Tager DTO-klasse som parameter, så det kan blive gemt i databasen.
		/// Metoden samler data fra GUI og rpi så de kan blive gemt samlet.
		/// </summary>
		/// <param name="bpMesData"></param>
		public void SaveMeasurement(BPMeasurementData_DTO bpMesData, string cpr)
		{
			BPMeasurementData_DTO bpmDataDto = new BPMeasurementData_DTO(bpMesData.MeasurementID, bpMesData.RawData,
				bpMesData.SystoliskValues, bpMesData.DiastoliskValues, bpMesData.PulseValues, bpMesData.MiddelValues, 
				bpMesData.StartDateTime, bpMesData.StopDateTime, bpMesData.AlarmDateTimes);

		}
	}
}
