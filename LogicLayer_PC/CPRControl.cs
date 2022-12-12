using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using DataLayer_PC;

namespace LogicLayer_PC
{
	/// <summary>
	/// Sørger for at validere om CPR-nummeret findes i CPR-databasen. Tilgår CPRDataAccess-klassen på datalaget.
	/// </summary>
	public class CPRControl
	{
		CPRDataAccess cprDA = new CPRDataAccess();
		
		//Opretter en 'midlertidig' liste til at 'holde' cpr-numrene som modtages fra datalaget.
		private List<string> midlCPRList = new List<string>();

		/// <summary>
		/// Tjekker om det indtastede CPR-nummer findes i den liste som er hentet fra CPR-databasen
		/// </summary>
		/// <returns>En bool som er 'true', hvis CPR-nummeret findes og 'false' hvis ikke.</returns>
		public bool ValidateCpr(string cprParameter)
		{
			//Gemmer CPR-numrene fra databasen i en liste
			foreach (string cpr in cprDA.GetCPRFromDatabase())
			{
				midlCPRList.Add(cpr);
			}

			//Metoden ValidateCpr er blevet kaldt fra CPR_Window med det indtastede CPR-nummer som parameter.
			//Derved kan vi nu tjekke om det indtastede CPR-nummer findes i listen (som består af cpr-numrene i databasen)
			if (midlCPRList.Contains(cprParameter))
				return true;
			else
				return false;
		}
	}
}
