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
using System.Runtime.ConstrainedExecution;
using LiveCharts;
using LiveCharts.Wpf;


namespace Presentation_Layer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
	    private MeasurementControlPC mesControlObj;
        private StopAndSave stopAndSaveObj;
        private System.Windows.Threading.DispatcherTimer dispatcherTimer;
        int taeller = 0;
        private int middelMax = 0;
        private int middelMin = 0 ;
        public string cpr { get; set; }
        private List<BPMesDataGUI_DTO> dtoGUI_list;
        private List<DateTime> alarmTriggeredTimes;


		//Tilknyt data til livecharts graf
		public ChartValues<double> Ymiddel { get; set; }
        public ChartValues<string> XdateTime { get; set; }

        public  ChartValues<double> Ysystolic { get; set; }
        public ChartValues<double> Ydiastolic { get; set; }
        public ChartValues<double> Ypulse { get; set; }


        public MainWindow()
        {
            InitializeComponent();
            mesControlObj = new MeasurementControlPC();
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            mesControlObj = new MeasurementControlPC();
            stopAndSaveObj = new StopAndSave();
            alarmTriggeredTimes = new List<DateTime>();

            Ymiddel = new ChartValues<double>();
            XdateTime = new ChartValues<string>();
            Ysystolic = new ChartValues<double>();
            Ydiastolic = new ChartValues<double>();
            Ypulse = new ChartValues<double>();
            
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Hide();
            CPR_Window cprWindowObj = new CPR_Window();

            //Sørger for at cpr-vinduet åbner og at koden for mainwindow ikke kører videre før det er lukket
            if (!cprWindowObj.ShowDialog().Value)
                this.Close();
            else
                this.Show();

            //Sørger for at patientens (det indtastede) cpr-nummer fremstår af cpr-tekstboksen
            cpr_textbox.Text = cprWindowObj.GetEnteredCpr();

			dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 1, 0); //Intervallet for hvor ofte data skifter på GUI'en
															  
			middelMax = Convert.ToInt32(middleBTMax_textbox.Text);
			middelMin = Convert.ToInt32(middleBTMin_textbox.Text);
		}
        private void DispatcherTimer_Tick(object? sender, EventArgs e)
        {
            dtoGUI_list = mesControlObj.GetAllValues();
            const int graphPointLimit = 8; //Grænsen for hvor mange punkter der bliver vist på graferne af gangen
            const int removeFactor = 9; //Faktoren der sørger for de ældste punkter bliver fjernet. Skal vist være 1 større end graphPointLimit

            if (taeller < dtoGUI_list.Count)
            {
                //Kode der får graferne vist:
	            ShowSecondOnXAxis();
	            Ymiddel.Add(dtoGUI_list[taeller].MiddelValue);
	            Ysystolic.Add(dtoGUI_list[taeller].SystoliskValue);
	            Ydiastolic.Add(dtoGUI_list[taeller].DiastoliskValue);
	            Ypulse.Add(dtoGUI_list[taeller].Pulse);

                //Kode der får værdierne vist i textboxene:
				middleBTValue_textbox.Text = Convert.ToString(dtoGUI_list[taeller].MiddelValue);
                pulseValue_textbox.Text = Convert.ToString(dtoGUI_list[taeller].Pulse);
                sysDiaValue_textbox.Text = dtoGUI_list[taeller].SystoliskValue + " / " + dtoGUI_list[taeller].DiastoliskValue;

                //Kode der sørger for at de ældste punkter på grafen bliver fjernet, når antal punkter har nået sin maks-grænse:
                if (taeller > graphPointLimit)
                {
	                Ymiddel.Remove(dtoGUI_list[taeller - removeFactor].MiddelValue);
	                Ysystolic.Remove(dtoGUI_list[taeller - removeFactor].SystoliskValue);
	                Ydiastolic.Remove(dtoGUI_list[taeller - removeFactor].DiastoliskValue);
	                Ypulse.Remove(dtoGUI_list[taeller - removeFactor].Pulse);
	                XdateTime.Remove(dtoGUI_list[taeller - removeFactor].CurrentSecond);
                }
                
                Alarm();
				taeller++;
            }
        }


        private void saveChanges_button_Click(object sender, RoutedEventArgs e)
        {
	        middelMax = Convert.ToInt32(middleBTMax_textbox.Text);
	        middelMin = Convert.ToInt32(middleBTMin_textbox.Text);
		}
        private void startMeasurement_button_Click(object sender, RoutedEventArgs e)
        {
            //Når der trykkes på "Start Måling" så går timeren i gang. Den udfører det den er implementeret til med det interval den er sat til at gøre det med
            dispatcherTimer.Start();
        }        
        private void Alarm()
        {
            if (middelMax < Convert.ToInt32(middleBTValue_textbox.Text) ||
                Convert.ToInt32(middleBTValue_textbox.Text) < middelMin)
            {
                middleBTValue_textbox.Foreground = Brushes.Red;
                alarmTriggeredTimes.Add(DateTime.Now);
            }
            else
            {
                middleBTValue_textbox.Foreground = Brushes.Black;
            }
        }
        private void stopAndSave_button_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop();
            //TODO: StartTime og StopTime står nu til DateTime.Now, men dem skal vi senere have fra rpi ligesom sys, dia osv.
            stopAndSaveObj.SaveMeasurement(dtoGUI_list, cpr_textbox.Text, DateTime.Now, DateTime.Now, alarmTriggeredTimes);

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
        public void ShowSecondOnXAxis()
        {
	        List<string> dateTime = mesControlObj.GetDateTime();
	        foreach (var item in dateTime)
	        {
		        XdateTime.Add(item);
	        }
	        DataContext = this;

			/*List<double> middelvalue = mesControlObj.GetMiddelValue();
            foreach (var item in middelvalue)
            {
                Ymiddel.Add(item); //Add middelvalues fra getMiddelVlaue()
            }
            DataContext = this;

			List<double> systolic = mesControlObj.GetSystolic();
            foreach (var item in systolic)
            {
                Ysystolic.Add(item);
            }

            DataContext = this;

            List<double> diastolic = mesControlObj.GetDiastolic();
            foreach (var item in diastolic)
            {
                Ydiastolic.Add(item);
            }

            DataContext = this;

            List<double> pulse = mesControlObj.GetPulse();
            foreach (double item in pulse)
            {
                Ypulse.Add(item);
            }
            DataContext = this;*/
		}
    }
}

