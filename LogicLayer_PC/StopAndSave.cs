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
		/// Tager DTO_DB som parameter, så listen af DTO-objekter vi har kan blive gemt i databasen
		/// Metoden samler data fra DTO_DB og information fra GUI (fx cpr) i ét samlet objekt
		/// </summary>
		/// <param name="bpMesData"></param>
		public void SaveMeasurement(List<BPMesDataGUI_DTO> bpDTOList, string cpr, DateTime startTime, DateTime stopTime, List<DateTime> alarmTimes)
		{ 
			//Opretter en liste til hver af de forskellige værdier, så værdierne kan blive gemt deri i databasen.
			List<double> pulseList = new List<double>();
			List<double> sysList = new List<double>();
			List<double> diaList = new List<double>();
			List<double> middelList = new List<double>();
			
			//En midlertidig liste af rådata, fordi vi lige nu ikke har noget rådata i tekstfilen.
			List<double> rawDataFixLater = new List<double>() { 12, 2, 7, 10 };

			//Kører dtoGUI objektet igennem og tilføjer de forskellige værdier til hver sin liste, som de kan blive gemt således i databasen
			foreach (BPMesDataGUI_DTO dtoGUIobj in bpDTOList)
			{
				pulseList.Add(dtoGUIobj.Pulse);
				sysList.Add(dtoGUIobj.SystoliskValue);
				diaList.Add(dtoGUIobj.DiastoliskValue);
				middelList.Add(dtoGUIobj.MiddelValue);
			}

			//Opretter et objekt af database-DTO'en og samler informationerne heri. De første kommer fra listerne som er kreeret ovenover og
			//alarmTiems kommer ind som parameter når man kalder SaveMeasurement() fra mainWindow
			BPMesDataDB_DTO bpDB_DTO = new BPMesDataDB_DTO(rawDataFixLater, sysList, diaList, pulseList, middelList,
				startTime, stopTime, alarmTimes);

			//Kalder metoden SaveBPMeasurement() som ligger på datalaget. Giver den ovenstående objekt og cpr-nummeret med som parameter
			saveMeasurementObj.SaveBpMeasurement(bpDB_DTO, cpr);
		}
		
	}
}
