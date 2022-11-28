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
        public ChartValues<double> XPressure { get; set; }

        List<double> xx = new List<double>();
        List<double> yy = new List<double>();

        int taeller = 0;

		CalibrationControlPC calibrationObject = new CalibrationControlPC();

        //public CalibrationWindow()
        //{
        //	InitializeComponent();
        //}

        public CalibrationWindow()
        {
            InitializeComponent();
            YVolt = new ChartValues<double>();
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
	        foreach (double volt in calibrationObject.GetVoltTest())
	        {
		        midlVoltList.Add(volt);
	        }

	        try
	        {
		        xx.Add(Convert.ToDouble(enterPressure_textbox.Text));
		        XPressure.Add(xx[taeller]);
		        yy.Add(midlVoltList[taeller]);
		        YVolt.Add(yy[taeller]);

		        enterPressure_textbox.Clear();
		        DataContext = this;
		        taeller++;
			}
	        catch (Exception exception)
	        {
		        MessageBox.Show(this, exception.Message, "Fejl");
	        }

        }

        private void makeLinearReg_button_Click(object sender, RoutedEventArgs e)
        {
            calibrationObject.RegressionCalculator(yy,xx);
        }

        private void finishCalibration_button_Click_1(object sender, RoutedEventArgs e)
        {
	        this.Close();
		}
    }
}
