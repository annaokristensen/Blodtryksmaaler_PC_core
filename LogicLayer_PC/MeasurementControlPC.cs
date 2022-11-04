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
                Console.WriteLine(item.Second + " " + item.SampleValue + " " + item.MiddelValue);
            }
        }

        public double GetMiddelValueTest()
        {
	        mesDataAccessObj.ReadSample();
	        double middelValue = DTOObject.MiddelValue;
	        return middelValue;
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

    }
}