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
        public bool IsCompleted { get; set; } = true;
        private IMeasurementDataAccess measurementDataAccessObj;
        private ICalbrationFileAcess calbrationFileAcess;
		public BPCalculator bpCalcObj { get; set; }
		public List<BPMesDataGUI_DTO> bpGUIlist;
		private ZeropointControlPC zeropointControl;

		public MeasurementControlPC(BlockingCollection<Datacontainer> RawDataBlocking, ZeropointControlPC zeropoint)
		{
            //Herunder slettes "Test" i objektoprettelsen, for at skifte til UDP
			measurementDataAccessObj = new TestMeasurementDataAccess(RawDataBlocking);
            this.RawDataBlocking = RawDataBlocking;
            bpCalcObj = new BPCalculator();
            bpGUIlist = new List<BPMesDataGUI_DTO>();
            calbrationFileAcess = new CalibrationFileAcess();
            zeropointControl = zeropoint;
        }
        public MeasurementControlPC(){}
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
            double calibration = calbrationFileAcess.ReadValue();

            while (IsCompleted == true)
            {
                try
                {
	                double calibrationValue = calbrationFileAcess.ReadValue();
	                var contanier = RawDataBlocking.Take();
                    List<double> RawData = contanier.GetRawDataList();
                    List<double> dataList = new List<double>();

                    foreach (double value in RawData)
                    {
                        var tmp = ((value - zeropointControl.Zeropoint) * calibrationValue);
                        dataList.Add(tmp);
                        Console.WriteLine(tmp);
                    }

                    bpGUIlist.Add(new BPMesDataGUI_DTO(dataList));
                    bpCalcObj.SaveValues(bpGUIlist[bpGUIlist.Count - 1]);

                }
                catch (InvalidOperationException)
                {
                    Console.WriteLine("Catch start");
                }
            }
        }
	}
}
