using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_PC;
using System.Data.SqlClient;

namespace DataLayer_PC
{
	/// <summary>
	/// Tilgår CPR-databasen for at kunne tjekke om CPR-nummeret er registreret deri
	/// </summary>
	
	public class CPRDataAccess
	{
		private SqlConnection conn;
		
		public CPRDataAccess()
		{

			//conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TestDBProjekt3;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
			//JULIES:
			conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Bloodpressure;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

		}

		/// <summary>
		/// Henter alle cpr-numre fra databasen og gemmer den i en liste af strings
		/// </summary>
		/// <returns>En liste af strings (cpr-numrene i databasen)</returns>
		public List<string> GetCPRFromDatabase()
		{
			List<string> cprInDBList = new List<string>();

			string queryString = "select * from CPR_table";

			SqlCommand sqlcommand = new SqlCommand(queryString, conn);

			conn.Open();

			SqlDataReader sdr = sqlcommand.ExecuteReader();

			//Læser fra cpr-kolonnen 
			while (sdr.Read())
			{
					cprInDBList.Add(Convert.ToString(sdr["cpr"])); 
			}
			conn.Close();

			return cprInDBList;
		}
	}
}
