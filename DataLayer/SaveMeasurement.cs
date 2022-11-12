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
        private SqlConnection connection;
        private SqlCommand command;
        private string connectionstring = @"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TestDBProjekt3;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public SaveMeasurement() { }
        public void setMeasurement(List<BPMeasurementData_DTO> bpMesData, string cpr)
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
