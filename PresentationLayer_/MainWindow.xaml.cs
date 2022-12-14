using DTO_PC;
using LogicLayer_PC;
using Presentation_Layer_PC;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.IO.Compression;
using System.Media;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Runtime.ConstrainedExecution;
using System.Windows.Documents;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Definitions.Charts;
using System.Numerics;
using System.Printing;
using System.Threading;
using NetCoreAudio;

namespace Presentation_Layer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private StopAndSave stopAndSaveObj;
        private System.Windows.Threading.DispatcherTimer dispatcherTimer;
        private int middelMax = 0;
        private int middelMin = 0 ;
        private List<BPMesDataGUI_DTO> dtoGUI_list;
        private List<DateTime> alarmTriggeredTimes;
        public ChartValues<double> YRawData { get; set; }
        private DateTime startTime;
        private DateTime stopTime;
        private List<double>[] RawDataArray;
		private MeasurementControlPC mesControlPC;
        private bool dataIsSaved = false;
        private bool measurementIsStarted = false;
        private double middelTemp = 0;
        private double pulseTemp = 0;
        private double systolicTemp = 0;
        private double diastolicTemp = 0;
		private double middelRounded = 0;
        private double pulseRounded = 0;
        private double systolicRounded = 0;
        private double diastolicRounded = 0;
        private BlockingCollection<Datacontainer> blocking;
        private ZeropointControlPC ZeropointController;
        private Thread consumerThread;

        public MainWindow()
        {
            InitializeComponent();

            ZeropointController = new ZeropointControlPC();
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            stopAndSaveObj = new StopAndSave();
            alarmTriggeredTimes = new List<DateTime>();
            YRawData = new ChartValues<double>();
            blocking = new BlockingCollection<Datacontainer>();
            //controllers = new BlockingCollection<Datacontainer>();
            //MeasurementDataAccess(); <- bruges ved UDP 
            mesControlPC = new MeasurementControlPC(blocking, ZeropointController);

            //testDtoGUI_list = new List<BPMesDataGUI_DTO>();
            dtoGUI_list = new List<BPMesDataGUI_DTO>();
            RawDataArray = new List<double>[5];
            DataContext = this;
	        consumerThread= new Thread(mesControlPC.Run);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
	        dataIsSaved = false;
            this.Hide();
            CPR_Window cprWindowObj = new CPR_Window();

            //Sørger for at cpr-vinduet åbner og at koden for mainwindow ikke kører videre før det er lukket
            if (!cprWindowObj.ShowDialog().Value)
                this.Close();
            else
                this.Show();

            stopAndSave_button.IsEnabled = false;

            //Sørger for at patientens (det indtastede) cpr-nummer fremstår af cpr-tekstboksen på mainWindow
            cpr_textbox.Text = cprWindowObj.GetEnteredCpr();
            pulseValue_textbox.Text = "0";
            sysDiaValue_textbox.Text = "0";
            middleBTValue_textbox.Text = "0";

            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 1); //Intervallet for hvor ofte data skifter på GUI'en
					
            //De to variable bruges nede i saveChanges_button_click. De sættes her i starten til de default værdier der står i textboxene.
			middelMax = Convert.ToInt32(middleBTMax_textbox.Text);
			middelMin = Convert.ToInt32(middleBTMin_textbox.Text);

            RawDataArray = ZeroStart();
        }
        private void DispatcherTimer_Tick(object? sender, EventArgs e)
        {
            dtoGUI_list = mesControlPC.ReadValues();

            while (dtoGUI_list.Count == 0) if (dtoGUI_list.Count > 0) break;
            RawDataArray[4] = RawDataArray[3];
            RawDataArray[3] = RawDataArray[2];
            RawDataArray[2] = RawDataArray[1];
            RawDataArray[1] = RawDataArray[0];
            RawDataArray[0] = dtoGUI_list[dtoGUI_list.Count-1].RawDataList;

            YRawData.Clear();
            
            for(int i = 0; i < 5; i++)
            {
                YRawData.AddRange(RawDataArray[i]);
            }
            //Affrunding af textboxenes værdier:
            middelTemp = dtoGUI_list[dtoGUI_list.Count - 1].MiddelValue;
            middelRounded = Math.Round(middelTemp, 0);
            pulseTemp = dtoGUI_list[dtoGUI_list.Count - 1].Pulse;
            pulseRounded = Math.Round(pulseTemp, 0);
			systolicTemp = dtoGUI_list[dtoGUI_list.Count - 1].SystoliskValue;
            systolicRounded = Math.Round(systolicTemp, 0);
            diastolicTemp = dtoGUI_list[dtoGUI_list.Count - 1].DiastoliskValue;
            diastolicRounded = Math.Round(diastolicTemp, 0);

			middleBTValue_textbox.Text = middelRounded.ToString();
            pulseValue_textbox.Text = pulseRounded.ToString();
            sysDiaValue_textbox.Text = systolicRounded + " / " + diastolicRounded;

            Alarm();
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
            mesControlPC.StartProducerThread();
            consumerThread.Start();
            dispatcherTimer.Start();
            stopAndSave_button.IsEnabled = true;
            finishOperation_button.IsEnabled = true;
            measurementIsStarted = true;
            startTime = DateTime.Now;
        }

        /// <summary>
        /// Alarm-metode som bliver kaldt hver gang tallene og grafen på GUI'en er blevet opdateret, så den holder øje med hver eneste opdatering
        /// </summary>
        private void Alarm()
        {
            if (middelMax < Convert.ToInt64(middleBTValue_textbox.Text) ||
                Convert.ToInt64(middleBTValue_textbox.Text) < middelMin)
            {
                middleBTValue_textbox.Foreground = Brushes.Red;
                middleBTValue_textbox.FontWeight = FontWeights.Bold;
                //For hver gang alarmen går i gang skal der gemmes et tidsstempel i en liste, så den liste af 'alarmtriggers' kan blive gemt i databasen
                alarmTriggeredTimes.Add(DateTime.Now);

				string file = "Cardiac alarm.wav";
				SoundPlayer player = new SoundPlayer(file);
				player.Play();
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
	            mesControlPC.IsCompleted = false;
	            consumerThread.Join();
	            dispatcherTimer.Stop();
				startMeasurement_button.IsEnabled = false;
				stopAndSave_button.IsEnabled = false;
	            stopTime = DateTime.Now;
	            stopAndSaveObj.SaveMeasurement(dtoGUI_list, cpr_textbox.Text, startTime, stopTime, alarmTriggeredTimes);
				dataIsSaved = true;
				MessageBox.Show(this, "Data blev gemt i databasen", "Succes");
            }
            catch (Exception exception)
            {
	            MessageBox.Show(this,
		            "Data kunne ikke gemmes i databasen. Tilkald tekniker. Detaljer: " + exception.Message, "Fejl");
            }
        }

        private void finishOperation_button_Click(object sender, RoutedEventArgs e)
        {
	        if (!measurementIsStarted && !dataIsSaved)
	        {
		        if (MessageBox.Show("Er du sikker på du vil afslutte? Ingen måling foretaget.", "Vent", MessageBoxButton.YesNo,
			            MessageBoxImage.Warning) == MessageBoxResult.Yes)
		        {
			        this.Close();
		        }
			}
	        else if(measurementIsStarted && !dataIsSaved)
            {
	            if (MessageBox.Show("Er du sikker på du vil afslutte? Målingen er ikke gemt.", "Vent", MessageBoxButton.YesNo,
		                MessageBoxImage.Warning) == MessageBoxResult.Yes)
	            {
		            this.Close();
	            }
            }
	        else
	        {
		        this.Close();
	        }
		}

        private List<double>[] ZeroStart()
        {
            List<double> zerolist = new List<double>();
            List<double>[] startarray = new List<double>[5];

            for(int i = 0; i < 200; i++)
            {
                zerolist.Add(0);
            }
            for(int i=0;i<5;i++)
            {
                startarray[i] = zerolist;
            }
            return startarray;
        }
    }
}

