using DTO_PC;
using LogicLayer_PC;
using Presentation_Layer_PC;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using DataLayer_PC;
using System.Runtime.ConstrainedExecution;
using System.Windows.Documents;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Definitions.Charts;


namespace Presentation_Layer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
	    private TestMeasurementControlPC testMesControlObj;
        private StopAndSave stopAndSaveObj;
        private System.Windows.Threading.DispatcherTimer dispatcherTimer;
        int taeller = 0;
        private int middelMax = 0;
        private int middelMin = 0 ;
        public string cpr { get; set; }
        private List<BPMesDataGUI_DTO> dtoGUI_list;

        private List<DateTime> alarmTriggeredTimes;

        //Tilknyt data til livecharts graf
        //public ChartValues<string> XdateTime { get; set; }
        public  ChartValues<double> YRawData { get; set; }

        private DateTime startTime;
        private DateTime stopTime;

        private List<double> rawDataListGUI = new List<double>();
        private MeasurementControlPC mesControlPC;


		public MainWindow()
        {
            InitializeComponent();
            testMesControlObj = new TestMeasurementControlPC();
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            testMesControlObj = new TestMeasurementControlPC();
            stopAndSaveObj = new StopAndSave();
            alarmTriggeredTimes = new List<DateTime>();
            //XdateTime = new ChartValues<string>();
            YRawData = new ChartValues<double>();
            mesControlPC = new MeasurementControlPC();
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

            stopAndSave_button.IsEnabled = false;
            finishOperation_button.IsEnabled = false;

            //Sørger for at patientens (det indtastede) cpr-nummer fremstår af cpr-tekstboksen på mainWindow
            cpr_textbox.Text = cprWindowObj.GetEnteredCpr();

            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 250); //Intervallet for hvor ofte data skifter på GUI'en
					
            //De to variable bruges nede i saveChanges_button_click. De sættes her i starten til de default værdier der står i textboxene.
			middelMax = Convert.ToInt32(middleBTMax_textbox.Text);
			middelMin = Convert.ToInt32(middleBTMin_textbox.Text);

        }
        private void DispatcherTimer_Tick(object? sender, EventArgs e)
        {
            //TIL TEST-METODEN:
            //dtoGUI_list = testMesControlObj.GetAllValues();

            //TIL UDP:
            dtoGUI_list[taeller] = mesControlPC.GetValues();
            rawDataListGUI.AddRange(dtoGUI_list[taeller].RawDataList);


            //TODO: Disse konstanter skal sættes meget længere op når vi modtager reel data
            const int graphPointLimit = 8; //Grænsen for hvor mange punkter der bliver vist på graferne af gangen
            const int removeFactor = 9; //Faktoren der sørger for de ældste punkter bliver fjernet. Skal vist være 1 større end graphPointLimit

            if (taeller < dtoGUI_list.Count)
            {
                //TEST/SLETTES:
                //ShowSecondOnXAxis();
                //YRawData.Add(dtoGUI_list[taeller].MiddelValue);

                foreach (double rawData in rawDataListGUI)
                {
	                YRawData.Add(rawData);
                }

                //TEXTBOXENES VÆRDIER:
				middleBTValue_textbox.Text = Convert.ToString(dtoGUI_list[taeller].MiddelValue);
                pulseValue_textbox.Text = Convert.ToString(dtoGUI_list[taeller].Pulse);
                sysDiaValue_textbox.Text = dtoGUI_list[taeller].SystoliskValue + " / " + dtoGUI_list[taeller].DiastoliskValue;

                //Kode der sørger for at de ældste punkter på grafen bliver fjernet, når antal punkter har nået sin maks-grænse:
                if (taeller > graphPointLimit)
                {
	                YRawData.Remove(dtoGUI_list[taeller - removeFactor].SystoliskValue);
                }
                Alarm();

				taeller++;
            }
        }

		//Når vi trykker "Gem ændringer" efter at have ændret på grænseværdierne for middelværdi, skal de nye tal gemmes i variablerne middelMax og middelMin
        private void saveChanges_button_Click(object sender, RoutedEventArgs e)
        {
	        middelMax = Convert.ToInt32(middleBTMax_textbox.Text);
	        middelMin = Convert.ToInt32(middleBTMin_textbox.Text);
		}

        //Når der trykkes på "Start Måling" så går timeren i gang. Den udfører det den er implementeret til med det interval den er sat til at gøre det med
        private void startMeasurement_button_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Start();
            stopAndSave_button.IsEnabled = true;
            startTime = DateTime.Now;
        }
        
        /// <summary>
        /// Alarm-metode som bliver kaldt hver gang tallene og grafen på GUI'en er blevet opdateret, så den holder øje med hver eneste opdatering
        /// </summary>
        private void Alarm()
        {
            if (middelMax < Convert.ToInt32(middleBTValue_textbox.Text) ||
                Convert.ToInt32(middleBTValue_textbox.Text) < middelMin)
            {
                middleBTValue_textbox.Foreground = Brushes.Red;
                middleBTValue_textbox.FontWeight = FontWeights.Bold;
                //For hver gang alarmen går i gang skal der gemmes et tidsstempel i en liste, så den liste af 'alarmtriggers' kan blive gemt i databasen
                alarmTriggeredTimes.Add(DateTime.Now);
            }
            else
            {
                middleBTValue_textbox.Foreground = Brushes.White;
                middleBTValue_textbox.FontWeight = FontWeights.Normal;
            }
        }

        private void stopAndSave_button_Click(object sender, RoutedEventArgs e)
        {
	        try
            {
	            //Når der trykkes "Stop og gem" så skal dispatcherTimer stoppe, så graferne og tallene stopper med at blive opdateret
	            dispatcherTimer.Stop();
	            finishOperation_button.IsEnabled = true;
	            stopTime = DateTime.Now;
				//Når der trykkes "Stop og gem" skal SaveMeasurement kaldes. Vi giver den dtoGUIlisten, cpr-nummeret og listen af alarm-tidspunkter med som parameter
				stopAndSaveObj.SaveMeasurement(dtoGUI_list, cpr_textbox.Text, startTime, stopTime, alarmTriggeredTimes);
				MessageBox.Show(this, "Data blev gemt i databasen", "Succes");
            }
            catch (Exception exception)
            {
	            MessageBox.Show(this,
		            "Data kunne ikke gemmes i databasen. Prøv venligst igen senere. Detaljer: " + exception.Message, "Fejl");
            }
        }

        // Metoden sørger for at lave x-aksen hvor tidspunktet for målepunktet skal vises
		//TODO: Skal denne metode slettes?
        /*public void ShowSecondOnXAxis()
        {
	        List<string> dateTime = testMesControlObj.GetDateTime();
	        foreach (var item in dateTime)
	        {
		        XdateTime.Add(item);
	        }
	        DataContext = this; 
        }*/
    }
}

