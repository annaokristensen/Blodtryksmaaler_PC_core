/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;*/
using System.Net;
using DataLayer_PC;

namespace LogicLayer_PC
{
	/// <summary>
	/// Sørger for at validere om CPR-nummeret findes i CPR-databasen. Tilgår CPRDataAccess-klassen på datalaget.
	/// </summary>
	class CPRControl
	{
		//CPRDataAccess cprDA = new CPRDataAccess(); (den kan ikke genkende klassen)
		
		//Opretter en 'midlertidig' liste til at 'holde' cpr-numrene som modtages fra datalaget.
		private List<string> midlCPRList = new List<String>();
		private string cprFromGUI;

		/// <summary>
		/// Tjekker om det indtastede CPR-nummer findes i den liste som er hentet fra CPR-databasen
		/// </summary>
		/// <returns>En bool som er 'true' hvis CPR-nummeret findes og 'false' hvis ikke.</returns>
		public bool ValidateCpr()
		{
			//cprDA.GetCPRFromDatabase (kalder metoden for at få cpr-numrene)

			//Skriv kode som henter det indtastede cpr-nummer fra præsentationslaget og gemmer i 'cprFromGUI'.

			if (midlCPRList.Contains(cprFromGUI))
				return true;
			else
				return false;
		}
	}
}
