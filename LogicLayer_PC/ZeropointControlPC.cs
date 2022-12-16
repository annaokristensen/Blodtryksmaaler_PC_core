using DataLayer_PC;
using DTO_PC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer_PC
{public class ZeropointControlPC
    {
        private IMeasurementDataAccess mesDataAccessObj;
        public double Zeropoint { get; set; }
        
        public ZeropointControlPC()
        {
            mesDataAccessObj = new MeasurementDataAccess();
            Zeropoint = -1.9;
        }
        public double GetZeropoint()
        {
            List<double> list = mesDataAccessObj.GetOneSecond();
            
            double total = 0;
            foreach (double s in list)
            {
                total += s;
            }

            Zeropoint = total / list.Count;
            return Zeropoint;
        }
    }
}
