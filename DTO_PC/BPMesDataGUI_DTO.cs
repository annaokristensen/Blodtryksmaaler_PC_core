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
		public string CurrentSecond { get; set; }

		//Contructoren bruges til til at gemme det indlæste data
		public BPMesDataGUI_DTO(string currentSecond, double pulse, double systoliskValue, double diastoliskValue, double middelValue)
		{
			this.CurrentSecond = currentSecond;
			this.Pulse = pulse;
			this.SystoliskValue = systoliskValue;
			this.DiastoliskValue = diastoliskValue;
			this.MiddelValue = middelValue;
		}
		public BPMesDataGUI_DTO()
		{
			
		}
	}
}
