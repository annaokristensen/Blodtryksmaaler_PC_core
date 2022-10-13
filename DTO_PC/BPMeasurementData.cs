﻿using System;
using System.Collections.Generic;

namespace DTO_PC
{
	/// <summary>
	/// DTO-klasse som overfører data mellem logik- og data-laget for, i datalaget, at kunne gemme data i databasen.
	/// </summary>
	public class BPMeasurementData
	{
		public int MeasurementID { get; set; }
		public string Cpr { get; set; }
		public List<double> RawData { get; set; }
		public List<double> SystoliskValues { get; set; }
		public List<double> DiastoliskValues { get; set; }
		public List<double> PulseValues { get; set; }
		public List<double> MiddelValues { get; set; }
		public DateTime StartDateTime { get; set; }
		public DateTime StopDateTime { get; set; }
		public DateTime AlarmDateTimes { get; set; }

		//En parametiseret constructor, så når vi bruger klassen på logiklaget, så skal vi indskrive alle parametrene for at kunne gemme
		public BPMeasurementData(int measurementID, string cpr, List<double> rawdata, List<double> systoliskValues,
			List<double> diastoliskValues, List<double> pulseValues, List<double> middelValues, DateTime startDateTime,
			DateTime stopDateTime, DateTime alarmDateTimes)
		{
			MeasurementID = measurementID;
			Cpr = cpr;
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