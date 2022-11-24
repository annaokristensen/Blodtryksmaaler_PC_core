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
        public ChartValues<int> XPressure { get; set; }

        CalibrationControlPC calibrationObject = new CalibrationControlPC();

        //public CalibrationWindow()
        //{
        //	InitializeComponent();
        //}

        public CalibrationWindow()
        {
            InitializeComponent();
            YVolt = new ChartValues<double>();
            XPressure = new ChartValues<int>();

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
                      List<double> xx = new List<double>();


                      foreach (string x in enterPressure_textbox.Text)
                      {
                         xx.Add(double.Parse(x));
                      }


            //          foreach (string x in enterPressure_textbox2.Text.Split(","))
            //          {
            //              xx.Add(double.Parse(x));
            //          }
            //          MessageBox.Show(RegressionCalculator(yy, xx));
        }

        private void makeLinearReg_button_Click(object sender, RoutedEventArgs e)
        {
            calibrationObject.RegressionCalculator();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
