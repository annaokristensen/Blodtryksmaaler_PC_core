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
		public List<double> RawDataList { get; set; }

		//Contructoren bruges til til at 'holde' de beregnede værdier - dem får vi samme antl af med samme interval
		

		public BPMesDataGUI_DTO(List<double> rawDataList)
		{
			RawDataList = rawDataList;
		}

		public BPMesDataGUI_DTO()
		{
			
		}
	}
}
