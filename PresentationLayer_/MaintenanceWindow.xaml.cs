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
	/// Interaction logic for MaintenanceWindow.xaml
	/// </summary>
	public partial class MaintenanceWindow : Window
    {
        
		public MaintenanceWindow()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{

		}

		private void opretforbindelse_button_Click(object sender, System.Windows.RoutedEventArgs e)
		{

		}

		private void moveOnToCpr_button_Click(object sender, RoutedEventArgs e)
		{
			//Når der trykkes på Videre til cpr og nulpunktjustering er udført, skal CprWindow åbne og MaintenanceWindow lukke
			// if (nulpunktjustering er udført)
			this.DialogResult = true;
			this.Close();
		}

		private void calibration_button_Click(object sender, RoutedEventArgs e)
		{
			//Når der trykkes på kalibrering, skal CalibrationWindow åbne og MaintenanceWindow lukker
			CalibrationWindow calibrationWindowObj = new CalibrationWindow();
			this.Close();
			calibrationWindowObj.ShowDialog();
		}
	}
}
