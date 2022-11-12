using System;
using System.Collections.Generic;

namespace DTO_PC
{
	/// <summary>
	/// DTO-klasse som overfører data mellem logik- og data-laget for, i datalaget, at kunne gemme data i databasen.
	/// </summary>
	public class BPMeasurementData_DTO
	{
		public double Pulse { get; set; }
		public double SystoliskValue { get; set; }
		public double DiastoliskValue { get; set; }
		public double MiddelValue { get; set; }
		public int MeasurementID { get; set; }
		public string dateTime { get; set; }

		//Cpr er udkommenteret, fordi den modsat de andre værdier kommer fra GUI'en (tror jeg - Julie)
		//public string Cpr { get; set; }
		public List<double> RawData { get; set; }
		public List<double> SystoliskValues { get; set; }
		public List<double> DiastoliskValues { get; set; }
		public List<double> PulseValues { get; set; }
		public List<double> MiddelValues { get; set; }
		public DateTime StartDateTime { get; set; }
		public DateTime StopDateTime { get; set; }
		public DateTime AlarmDateTimes { get; set; }

        //En parametiseret constructor, så når vi bruger klassen på logiklaget, så skal vi indskrive alle parametrene for at kunne gemme
        public BPMeasurementData_DTO(int measurementID, /*string cpr*/ List<double> rawdata, List<double> systoliskValues,
			List<double> diastoliskValues, List<double> pulseValues, List<double> middelValues, DateTime startDateTime,
			DateTime stopDateTime, DateTime alarmDateTimes)
		{
			MeasurementID = measurementID;
			//Cpr = cpr;
			RawData = rawdata;
			SystoliskValues = systoliskValues;
			DiastoliskValues = diastoliskValues;
			PulseValues = pulseValues;
			MiddelValues = middelValues;
			StartDateTime = startDateTime;
			StopDateTime = stopDateTime;
			AlarmDateTimes = alarmDateTimes;
		}

		//Contructoren bruges til til at gemme det indlæste data
		public BPMeasurementData_DTO(double pulse, double systoliskValue, double diastoliskValue, double middelValue, string dateTime)
		{
			this.Pulse = pulse;
			this.SystoliskValue = systoliskValue;
			this.DiastoliskValue = diastoliskValue;
			this.MiddelValue = middelValue;
			this.dateTime = dateTime;
		}
		public BPMeasurementData_DTO()
		{
			
		}
	}
}
