using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Runtime.InteropServices.ComTypes;
using DTO_PC;

namespace DataLayer_PC
{
	/// <summary>
	/// Klassen tilgår databasen og gemmer måledata deri
	/// </summary>
	class MeasurementDataAccess
	{
		//SqlConnection conn;

		public MeasurementDataAccess()
		{
			//conn = new SqlConnection(@"INDSÆT CONNECTION STRING TIL MEASUREMENT-DATABASE")
		}

		public void StoreData(BPMeasurementData_DTO bpMesData)
		{
			//Indsæt kode til at gemme i databasen her
		}
	}
}
