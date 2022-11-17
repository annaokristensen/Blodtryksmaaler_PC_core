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
        //Test metode skal slettes
        public void TestThread()
        {
            int cnt = 10;
            while(cnt > 0)
            {
                Console.WriteLine("test");
                cnt--;
            }
 
        }

        /// <summary>
        /// Metoden kaldes fra mainWindow. Tager fat i ReadSample (på datalaget) og putter de værdier vi får derfra ind i en liste af dto_DB
        /// </summary>
        /// <returns></returns>
        public List<BPMesDataGUI_DTO> GetAllValues()
        {
	        List<BPMesDataGUI_DTO> dto_List = mesDataAccessObj.ReadSample();
			return dto_List;
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
    }
}