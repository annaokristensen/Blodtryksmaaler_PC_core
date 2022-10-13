using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DTO_PC;

namespace DataLayer_PC
{
	/// <summary>
	/// Tilgår CPR-databasen for at kunne tjekke om CPR-nummeret er registreret deri
	/// </summary>
	class CPRDataAccess
	{
		//SqlConnection conn;

		public CPRDataAccess()
		{
			//conn = new SqlConnection(@"INDSÆT CONNECTION STRING TIL CPR-DATABASE")
		}

		/// <summary>
		/// IDÉ: Henter en liste af alle CPR-numre i databasen, så der senere kan sammenlignes om
		/// det indtastede CPR-nummer findes i listen.
		/// </summary>
		/// <returns>En liste af strings (cpr-numrene i databasen)</returns>
		public List<string> GetCPRFromDatabase()
		{
			List<string> cprInDBList = new List<string>();

			//string queryString = "select * from BloodPressureData_DB";

			//Indsæt kode som læser fra CPR-databasen og putter det ind i cprInDBList

			return cprInDBList;
		}
	}
}
