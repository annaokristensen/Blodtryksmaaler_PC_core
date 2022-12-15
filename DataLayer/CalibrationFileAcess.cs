using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer_PC
{
    public class CalibrationFileAcess : ICalbrationFileAcess
    {
        private string CalibrationPath = @"CalibrationValue.txt";

        public double ReadValue()
        {
	        string readCalibrationValue = File.ReadAllText("CalibrationValue.txt");
	        readCalibrationValue = readCalibrationValue.Replace('.', ',');
	        double calibrationValue = Convert.ToDouble(readCalibrationValue);
	        return calibrationValue;

          
        }

        public void ReplaceValue(double value)
        {
            File.WriteAllTextAsync(CalibrationPath,Convert.ToString(value));
        }
    }
}
