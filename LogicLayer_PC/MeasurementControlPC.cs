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
        private BPMesDataGUI_DTO DTOObject;

        public MeasurementControlPC()
        {

	        mesDataAccessObj = new MeasurementDataAccess();
            DTOObject = new BPMesDataGUI_DTO();
            
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
	        List<BPMesDataGUI_DTO> dto_List = mesDataAccessObj.ReadSample();
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
        public List<BPMesDataGUI_DTO> GetAllValues()
        {
			List<BPMesDataGUI_DTO> dto_List = mesDataAccessObj.ReadSample();
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
        public List<string> GetDateTime()
        {
            List<string> dateTime_List = new List<string>();
            foreach (var item in mesDataAccessObj.ReadSample())
            {
                dateTime_List.Add(item.CurrentSecond);
            }
            return dateTime_List;
        }

        public List<double> GetSystolic()
        {
            List<double> systolic_list = new List<double>();
            foreach (var item in mesDataAccessObj.ReadSample())
            {
                systolic_list.Add(item.SystoliskValue);
            }

            return systolic_list;
        }
        public List<double> GetDiastolic()
        {
            List<double> diastolic_list = new List<double>();
            foreach (var item in mesDataAccessObj.ReadSample())
            {
                diastolic_list.Add(item.DiastoliskValue);
            }

            return diastolic_list;
        }
        public List<double> GetPulse()
        {
            List<double> pulse_list = new List<double>();
            foreach (var item in mesDataAccessObj.ReadSample())
            {
                pulse_list.Add(item.Pulse);
            }

            return pulse_list;
        }

    }
}