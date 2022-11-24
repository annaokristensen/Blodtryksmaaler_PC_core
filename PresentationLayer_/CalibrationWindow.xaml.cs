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

namespace Presentation_Layer_PC
{
	/// <summary>
	/// Interaction logic for CalibrationWindow.xaml
	/// </summary>
	public partial class CalibrationWindow : Window
	{
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
		}
	}
}
