using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer_PC
{
    public class DownSampler
    {
        private double downsamplingsfactor = 3;
        public List<double> downSampler(List<double> list)
        {
            int counter = 1;
            List<double> newList = new List<double>();
            foreach (double value in list)
            {
                if (counter<downsamplingsfactor)
                {
                    counter++;
                }
                else
                {
                    newList.Add(value);
                    counter = 1;
                }
            }
            return newList;
        }
    }
}
