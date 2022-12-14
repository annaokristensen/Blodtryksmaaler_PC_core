using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DataLayer_PC;
using DTO_PC;

namespace LogicLayer_PC
{
	public class MeasurementControlPC : IMeasurementControlPC
	{
        private readonly BlockingCollection<Datacontainer> RawDataBlocking;
        private IMeasurementDataAccess measurementDataAccessObj;
        private IMeasurementDataAccess measurementDataAccessObj2;
        private ICalbrationFileAcess calbrationFileAcess;
		public BPCalculator bpCalcObj { get; set; }
		public BPMesDataGUI_DTO BPDTO { get; set; }
		//private BPMesDataGUI_DTO calcValuesDTO;
		public List<double> rawDataListMC = new List<double>();
		public List<BPMesDataGUI_DTO> bpGUIlist;
		private ZeropointControlPC zeropointControl;
        public double Zero { get; set; }
        //private BPMesDataGUI_DTO bpGUIDTO;
        private Server udp;

        public MeasurementControlPC(BlockingCollection<Datacontainer> RawDataBlocking, ZeropointControlPC zeropoint)
		{
            udp = new Server();

            measurementDataAccessObj = new TestMeasurementDataAccess(RawDataBlocking);
            this.RawDataBlocking = RawDataBlocking;

			bpCalcObj = new BPCalculator();
            bpGUIlist = new List<BPMesDataGUI_DTO>();
            calbrationFileAcess = new CalibrationFileAcess();
            zeropointControl = zeropoint;
        }
        public MeasurementControlPC(IMeasurementDataAccess ImeasurementDataAccess)
        {
            bpCalcObj = new BPCalculator();
            bpGUIlist = new List<BPMesDataGUI_DTO>();
            calbrationFileAcess = new CalibrationFileAcess();
            zeropointControl = new ZeropointControlPC();
        }
       
        public void StartProducerThread()
        {
            Thread producerThread = new Thread(measurementDataAccessObj.ReadSample);          
            producerThread.Start();
        }
        public List<BPMesDataGUI_DTO> ReadValues()
        {
            return bpGUIlist;
        }
        public void Run()
        {
            while (!RawDataBlocking.IsCompleted)
            {
                try
                {
                    var contanier = RawDataBlocking.Take();
                    List<double> RawData = contanier.GetRawDataList();
                    List<double> dataList = new List<double>();

                    foreach (double value in RawData)
                    {
                        var tmp = (value - zeropointControl.Zeropoint * calbrationFileAcess.ReadValue());
                        dataList.Add(tmp);
                        Console.WriteLine(tmp);
                    }

                    bpGUIlist.Add(new BPMesDataGUI_DTO(dataList));
                    bpCalcObj.saveValues(bpGUIlist[bpGUIlist.Count - 1]);

                }
                catch (InvalidOperationException)
                {
                    Console.WriteLine("Catch start");
                }
            }
        }

      

    }
}
