﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LogicLayer_PC;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Linq;
using LiveCharts;

namespace Presentation_Layer_PC
{
	/// <summary>
	/// Interaction logic for CalibrationWindow.xaml
	/// </summary>
	public partial class CalibrationWindow : Window
	{
        public ChartValues<double> YVolt { get; set; }
        public ChartValues<double> LinearSlopeY { get; set; }
        public ChartValues<double> LinearSlopeX { get; set; }
		public ChartValues<double> XPressure { get; set; }

        List<double> xx = new List<double>();
        List<double> yy = new List<double>();
        int counter = 0;
		List<double> xSlopeValues = new List<double>(){0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15};
		private List<double> ySlopeValues = new List<double>();
		private double linearSlope = 0;
		private double offset = 0;

		CalibrationControlPC calibrationControlObj = new CalibrationControlPC();

        public CalibrationWindow()
        {
            InitializeComponent();
            YVolt = new ChartValues<double>();
			LinearSlopeY = new ChartValues<double>();
			LinearSlopeX = new ChartValues<double>();
			XPressure = new ChartValues<double>();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
	        enterPressure_textbox.Focus();
        }

        private void registerPressure_button_Click(object sender, RoutedEventArgs e)
        {
	        enterPressure_textbox.Focus();

            //Koden henter data fra en test-metode, men det kan nemt genbruges til det rigtige data
	        List<double> midlVoltList = new List<double>();
	        foreach (double volt in calibrationControlObj.GetVoltTest())
	        {
		        midlVoltList.Add(volt);
	        }

	        try
	        {
		        xx.Add(Convert.ToDouble(enterPressure_textbox.Text));
		        XPressure.Add(xx[counter]);
		        yy.Add(midlVoltList[counter]);
		        YVolt.Add(yy[counter]);

		        enterPressure_textbox.Clear();
		        DataContext = this;

		        counter++;
			}
	        catch (Exception exception)
	        {
		        MessageBox.Show(this, exception.Message, "Fejl");
	        }
        }


        private void finishCalibration_button_Click_1(object sender, RoutedEventArgs e)
        {
			calibrationControlObj.SaveCalibrationValue();
	        this.Close();
		}

		private void makeLinearReg_button_Click(object sender, RoutedEventArgs e)
		{
			linearSlope = calibrationControlObj.RegressionCalculator(yy, xx);
			offset = yy[0];
			ySlopeValues.AddRange(calibrationControlObj.GetLinearYValues(linearSlope, counter, offset));
			List<double> YSlopePlusOffSet = new List<double>();


			for (int i = 0; i < counter; i++)
			{
				YSlopePlusOffSet.Add(ySlopeValues[i]);
			}


			//Tilføjer punkter til x-aksen og y-aksen
			for (int i = 0; i < counter; i++)
			{
				LinearSlopeX.Add(xSlopeValues[i]);
			}


			foreach (double yVal in YSlopePlusOffSet)
			{
				LinearSlopeY.Add(yVal);
			}

			calibrationSlope_textbox.Text = "Kalibrering: " + linearSlope;

		}
	}
}
