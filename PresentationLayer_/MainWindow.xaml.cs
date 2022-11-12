using DTO_PC;
using LogicLayer_PC;
using Presentation_Layer_PC;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using DataLayer_PC;
using DTO_PC;
using LogicLayer_PC;
using Presentation_Layer_PC;
using System.Runtime.ConstrainedExecution;


namespace Presentation_Layer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public BPMeasurementData_DTO DTOobj;
        private MeasurementControlPC mesControlObj;
        private System.Windows.Threading.DispatcherTimer dispatcherTimer;
        int taeller = 0;

        public MainWindow()
        {
            InitializeComponent();
            DTOobj = new BPMeasurementData_DTO();
            mesControlObj = new MeasurementControlPC();
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
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

            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 2); //Intervallet for hvor ofte data skifter på GUI'en   
		}
        private void DispatcherTimer_Tick(object? sender, EventArgs e)
        {
            List<BPMeasurementData_DTO> dtoGUI_list = mesControlObj.GetAllValues();

            if (taeller < dtoGUI_list.Count)
            {
                middleBTValue_textbox.Text = Convert.ToString(dtoGUI_list[taeller].MiddelValue);
                pulseValue_textbox.Text = Convert.ToString(dtoGUI_list[taeller].Pulse);
                sysDiaValue_textbox.Text = dtoGUI_list[taeller].SystoliskValue + " / " + dtoGUI_list[taeller].DiastoliskValue;
                taeller++;
                Alarm();
            }
        }
        private void saveChanges_button_Click(object sender, RoutedEventArgs e)
        {

        }
        private void startMeasurement_button_Click(object sender, RoutedEventArgs e)
        {
            //Når der trykkes på "Start Måling" så går timeren i gang. Den udfører det den er implementeret til med det interval den er sat til at gøre det med
            dispatcherTimer.Start();
        }        
        private void Alarm()
        {
            if (Convert.ToInt32((middleBTMax_textbox.Text)) < Convert.ToInt32(middleBTValue_textbox.Text) ||
                Convert.ToInt32(middleBTValue_textbox.Text) < Convert.ToInt32(middleBTMin_textbox.Text))
            {
                middleBTValue_textbox.Foreground = Brushes.Red;
            }
            else
            {
                middleBTValue_textbox.Foreground = Brushes.Black;
            }
        }
        private void stopAndSave_button_Click(object sender, RoutedEventArgs e)
        {





            //if (findesIOffentligDB = true && TB_kommentar.Text != "")
            //{
            //    logikObj.setMaaling(CPR, voltage, dato, AV_blok, TB_kommentar.Text);
            //    Close();
            //}
            //else
            //{
            //    MessageBox.Show("Skriv en kommentar til målingen i kommentarfeltet. Gem derefter målingen");
            //}


            ////findesIOffentligDB = true;
            ////Close();
        }
    }
}

