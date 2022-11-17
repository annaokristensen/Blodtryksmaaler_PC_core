using System;
using System.Collections.Generic;

namespace DTO_PC
{
	/// <summary>
	/// DTO-klasse som holder det data der skal vises på GUI'en
	/// </summary>
	public class BPMesDataGUI_DTO
	{
		public double Pulse { get; set; }
		public double SystoliskValue { get; set; }
		public double DiastoliskValue { get; set; }
		public double MiddelValue { get; set; }
		//public string CurrentSecond { get; set; }

		public double RawData { get; set; }

		//Contructoren bruges til til at 'holde' de beregnede værdier - dem får vi samme antl af med samme interval
		public BPMesDataGUI_DTO(double pulse, double middelValue, double systoliskValue, double diastoliskValue)
		{
			Pulse = pulse;
			MiddelValue = middelValue;
			SystoliskValue = systoliskValue;
			DiastoliskValue = diastoliskValue;
		}

		//Vi har en seperat constructor til rawdata, fordi vi modtager langt flere værdier end for de beregnede (ovenstående) og derfor ville det være svært
		//at gemme rawdata i samme dto_objekt
		public BPMesDataGUI_DTO(double rawData)
		{
			RawData = rawData;
		}

		public BPMesDataGUI_DTO()
		{
			
		}
	}
}
