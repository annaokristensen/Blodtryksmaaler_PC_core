using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_PC;

namespace LogicLayer_PC
{
    public class BPCalculator 
    {
        public double systole { get; private set; }
        public double diastole { get; private set; }
        public double middel { get; private set; }
        public int puls { get; private set; }
        
        private List<double> measurement;
        private List<double>[] mesLists;
        private int counter = 0;
        private bool starter = false;

        public BPCalculator()
        {
            measurement = new List<double>();
            mesLists = new List<double>[4];
        }

        public void saveValues(BPMesDataGUI_DTO dto)
        {
            //if (starter == false)
            //{
            //    if (counter == 0)
            //    {
            //        mesLists[0] = dto.RawDataList;
            //        counter++;
            //    }
            //    else if (counter == 1)
            //    {
            //        mesLists[1] = dto.RawDataList;
            //        counter++;
            //    }
            //    else if (counter == 2)
            //    {
            //        mesLists[2] = dto.RawDataList;
            //        counter++;
            //    }
            //    else if (counter == 3)
            //    {
            //        mesLists[3] = dto.RawDataList;
            //        counter++;
            //    }
            //    else if (counter == 4)
            //    {
            //        mesLists[4] = dto.RawDataList;
            //        counter = 0;
            //        starter = true;
            //    }
            //}
            //else
            //{
            //    mesLists[0] = mesLists[1];
            //    mesLists[1] = mesLists[2];
            //    mesLists[2] = mesLists[3];
            //    mesLists[3] = mesLists[4];
            //    mesLists[5] = measurement;
            //}

            //this.measurement.Clear();
            
            //foreach (double value in mesLists[0])
            //{
            //    this.measurement.Add(value);
            //}
            ////foreach (double value in mesLists[1])
            ////{
            ////    this.measurement.Add(value);
            ////}
            ////foreach (double value in mesLists[2])
            ////{
            ////    this.measurement.Add(value);
            ////}
            ////foreach (double value in mesLists[3])
            ////{
            ////    this.measurement.Add(value);
            ////}
            ////foreach (double value in mesLists[4])
            ////{
            ////    this.measurement.Add(value);
            ////}

            //calcAverage(this.measurement);

            //double[] values = new double[3];

            //dto.SystoliskValue = systole;
            //dto.DiastoliskValue = diastole;
            //dto.MiddelValue = middel;
            //dto.Pulse = Convert.ToDouble(puls);

            dto.SystoliskValue = 10;
            dto.DiastoliskValue = 20;
            dto.MiddelValue = 30;
            dto.Pulse = Convert.ToDouble(40);

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
                int plusDiff = 1;
                int minusDiff =1;
                if (measurement[i] == measurement[i + plusDiff])
                {
                    plusDiff++;
                }
                else if (measurement[i] == measurement[i - minusDiff])
                {
                    minusDiff++;
                }
               else if (measurement[i]>highLimit&&measurement[i]>measurement[i-minusDiff]&&measurement[i]>measurement[i+plusDiff])
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
