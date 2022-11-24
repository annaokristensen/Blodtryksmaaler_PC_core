using DataLayer_PC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public List<double> GetVolt()
        {
            List<double> VoltList = mesDataAccessObj.

            double total = 0;
            foreach(double s in VoltList)
            {
                total += s;
            }

            Volt = total / VoltList.Count;
            return VoltList;
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
