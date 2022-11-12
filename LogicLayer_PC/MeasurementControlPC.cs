using DataLayer_PC;
using DTO_PC;

namespace LogicLayer_PC
{
    /// <summary>
    /// Sørger for at målingen startes og vises på præsentationslaget
    /// </summary>
    public class MeasurementControlPC
    {
        private MeasurementDataAccess mesDataAccessObj;
        private BPMeasurementData_DTO DTOObject;

        public MeasurementControlPC()
        {

	        mesDataAccessObj = new MeasurementDataAccess();
            DTOObject = new BPMeasurementData_DTO();
            
        }
        public void GetSamplesList()
        {
            foreach (var item in mesDataAccessObj.ReadSample())
            {
                Console.WriteLine(item.Pulse + " " + item.SystoliskValue + " " + item.DiastoliskValue + " " + item.MiddelValue);
            }
        }

        public double GetMiddelValueTest()
        {
	        List<BPMeasurementData_DTO> dto_List = mesDataAccessObj.ReadSample();
            // TODO FIX return dto_List;
            return 0.0;
        }

        public void TestThread()
        {
            int cnt = 10;
            while(cnt > 0)
            {
                Console.WriteLine("test");
                cnt--;
            }
 
        }
        public List<BPMeasurementData_DTO> GetAllValues()
        {
			List<BPMeasurementData_DTO> dto_List = mesDataAccessObj.ReadSample();
			return dto_List;
		}
        public List<double> GetMiddelValue()
        {
            List<double> middelValue_List = new List<double>(); //hvis vi kun får en værdi så skal vi bare slette listen og kun skrive double 
            foreach (var item in mesDataAccessObj.ReadSample())
            {
                middelValue_List.Add(item.MiddelValue);
            }
            return middelValue_List;
        }
    }
}