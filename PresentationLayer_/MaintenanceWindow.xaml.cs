using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
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
using Presentation_Layer;

namespace Presentation_Layer_PC
{
	/// <summary>
	/// Interaction logic for MaintenanceWindow.xaml
	/// </summary>
	public partial class MaintenanceWindow : Window
    {
		bool isZeroPointAdjDone = false;
		private ZeropointControlPC zeropoint;
        
		public MaintenanceWindow(ZeropointControlPC zeropoint)
		{
			InitializeComponent();
			this.zeropoint = zeropoint;
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			zeroPointNotDone_errorMessage.Visibility = Visibility.Hidden;
            NulpunktjusteringComplete.Visibility = Visibility.Hidden;
        }

		private void moveOnToCpr_button_Click(object sender, RoutedEventArgs e)
		{
			//Når der trykkes på Videre til cpr og nulpunktjustering er udført, skal CprWindow åbne og MaintenanceWindow lukke
			if (isZeroPointAdjDone == true)
			{ 
				this.DialogResult = true;
				this.Close();
			}
			else
			{
				zeroPointNotDone_errorMessage.Visibility = Visibility.Visible;
			}
		}
		private void calibration_button_Click(object sender, RoutedEventArgs e)
		{
			//Når der trykkes på kalibrering, skal CalibrationWindow åbne og MaintenanceWindow lukker
			CalibrationWindow calibrationWindowObj = new CalibrationWindow();
			calibrationWindowObj.ShowDialog();
		}
		private void zeroPointAdjustment_button_Click(object sender, RoutedEventArgs e)
		{
			zeropoint.GetZeropoint();
			isZeroPointAdjDone = true;
            NulpunktjusteringComplete.Visibility = Visibility.Visible;

            zeroPointNotDone_errorMessage.Visibility = Visibility.Hidden;
		}
	}
}
