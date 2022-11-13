using System;
using System.Collections.Generic;
using System.Text;

namespace DTO_PC
{
	/// <summary>
	/// DTO-klasse som holder det data som skal gemmes i databasen
	/// </summary>
	public class BPMesDataDB_DTO
	{
		public List<double> RawData { get; set; }
		public List<double> SystoliskValues { get; set; }
		public List<double> DiastoliskValues { get; set; }
		public List<double> PulseValues { get; set; }
		public List<double> MiddelValues { get; set; }
		public DateTime StartDateTime { get; set; }
		public DateTime StopDateTime { get; set; }
		public List<DateTime> AlarmDateTimes { get; set; }

		//En parametiseret constructor, så når vi bruger klassen på logiklaget, så skal vi indskrive alle parametrene for at kunne gemme
		public BPMesDataDB_DTO(List<double> rawdata, List<double> systoliskValues,
			List<double> diastoliskValues, List<double> pulseValues, List<double> middelValues, DateTime startDateTime,
			DateTime stopDateTime, List<DateTime> alarmDateTimes)
		{
			RawData = rawdata;
			SystoliskValues = systoliskValues;
			DiastoliskValues = diastoliskValues;
			PulseValues = pulseValues;
			MiddelValues = middelValues;
			StartDateTime = startDateTime;
			StopDateTime = stopDateTime;
			AlarmDateTimes = alarmDateTimes;
		}
	}
}
