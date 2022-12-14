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
            FileStream fileStream = new FileStream(CalibrationPath, FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader reader = new StreamReader(fileStream);

            double value = Convert.ToDouble(reader.ReadLine());
            fileStream.Close();

            return value;
        }

        public void ReplaceValue(double value)
        {
            File.WriteAllText(CalibrationPath, String.Empty);

            FileStream fileStream = new FileStream(CalibrationPath, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter writer = new StreamWriter(fileStream);
            writer.WriteLine(Convert.ToString(value));

            fileStream.Close();

        }
    }
}
