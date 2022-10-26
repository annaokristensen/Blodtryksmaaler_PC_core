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
using Presentation_Layer_PC;

namespace Presentation_Layer
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			this.Hide();
			CPR_Window cprWindowObj = new CPR_Window();
			//Sørger for at patientens (det indtastede) cpr-nummer fremstår af cpr-tekstboksen
			cpr_textbox.Text = cprWindowObj.cpr;

			//Sørger for at cpr-vinduet åbner og at koden for mainwindow ikke kører videre før det er lukket
			if (!cprWindowObj.ShowDialog().Value)
				this.Close();
			else
				this.Show();
		}
	}
}
