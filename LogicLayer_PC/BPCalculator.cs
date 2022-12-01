using LogicLayer_RPi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer_RPi
{
    public class BPCalculator 
    {
        public double systole { get; private set; }
        public double diastole { get; private set; }
        public double middel { get; private set; }
        public int puls { get; private set; }
        
        private List<double> measurement;
        private List<double>[] mesLists = new List<double>[4];
        private int counter = 0;
        private bool starter = false;

        public double[] getValues(List<double> measurement)
        {
            if (starter == false)
            {
                if (counter == 0)
                {
                    mesLists[0] = measurement;
                    counter++;
                }
                else if (counter == 1)
                {
                    mesLists[1] = measurement;
                    counter++;
                }
                else if (counter == 2)
                {
                    mesLists[2] = measurement;
                    counter++;
                }
                else if (counter == 3)
                {
                    mesLists[3] = measurement;
                    counter++;
                }
                else if (counter == 4)
                {
                    mesLists[4] = measurement;
                    counter = 0;
                    starter = true;
                }
            }
            else
            {
                mesLists[0] = mesLists[1];
                mesLists[1] = mesLists[2];
                mesLists[2] = mesLists[3];
                mesLists[3] = mesLists[4];
                mesLists[5] = measurement;
            }

            this.measurement.Clear();
            
            foreach (double value in mesLists[0])
            {
                this.measurement.Add(value);
            }
            foreach (double value in mesLists[1])
            {
                this.measurement.Add(value);
            }
            foreach (double value in mesLists[2])
            {
                this.measurement.Add(value);
            }
            foreach (double value in mesLists[3])
            {
                this.measurement.Add(value);
            }
            foreach (double value in mesLists[4])
            {
                this.measurement.Add(value);
            }

            calcAverage(this.measurement);

            double[] values = new double[3];

            values[0] = systole;
            values[1] = diastole;
            values[2] = middel;
            values[3] = Convert.ToDouble(puls);

            return values;
        }
        private void calcAverage(List<double> measurement)
        {
            double totalBP = measurement.Sum();
            int bpDataPoints = measurement.Count();
            double averageBP = totalBP / bpDataPoints;

            getSysBP(bpDataPoints, averageBP);
            getDiaBP(bpDataPoints, averageBP);

            middel = (((systole - diastole) * 1 / 3) + diastole);

        }
        
        private void getSysBP(int bpDataPoints, double averageBP)
        {
            double highLimit = averageBP*1.03;
            double highPeakTotal = 0;
            int highPeakCounter = 0;

            for (int i=0;i<bpDataPoints; i++)
            {
                if(measurement[i]>highLimit&&measurement[i]>measurement[i-1]&&measurement[i]>measurement[i+1])
                {
                    highPeakTotal += measurement[i];
                    highPeakCounter++;
                }
            }

            double sys = highPeakTotal / highPeakCounter;
            systole = sys;
            puls = highPeakCounter;
            
        }
        private void getDiaBP(int bpDataPoints, double averageBP)
        {
            double lowLimit = averageBP * 0.97;
            double lowPeakTotal = 0;
            int lowPeakCounter = 0;
           

            for (int i = 0; i < bpDataPoints; i++)
            {
                if (measurement[i] < lowLimit && measurement[i] < measurement[i - 1] && measurement[i] < measurement[i + 1])
                {
                    lowPeakTotal += measurement[i];
                    lowPeakCounter++;
                }
            }

            double dia = lowPeakTotal / lowPeakCounter;
            diastole = dia;
            
        }
    }
}
