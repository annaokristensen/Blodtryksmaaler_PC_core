using System;
using System.Collections.Generic;
using System.Text;


namespace DTO_PC
{
    /// <summary>
    /// DTO eksempel
    /// </summary>
    public class Eksempel
    {
        public Eksempel(double sekunder, double værdi)
        {
            Sekunder = sekunder;
            Værdi = værdi;
        }
        /// <summary>
        /// Eksempel
        /// </summary>
        public double Sekunder { get; set; }
        /// <summary>
        /// Eksempel
        /// </summary>
        public double Værdi { get; set; }
    }
}
