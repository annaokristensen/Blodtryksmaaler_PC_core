﻿using DataLayer_PC;
using DTO_PC;

namespace LogicLayer_PC
{
    /// <summary>
    /// Sørger for at målingen startes og vises på præsentationslaget
    /// </summary>
    public class TestMeasurementControlPC : IMeasurementControlPC
    {
	    private TestMeasurementDataAccess testMesDataAccessObj;
        private BPMesDataGUI_DTO DTOObject;

        public TestMeasurementControlPC()
        {

	        testMesDataAccessObj = new TestMeasurementDataAccess();
            DTOObject = new BPMesDataGUI_DTO();
            
        }
        /*public void GetSamplesList()
        {
            
            foreach (var item in testMesDataAccessObj.ReadSampleTest())
            {
                Console.WriteLine(testMesDataAccessObj.ReadSampleTest().Pulse + " " + item.SystoliskValue + " " + item.DiastoliskValue + " " + item.MiddelValue);
            }
        }*/

        public BPMesDataGUI_DTO GetValues()
        {
            BPMesDataGUI_DTO dtoObj = testMesDataAccessObj.ReadSample();

            return dtoObj;
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
        /*public List<BPMesDataGUI_DTO> GetAllValues()
        {
	        List<BPMesDataGUI_DTO> dto_List = testMesDataAccessObj.ReadSampleTest();
			return dto_List;
		}*/

        public List<string> GetDateTime()
        {
            List<string> dateTime_List = new List<string>();
            //foreach (var item in mesDataAccessObj.ReadSampleTest())
            //{
            //    dateTime_List.Add(item.CurrentSecond);
            //}
            return dateTime_List;
        }
    }
}