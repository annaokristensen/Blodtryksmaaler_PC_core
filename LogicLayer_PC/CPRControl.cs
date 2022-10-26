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
		CPRDataAccess cprDA = new CPRDataAccess(); //(den kan ikke genkende klassen)
		
		//Opretter en 'midlertidig' liste til at 'holde' cpr-numrene som modtages fra datalaget.
		private List<string> midlCPRList = new List<string>();
		private string cprFromGUI;

		/// <summary>
		/// Tjekker om det indtastede CPR-nummer findes i den liste som er hentet fra CPR-databasen
		/// </summary>
		/// <returns>En bool som er 'true', hvis CPR-nummeret findes og 'false' hvis ikke.</returns>
		public bool ValidateCpr(string cprParameter)
		{
			foreach (string cpr in cprDA.GetCPRFromDatabase())
			{
				midlCPRList.Add(cpr);
			}

			//Det indtastede CPR-nummer skal hentes fra GUI. Men det kræver at logiklaget kender præsentationslaget og kan kalde metoden??

			//INDKOMMENTÉR FØLGENDE:
			//CPR_Window cprWindowObj = new CPR_Window();
			//cprFromGUI = cprWindowObj.GetEnteredCpr();
			
			if (midlCPRList.Contains(cprFromGUI))
				return true;
			else
				return false;
		}
	}
}
