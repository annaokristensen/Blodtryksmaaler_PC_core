using System;
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

		private void finishCalibration_button_Click(object sender, RoutedEventArgs e)
        {
            MaintenanceWindow maintenanceWindowObj = new MaintenanceWindow();
            this.Close();
            maintenanceWindowObj.ShowDialog();
            //Kommentar
        }

        private void registerPressure_button_Click(object sender, RoutedEventArgs e)
        {
	        enterPressure_textbox.Focus();
	        int taeller = 0;
	        //List<double> midlVoltList = new List<double>();

	        /*foreach (double volt in calibrationObject.GetVolt())
	        {
		        midlVoltList.Add(volt);
	        }*/


	        xx.Add(Convert.ToDouble(enterPressure_textbox.Text));
	        XPressure.Add(xx[taeller]);
	        //yy.Add(calibrationObject.GetVolt());
	        yy.Add(calibrationObject.GetVolt());
	        YVolt.Add(yy[taeller]);

	        enterPressure_textbox.Clear();
	        DataContext = this;

	        taeller++;
        }

        private void makeLinearReg_button_Click(object sender, RoutedEventArgs e)
        {
            calibrationObject.RegressionCalculator(yy,xx);
        }
	}
}
