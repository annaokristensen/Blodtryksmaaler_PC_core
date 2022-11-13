﻿using DTO_PC;
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
        private SqlConnection connection;
        private SqlCommand command;
        private string connectionstring = @"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TestDBProjekt3;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public SaveMeasurement()
        {
			conn = new SqlConnection(@"Data Source=LAPTOP-JKBR8I3G\SQLEXPRESS;Initial Catalog=BloodPressureData_DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

		public void SaveBPMeasurement(BPMesDataDB_DTO bpDB_DTO, string cpr)
		{
			conn.Open();
			//sql-kode som sørger for at gemme i databasen. Det er delt op på flere linjer for syns skyld
			string queryString = "INSERT INTO dbo.MeasurementData_DB(" +
			                     "cpr" +
			                     ", startDateTime" +
			                     ", stopDateTime" +
			                     ", rawData" +
			                     ", systoliskValues" +
			                     ", diastoliskValues" +
			                     ", pulseValues" +
			                     ", middelValues)" +
			                     //  ", alarmDateTimes" +
			                     " VALUES (" +
			                     cpr +
			                     ", '" + bpDB_DTO.StartDateTime.ToString("yyyMMdd HH:mm:ss") + "'" +
			                     ", '" + bpDB_DTO.StopDateTime.ToString("yyyMMdd HH:mm:ss") + "'" +
			                     ", " + "@rawData" +
			                     ", " + "@sysValues" +
			                     ", " + "@diaValues" +
			                     ", " + "@pulseValues" +
			                     ", " + "@middelValues)";
			                   //  ", " + "@alarmTimes)";

			SqlCommand command = new SqlCommand(queryString, conn);
			//Kode der sørger for at de List's vi har, bliver gemt på det korrekte format:
			command.Parameters.AddWithValue("@rawData", bpDB_DTO.RawData.ToArray().SelectMany(value => BitConverter.GetBytes(value).ToArray()));
			command.Parameters.AddWithValue("@sysValues", bpDB_DTO.SystoliskValues.ToArray().SelectMany(value => BitConverter.GetBytes(value).ToArray()));
			command.Parameters.AddWithValue("@diaValues", bpDB_DTO.DiastoliskValues.ToArray().SelectMany(value => BitConverter.GetBytes(value).ToArray()));
			command.Parameters.AddWithValue("@pulseValues", bpDB_DTO.PulseValues.ToArray().SelectMany(value => BitConverter.GetBytes(value).ToArray()));
			command.Parameters.AddWithValue("@middelValues", bpDB_DTO.MiddelValues.ToArray().SelectMany(value => BitConverter.GetBytes(value).ToArray()));
			//command.Parameters.AddWithValue("@alarmTimes", bpDB_DTO.AlarmDateTimes.ToArray().SelectMany(value => BitConverter.GetBytes(value.Ticks).ToArray()));
			command.ExecuteScalar();
			//command.ExecuteNonQuery();
			conn.Close();
		}


        public void SetMeasurement(List<BPMesDataGUI_DTO> bpMesData, string cpr)
        {

            double[] pulseValueArray = new double[bpMesData.Count];
            connection = new SqlConnection(connectionstring);
            string insertStringParam = @"INSERT INTO measurementValue (cpr, pulse) values ('" + cpr + "',@bpMesData,')";
            using (command = new SqlCommand(insertStringParam, connection))
            {
                command.Parameters.AddWithValue("@pulseVoltage", pulseValueArray); //blob
            }
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();


        }
    }
}