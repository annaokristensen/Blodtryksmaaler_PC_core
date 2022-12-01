using DataLayer_PC;
using DTO_PC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer_PC
{
    internal class ZeropointControlPC
    {
        private MeasurementDataAccess mesDataAccessObj;
        public double Zeropoint { get; private set; }
        
        public ZeropointControlPC()
        {
            mesDataAccessObj = new MeasurementDataAccess();
            Zeropoint = 0;
        }
        public void GetZeropoint()
        {
            //List<double> list = mesDataAccessObj.;

            //double total = 0;
            //foreach(double s in list)
            //{
            //    total += s;
            //}

            //Zeropoint = total / list.Count;
        }
    }
}
