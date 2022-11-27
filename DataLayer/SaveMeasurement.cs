using DTO_PC;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer_PC
{
    public class SaveMeasurement
    {
	    private SqlConnection conn;
	    private SqlCommand command;

        public SaveMeasurement()
        {
			conn = new SqlConnection(@"Data Source=LAPTOP-JKBR8I3G\SQLEXPRESS;Initial Catalog=BloodPressureData_DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

		/// <summary>
		/// Metoden kaldes fra logiklaget. Indeholder det sql-kode som skal 'sendes' til databasen så data kan blive gemt
		/// </summary>
		/// <param name="bpDB_DTO"></param>
		/// <param name="cpr"></param>
		public void SaveBpMeasurement(BPMesDataDB_DTO bpDB_DTO, string cpr)
		{
			conn.Open();
			//sql-kode som sørger for at gemme i databasen. Det er delt op på flere linjer for syns skyld
			string queryString = "INSERT INTO dbo.MeasurementData_DB (" +
			                     "cpr" +
			                     ", startDateTime" +
			                     ", stopDateTime" +
			                     ", rawData" +
			                     ", systoliskValues" +
			                     ", diastoliskValues" +
			                     ", pulseValues" +
			                     ", middelValues" +
			                     ", alarmDateTimes)" +
			                     " VALUES ('" +
			                     cpr + "'" +
			                     ", '" + bpDB_DTO.StartDateTime.ToString("yyyMMdd HH:mm:ss") + "'" +
			                     ", '" + bpDB_DTO.StopDateTime.ToString("yyyMMdd HH:mm:ss") + "'" +
			                     ", " + "@rawData" +
			                     ", " + "@sysValues" +
			                     ", " + "@diaValues" +
			                     ", " + "@pulseValues" +
			                     ", " + "@middelValues" +
			                     ", " + "@alarmTimes)";

			command = new SqlCommand(queryString, conn);

			command.Parameters.AddWithValue("@rawdata", bpDB_DTO.RawData.ToArray().SelectMany(value => BitConverter.GetBytes(value)).ToArray());
			command.Parameters.AddWithValue("@sysValues", bpDB_DTO.SystoliskValues.ToArray().SelectMany(value => BitConverter.GetBytes(value)).ToArray());
			command.Parameters.AddWithValue("@diaValues", bpDB_DTO.DiastoliskValues.ToArray().SelectMany(value => BitConverter.GetBytes(value)).ToArray());
			command.Parameters.AddWithValue("@pulseValues", bpDB_DTO.PulseValues.ToArray().SelectMany(value => BitConverter.GetBytes(value)).ToArray());
			command.Parameters.AddWithValue("@middelValues", bpDB_DTO.MiddelValues.ToArray().SelectMany(value => BitConverter.GetBytes(value)).ToArray());
			command.Parameters.AddWithValue("@alarmTimes", bpDB_DTO.AlarmDateTimes.ToArray().SelectMany(value => BitConverter.GetBytes(value.Ticks)).ToArray());

			command.ExecuteScalar();
			conn.Close();
		}
    }
}
