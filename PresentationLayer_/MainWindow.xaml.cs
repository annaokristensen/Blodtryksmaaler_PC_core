using System;
using System.Collections.Generic;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DataLayer_PC;
using DTO_PC;
using LogicLayer_PC;
using Presentation_Layer_PC;
using System.Forms;




namespace Presentation_Layer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public BPMeasurementData_DTO DTOobj;
        private MeasurementControlPC mesControlObj;
        

        public MainWindow()
        {
            InitializeComponent();
            DTOobj = new BPMeasurementData_DTO();
            mesControlObj = new MeasurementControlPC();
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
        private void saveChanges_button_Click(object sender, RoutedEventArgs e)
        {

        }
        private void startMeasurement_button_Click(object sender, RoutedEventArgs e)
        {
	        //middleBTMax_textbox.Text = Convert.ToString(mesControlObj.GetMiddelValueTest());
	        //mesControlObj.GetMiddelValueTest();
            middleBTValue_textbox.Text = Convert.ToString(DTOobj.MiddelValue); //Udskriver MiddelValue i tekstboksen
        }

        private void Alarm()
        {
            if (Convert.ToInt32((middleBTMax_textbox.Text)) < Convert.ToInt32(middleBTValue_textbox.Text) ||
                Convert.ToInt32(middleBTValue_textbox.Text) < Convert.ToInt32(middleBTMin_textbox.Text))
            {
                //middleBTValue_textbox.ForeColor = Color.Red;
                // kan ikke finde ud af at gøre teksten rød. er lidt i tvivl om vi mangler en nuget package
            }
        }

	}
}

