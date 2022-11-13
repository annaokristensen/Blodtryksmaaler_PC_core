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
using Presentation_Layer;

namespace Presentation_Layer_PC
{
	/// <summary>
	/// Interaction logic for CPR_Window.xaml
	/// </summary>
	public partial class CPR_Window : Window
	{
		public string Cpr { get; set; }
		CPRControl cprControl = new CPRControl();

		
		public CPR_Window()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			//Sørger for at fejlmeddelsen ikke vises når vinduet loades
			errorMessage_label.Visibility = Visibility.Hidden;
			//Sørger for at cursoren starter i tekstfeltet til cpr-nummeret
			enterCPR_textbox.Focus();
		}

		private void register_button_Click(object sender, RoutedEventArgs e)
		{
			//tjekker for om cpr-nummeret er i databasen ved at kalde metoden ValidateCpr og angive det indtastede cpr-nummer som parameter
			if (cprControl.ValidateCpr(enterCPR_textbox.Text))
			{
				Cpr = enterCPR_textbox.Text;
				this.DialogResult = true;
				this.Close();
			}
			//Viser en fejlmeddelse hvis det indtastede cpr-nummer ikke findes i databasen
			else
			{
				errorMessage_label.Visibility = Visibility.Visible;
			}
		}

		//Metode der kan kaldes for at få det indtastede cpr-nummer gemt sammen med målingen
		public string GetEnteredCpr()
		{
			return Cpr;
		}

		//Hvad der skal ske hvis brugeren trykker i cpr-tekstfeltet (fejlmeddelsen skal skjules)
		private void enterCPR_textbox_GotFocus(object sender, RoutedEventArgs e)
		{
			errorMessage_label.Visibility = Visibility.Hidden;
		}
	}
}
