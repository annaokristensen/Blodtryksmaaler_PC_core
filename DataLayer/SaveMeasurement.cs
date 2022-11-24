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

			SqlCommand command = new SqlCommand(queryString, conn);

			//command.ExecuteScalar();

			//Kode der sørger for at de List's vi har, bliver gemt på det korrekte format. NEDENSTÅENDE KODE ER KOPIERET FRA NETTET
			List<byte> values = bpDB_DTO.RawData.ToArray().SelectMany(value => BitConverter.GetBytes(value).ToArray()).ToList();
			command.Parameters.AddWithValue("@rawData", values);

			List<byte> values2 = bpDB_DTO.SystoliskValues.ToArray().SelectMany(value2 => BitConverter.GetBytes(value2).ToArray()).ToList();
			command.Parameters.AddWithValue("@sysValues", values2);

			List<byte> values3 = bpDB_DTO.DiastoliskValues.ToArray().SelectMany(value3 => BitConverter.GetBytes(value3).ToArray()).ToList();
			command.Parameters.AddWithValue("@diaValues", values3);

			List<byte> values4 = bpDB_DTO.PulseValues.ToArray().SelectMany(value4 => BitConverter.GetBytes(value4).ToArray()).ToList();
			command.Parameters.AddWithValue("@pulseValues", values4);

			List<byte> values5 = bpDB_DTO.MiddelValues.ToArray().SelectMany(value5 => BitConverter.GetBytes(value5).ToArray()).ToList();
			command.Parameters.AddWithValue("@middelValues", values5);

			List<byte> values6 = bpDB_DTO.AlarmDateTimes.ToArray().SelectMany(value6 => BitConverter.GetBytes(value6.Ticks).ToArray()).ToList();
			command.Parameters.AddWithValue("@alarmTimes", values2);

			//Har slettet rawData og sysValues
			//command.Parameters.AddWithValue("@diaValues", bpDB_DTO.DiastoliskValues.ToArray().SelectMany(value => BitConverter.GetBytes(value).ToArray()));
			//command.Parameters.AddWithValue("@pulseValues", bpDB_DTO.PulseValues.ToArray().SelectMany(value => BitConverter.GetBytes(value).ToArray()));
			//command.Parameters.AddWithValue("@middelValues", bpDB_DTO.MiddelValues.ToArray().SelectMany(value => BitConverter.GetBytes(value).ToArray()));
			//command.Parameters.AddWithValue("@alarmTimes", bpDB_DTO.AlarmDateTimes.ToArray().SelectMany(value => BitConverter.GetBytes(value.Ticks).ToArray()));

			command.ExecuteScalar();
			//command.ExecuteNonQuery();
			conn.Close();
		}
    }
}
