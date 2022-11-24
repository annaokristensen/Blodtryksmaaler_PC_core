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

namespace Presentation_Layer_PC
{
	/// <summary>
	/// Interaction logic for CalibrationWindow.xaml
	/// </summary>
	public partial class CalibrationWindow : Window
	{
        //public CalibrationWindow()
        //{
        //	InitializeComponent();
        //}

        //private void registerPressure_button_Click(object sender, RoutedEventArgs e)
        //{
        //          List<double> yy = new List<double>();
        //          List<double> xx = new List<double>();

        //          foreach (string x in enterPressure_textbox.Text.Split(","))
        //          {
        //              yy.Add(double.Parse(x));
        //          }
        //          foreach (string x in enterPressure_textbox2.Text.Split(","))
        //          {
        //              xx.Add(double.Parse(x));
        //          }
        //          MessageBox.Show(RegressionCalculator(yy, xx));
        //      }

        //      public string RegressionCalculator(List<double> y, List<double> x)
        //      {
        //          var squarex = x.Sum(e => Math.Pow(e - x.Average(), 2));
        //          var xy = x.Zip(y, (first, second) => (first - x.Average()) * (second - y.Average())).Sum();
        //          double b1 = xy / squarex;
        //          double b0 = y.Average() - (x.Average() * b1);
        //          return "Kalibrering= " + b1.ToString();
        //      }

        public CalibrationWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void finishCalibration_button_Click(object sender, RoutedEventArgs e)
        {
            MaintenanceWindow maintenanceWindowObj = new MaintenanceWindow();
            this.Close();
            maintenanceWindowObj.ShowDialog();
            //Kommentar
        }

    }
}
