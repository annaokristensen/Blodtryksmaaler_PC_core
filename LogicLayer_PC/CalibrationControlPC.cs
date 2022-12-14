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
        private IMeasurementDataAccess mesDataAccessObj;
        private ICalbrationFileAcess calbrationFileAcess;
        public List<BPMesDataGUI_DTO> GUISTOlist;
        public double CalibrationValue { get; set; }
        public double Volt { get; set; }
        private double b1 = 0;
        private double b0 = 0;
        private double b1Rounded = 0;


		public CalibrationControlPC()
        {
            mesDataAccessObj = new MeasurementDataAccess();
            calbrationFileAcess = new CalibrationFileAcess();
            Volt = 0;
        }

        /// <summary>
        /// METODEN som skal returnere den gennemsnitlige spændingsværdi der er modtager fra udp for det pågældende tryk
        /// </summary>
        /// <returns></returns>
        //
        public void SaveCalibrationValue()
        {
            double value = CalibrationValue;
            calbrationFileAcess.ReplaceValue(value);
        }
        public double GetVoltFromUDP()
        {
           List<double> VoltDataFromUDPList = new List<double>();

	        foreach (double rawData in mesDataAccessObj.GetOneSecond())
	        {
		        VoltDataFromUDPList.Add(rawData);
	        }

	        double avgVolt = VoltDataFromUDPList.Average();

	        return avgVolt;
        }

        /// <summary>
        /// TEST-METODE som bare returnerer nogle værdier til kalibrering
        /// </summary>
        /// <returns></returns
        public List<double> GetVoltTest()
        {
	        List<double> voltListTest = new List<double>() { 20, 25, 30, 35, 40};
	        return voltListTest;
        }

        public double RegressionCalculator(List<double> y, List<double> x)
        {
            var squarex = x.Sum(e => Math.Pow(e - x.Average(), 2));
            var xy = x.Zip(y, (first, second) => (first - x.Average()) * (second - y.Average())).Sum();
            b1 = xy / squarex;
            b0 = y.Average() - (x.Average() * b1);
            CalibrationValue = b1;
            b1Rounded = Math.Round(b1, 2);
            
            return b1Rounded;
        }

        public List<double> GetLinearYValues(int counter, double offset, double lastYValue)
        {
	        List<double> linearYValues = new List<double>();
			double slope = b1Rounded;
            double midlSlope = offset;
            double nextSlope = offset;
            int count1;
            

            for (count1 = 0; count1 < counter-1; count1++)
	        {
                if(count1 < 1)
	                linearYValues.Add(midlSlope);
                else
                {
					nextSlope = midlSlope * slope;
					linearYValues.Add(nextSlope);
					midlSlope = nextSlope;
				}
	        }

            for (int i = count1; i < lastYValue; i++)
            {
	            if (i < lastYValue - 1)
	            {
		            nextSlope = midlSlope * slope;
		            midlSlope = nextSlope;
				}
	            else
					midlSlope = nextSlope;
			}
            linearYValues.Add(nextSlope);
            
            return linearYValues;
        }
    }
}
