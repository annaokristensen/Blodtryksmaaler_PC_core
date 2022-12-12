﻿using DataLayer_PC;
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
            IMeasurementControlPC MesControl = new MeasurementControlPC(mesDataAccessObj);
            GUISTOlist = MesControl.ReadValues();
         //  BPMesDataGUI_DTO GUIDTOobj = MesControl.GetValues(); //GAMMEL 
            
            BPMesDataGUI_DTO GUIDTOobj;
            //MesControl.GetValues(out GUIDTOobj);
            GUIDTOobj = GUISTOlist[GUISTOlist.Count];

            List<double> VoltDataFromUDPList = GUIDTOobj.RawDataList;

	        foreach (double rawData in GUIDTOobj.RawDataList)
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
	        List<double> voltListTest = new List<double>() { 12.5, 17.3, 33.5, 55.5, 70.2};
	        return voltListTest;
        }

        public string RegressionCalculator(List<double> y, List<double> x)
        {
            var squarex = x.Sum(e => Math.Pow(e - x.Average(), 2));
            var xy = x.Zip(y, (first, second) => (first - x.Average()) * (second - y.Average())).Sum();
            double b1 = xy / squarex;
            double b0 = y.Average() - (x.Average() * b1);
            CalibrationValue = b1;
            return "Kalibrering= " + b1.ToString();
        }
    }
}
