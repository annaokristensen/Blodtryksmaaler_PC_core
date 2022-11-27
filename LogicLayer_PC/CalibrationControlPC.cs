using DataLayer_PC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_PC;

namespace LogicLayer_PC
{
    public class CalibrationControlPC
    {
        private MeasurementDataAccess mesDataAccessObj;
        public double Volt { get; set; }

        public CalibrationControlPC()
        {
            mesDataAccessObj = new MeasurementDataAccess();
            Volt = 0;
        }

        public double GetVolt()
        {
	        int taeller = 0;
            BPMesDataGUI_DTO GUIDTOobj = new BPMesDataGUI_DTO();
            List<double> VoltList = new List<double>() { 12.5, 17.3, 33.5, 55.5 };

			/*foreach (double rawData in GUIDTOobj.RawDataList)
            {
	            VoltList.Add(rawData);
            }*/

			//double avgVolt = VoltList.Average();

			//return VoltList;
			taeller++;
			return VoltList[taeller];
        }

        public string RegressionCalculator(List<double> y, List<double> x)
        {
            var squarex = x.Sum(e => Math.Pow(e - x.Average(), 2));
            var xy = x.Zip(y, (first, second) => (first - x.Average()) * (second - y.Average())).Sum();
            double b1 = xy / squarex;
            double b0 = y.Average() - (x.Average() * b1);
            return "Kalibrering= " + b1.ToString();
        }
    }
}
