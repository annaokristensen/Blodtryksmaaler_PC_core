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

namespace Presentation_Layer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private StopAndSave stopAndSaveObj;
        private BlockingCollection<Datacontainer> controllers;
        private System.Windows.Threading.DispatcherTimer dispatcherTimer;
        int counter = 1;
        private int rawDataCounter = 0;
        private int middelMax = 0;
        private int middelMin = 0 ;
        SoundPlayer player = new SoundPlayer();
        string file = "Cardiac alarm.wav";
        public string cpr { get; set; }
        private List<BPMesDataGUI_DTO> dtoGUI_list;
        private List<BPMesDataGUI_DTO> testDtoGUI_list;
        private BPMesDataGUI_DTO bpGUIDTOObj;
        private List<DateTime> alarmTriggeredTimes;
        public ChartValues<double> YRawData { get; set; }
        private DateTime startTime;
        private DateTime stopTime;
        private List<double> rawDataListGUI;
        private List<double> testRawDataListGUI;
        private List<double>[] RawDataArray;
		private MeasurementControlPC mesControlPC;
        private bool dataIsSaved = false;



        public MainWindow()
        {
            InitializeComponent();
     
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            stopAndSaveObj = new StopAndSave();
            alarmTriggeredTimes = new List<DateTime>();
            //XdateTime = new ChartValues<string>();
            YRawData = new ChartValues<double>();

            controllers = new BlockingCollection<Datacontainer>();

            //MeasurementDataAccess(); <- bruges ved UDP 
            mesControlPC = new MeasurementControlPC(controllers);

            //testDtoGUI_list = new List<BPMesDataGUI_DTO>();
            dtoGUI_list = new List<BPMesDataGUI_DTO>();
            testRawDataListGUI = new List<double>();
            rawDataListGUI = new List<double>();
            RawDataArray = new List<double>[5];
            DataContext = this;
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
            finishOperation_button.IsEnabled = false;

            //Sørger for at patientens (det indtastede) cpr-nummer fremstår af cpr-tekstboksen på mainWindow
            cpr_textbox.Text = cprWindowObj.GetEnteredCpr();

            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 1); //Intervallet for hvor ofte data skifter på GUI'en
					
            //De to variable bruges nede i saveChanges_button_click. De sættes her i starten til de default værdier der står i textboxene.
			middelMax = Convert.ToInt32(middleBTMax_textbox.Text);
			middelMin = Convert.ToInt32(middleBTMin_textbox.Text);

            RawDataArray = ZeroStart();

            

            //Thread Start


        }
        private void DispatcherTimer_Tick(object? sender, EventArgs e)
        {
            //TIL TEST:
            dtoGUI_list = mesControlPC.ReadValues();
            
            // rawDataCounter = dtoGUI_list[counter].RawDataList.Count;

            // BPMesDataGUI_DTO bpGUIdto = mesControlPC.ReadValues();
            //mesControlPC.ReadValues();

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

            //if(counter>=5)
            //{
            //    YRawData.AddRange(dtoGUI_list[dtoGUI_list.Count-1].RawDataList);
            //    YRawData.AddRange(dtoGUI_list[dtoGUI_list.Count - 2].RawDataList);
            //    YRawData.AddRange(dtoGUI_list[dtoGUI_list.Count - 3].RawDataList);
            //    YRawData.AddRange(dtoGUI_list[dtoGUI_list.Count - 4].RawDataList);
            //    YRawData.AddRange(dtoGUI_list[dtoGUI_list.Count - 5].RawDataList);
            //}
            //else if(counter==4)
            //{
            //    YRawData.AddRange(dtoGUI_list[dtoGUI_list.Count-1].RawDataList);
            //    YRawData.AddRange(dtoGUI_list[dtoGUI_list.Count - 2].RawDataList);
            //    YRawData.AddRange(dtoGUI_list[dtoGUI_list.Count - 3].RawDataList);
            //    YRawData.AddRange(dtoGUI_list[dtoGUI_list.Count - 4].RawDataList);
            //}
            //else if (counter == 3)
            //{
            //    YRawData.AddRange(dtoGUI_list[dtoGUI_list.Count-1].RawDataList);
            //    YRawData.AddRange(dtoGUI_list[dtoGUI_list.Count - 2].RawDataList);
            //    YRawData.AddRange(dtoGUI_list[dtoGUI_list.Count - 3].RawDataList);
            //}
            //else if (counter == 2)
            //{
            //    YRawData.AddRange(dtoGUI_list[dtoGUI_list.Count-1].RawDataList);
            //    YRawData.AddRange(dtoGUI_list[dtoGUI_list.Count - 2].RawDataList);
            //}
            //else
            //{
            //    YRawData.AddRange(dtoGUI_list[dtoGUI_list.Count-1].RawDataList);
            //}

            //middleBTValue_textbox.Text = Convert.ToString();
            //pulseValue_textbox.Text = Convert.ToString(counter);
            //sysDiaValue_textbox.Text = Convert.ToString(counter);

            //TEXTBOXENES VÆRDIER TIL UDP:
            middleBTValue_textbox.Text = Convert.ToString(dtoGUI_list[dtoGUI_list.Count-1].MiddelValue);
            pulseValue_textbox.Text = Convert.ToString(dtoGUI_list[dtoGUI_list.Count -1].Pulse);
            sysDiaValue_textbox.Text = dtoGUI_list[dtoGUI_list.Count -1].SystoliskValue + " / " + dtoGUI_list[dtoGUI_list.Count -1].DiastoliskValue;

            //Alarm();

            counter++;

            //testRawDataListGUI.AddRange(dtoGUI_list. RawDataList); //tilføjer det der ligger i dto til en lokal rawdatalist 

            //foreach (double rawData in dtoGUI_list[counter].RawDataList)
            //{
            // testRawDataListGUI.Add(rawData);
            //}

            //dtoGUI_list.Add(new BPMesDataGUI_DTO());
            /*dtoGUI_list.Add(bpGUIDTOObj);
            dtoGUI_list[taeller] = mesControlPC.BPDTO; // GetValues();
            testRawDataListGUI.AddRange(dtoGUI_list[taeller].RawDataList);
            testRawDataListGUI.AddRange(bpGUIDTOObj.RawDataList);*/

            ////TIL UDP:
            //dtoGUI_list[taeller] = mesControlPC.GetBPValues();
            //rawDataListGUI.AddRange(dtoGUI_list[taeller].RawDataList);

            ////TODO: Disse konstanter skal sættes meget længere op når vi modtager reel data
            //const int graphPointLimit = 3; //Grænsen for hvor mange punkter der bliver vist på graferne af gangen
            //const int removeFactor = graphPointLimit + 1; //Faktoren der sørger for de ældste punkter bliver fjernet. Skal vist være 1 større end graphPointLimit

            //if (counter < dtoGUI_list.Count)
            //{
	           // //TIL TEST:
	           // ////foreach (double rawTestData in testRawDataListGUI)
	           // ////{
		          // //// YRawData.Add(rawTestData);

            // ////       //Kode der sørger for at de ældste punkter på grafen bliver fjernet, når antal punkter har nået sin maks-grænse:
            // ////       if (counter > graphPointLimit)
            // ////       {
            // ////           //TIL TEST:
            // ////           //YRawData.Remove(dtoGUI_list[taeller - removeFactor].RawDataList);
            // ////           YRawData.Remove(testRawDataListGUI[counter - removeFactor]);

            // ////           //TIL UDP:
            // ////           //YRawData.Remove(dtoGUI_list[taeller - removeFactor].RawDataList);
            // ////       }
            // ////   }

	           // //TIL UDP:
	           // /*foreach (double rawData in rawDataListGUI)
            //    {
            //        YRawData.Add(rawData);
            //    }*/

	           // //TEXTBOXENES VÆRDIER TIL TEST:
	           // /*middleBTValue_textbox.Text = Convert.ToString(dtoGUI_list[taeller].MiddelValue);
	           // pulseValue_textbox.Text = Convert.ToString(dtoGUI_list[taeller].Pulse);
	           // sysDiaValue_textbox.Text =
		          //  dtoGUI_list[taeller].SystoliskValue + " / " + dtoGUI_list[taeller].DiastoliskValue;*/

	           // middleBTValue_textbox.Text = Convert.ToString(counter);
	           // pulseValue_textbox.Text = Convert.ToString(counter);
	           // sysDiaValue_textbox.Text = Convert.ToString(counter);

            //    //TEXTBOXENES VÆRDIER TIL UDP:
            //    //middleBTValue_textbox.Text = Convert.ToString(dtoGUI_list[taeller].MiddelValue);
            //    //pulseValue_textbox.Text = Convert.ToString(dtoGUI_list[taeller].Pulse);
            //    //sysDiaValue_textbox.Text = dtoGUI_list[taeller].SystoliskValue + " / " + dtoGUI_list[taeller].DiastoliskValue;

            //    //Alarm();
            //    counter++;
            //}
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
            Thread consumerThread = new Thread(mesControlPC.Run);

            consumerThread.Start();

            dispatcherTimer.Start();
            stopAndSave_button.IsEnabled = true;
            finishOperation_button.IsEnabled = true;
            startTime = DateTime.Now;
        }
        
        /// <summary>
        /// Alarm-metode som bliver kaldt hver gang tallene og grafen på GUI'en er blevet opdateret, så den holder øje med hver eneste opdatering
        /// </summary>
        //private void Alarm()
        //{
        //    if (middelMax < Convert.ToInt32(middleBTValue_textbox.Text) ||
        //        Convert.ToInt32(middleBTValue_textbox.Text) < middelMin)
        //    {
        //        middleBTValue_textbox.Foreground = Brushes.Red;
        //        middleBTValue_textbox.FontWeight = FontWeights.Bold;
        //        //For hver gang alarmen går i gang skal der gemmes et tidsstempel i en liste, så den liste af 'alarmtriggers' kan blive gemt i databasen
        //        alarmTriggeredTimes.Add(DateTime.Now);
        //        //var sri = Application.GetResourceStream(new Uri("Cardiac alarm.wav"));
        //        //if ((sri != null))
        //        //{   player.Load();
        //        //    player.Play();
        //        //}
        //        //player.
        //        string file = "Cardiac alarm.wav";

        //        //get the current assembly
        //        var assembly = System.Reflection.Assembly.GetExecutingAssembly();

        //        //load the embedded resource as a stream
        //        var stream = assembly.GetManifestResourceStream(string.Format("{0}.Resources.{1}", assembly.GetName().Name, file));

        //        //load the stream into the player
        //        var player = new System.Media.SoundPlayer(stream);

        //        //play the sound
        //        player.Play();

        //    }
        //    else
        //    {
        //        middleBTValue_textbox.Foreground = Brushes.White;
        //        middleBTValue_textbox.FontWeight = FontWeights.Normal;
        //    }
        //}

        private void stopAndSave_button_Click(object sender, RoutedEventArgs e)
        {
	        try
            {
	            //Når der trykkes "Stop og gem" så skal dispatcherTimer stoppe, så graferne og tallene stopper med at blive opdateret
	            dispatcherTimer.Stop();
	            stopTime = DateTime.Now;
				//Når der trykkes "Stop og gem" skal SaveMeasurement kaldes. Vi giver den dtoGUIlisten, cpr-nummeret og listen af alarm-tidspunkter med som parameter
				stopAndSaveObj.SaveMeasurement(dtoGUI_list, cpr_textbox.Text, startTime, stopTime, alarmTriggeredTimes);
				dataIsSaved = true;
				MessageBox.Show(this, "Data blev gemt i databasen", "Succes");
            }
            catch (Exception exception)
            {
	            MessageBox.Show(this,
		            "Data kunne ikke gemmes i databasen. Prøv venligst igen senere. Detaljer: " + exception.Message, "Fejl");
            }
        }
        public void FinishOperationMethod()
        {
            this.Close();
			MaintenanceWindow maintenanceWindowObj = new MaintenanceWindow();
			maintenanceWindowObj.ShowDialog();
		}
        private void finishOperation_button_Click(object sender, RoutedEventArgs e)
        {
	        if (dataIsSaved == true)
	        {
                FinishOperationMethod();
	        }
	        else
            {
	            if (MessageBox.Show("Er du sikker på du vil afslutte?", "Spørgsmål", MessageBoxButton.YesNo,
		                MessageBoxImage.Warning) == MessageBoxResult.Yes)
	            {
					FinishOperationMethod();
				}
	            else //hvis der trykkes nej, skal der ikke ske andet end at messagebox'en lukker (sker automatisk når man trykker)
	            {
		            
	            }
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

