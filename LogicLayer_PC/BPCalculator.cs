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
            mesLists = new List<double>[5];
        }

        public void SaveValues(BPMesDataGUI_DTO dto)
        {
            measurement.Clear();
            if (counter < 5)
            {
                dto.SystoliskValue = 0;
                dto.DiastoliskValue = 0;
                dto.MiddelValue = 0;
                dto.Pulse = Convert.ToDouble(0);

                mesLists[counter] = dto.RawDataList;

                counter++;
            }
            else
            {
                mesLists[4] = mesLists[3];
                mesLists[3] = mesLists[2];
                mesLists[2] = mesLists[1];
                mesLists[1] = mesLists[0];
                mesLists[0] = dto.RawDataList;

                for (int i = 0; i < 5; i++)
                {
                    measurement.AddRange(mesLists[i]);
                }
                CalcAverage(measurement);

                dto.SystoliskValue = systole;
                dto.DiastoliskValue = diastole;
                dto.MiddelValue = middel;
                dto.Pulse = Convert.ToDouble(puls);
            }
        }
        private void CalcAverage(List<double> mesList)
        {
            double totalBP = mesList.Sum();
            int bpDataPoints = mesList.Count();
            double averageBP = totalBP / bpDataPoints;
            

            GetSysBP(bpDataPoints, averageBP);
            GetDiaBP(bpDataPoints, averageBP);

            middel = (((systole - diastole) * 1 / 3) + diastole);

        }
        
        private void GetSysBP(int bpDataPoints, double averageBP)
        {
            
            double lowLimit = averageBP * 0.97;
            double highLimit = averageBP * 1.05;
                      
            double highPeakTotal = 0;
            int highPeakCounter = 0;
            double systoleLoop = highLimit;

            bool setSys = false;
            bool setDia = false;

            for (int i=0;i<bpDataPoints; i++)
            {
                if (measurement[i] > highLimit)
                {
                    if (measurement[i] > systoleLoop)
                    {
                        systoleLoop = measurement[i];
                    }
                    setSys = true;
                }

                if (measurement[i] > lowLimit && measurement[i] < highLimit && setSys == true)
                {
                    highPeakTotal += systoleLoop;
                    highPeakCounter++;
                    systoleLoop = highLimit;
                    setSys = false;
                }
            }
            double sys = highPeakTotal / highPeakCounter;
            systole = sys;
            puls = highPeakCounter*12;
           
        }
        private void GetDiaBP(int bpDataPoints, double averageBP)
        {
            double lowLimit = averageBP * 0.97;
            double highLimit = averageBP * 1.05;

            double lowPeakTotal = 0;
            int lowPeakCounter = 0;
            double diastoleLoop = lowLimit;

            bool setDia = false;

            for (int i = 0; i < bpDataPoints; i++)
            {               
                if (measurement[i] < lowLimit)
                {
                    if (measurement[i] < diastoleLoop)
                    {
                        diastoleLoop = measurement[i];
                    }
                    setDia = true;
                }
                if (measurement[i] > lowLimit && measurement[i] < highLimit && setDia == true)
                {
                    lowPeakTotal += diastoleLoop;
                    lowPeakCounter++;
                    diastoleLoop = lowLimit;
                    setDia = false;
                }
            }
            double dia = lowPeakTotal / lowPeakCounter;
            diastole = dia;
        }
    }
}
