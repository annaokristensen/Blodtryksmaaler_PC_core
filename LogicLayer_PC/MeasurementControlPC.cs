using DataLayer_PC;
using DTO_PC;

namespace LogicLayer_PC
{
    /// <summary>
    /// Sørger for at målingen startes og vises på præsentationslaget
    /// </summary>
    public class MeasurementControlPC
    {
        private MeasurementDataAccess measurementDataAccessObject;
        private BPMeasurementData_DTO DTOObject;

        public MeasurementControlPC()
        {
            
            measurementDataAccessObject = new MeasurementDataAccess();
            DTOObject = new BPMeasurementData_DTO();
            
        }
        public void getSamplesList()
        {
            foreach (var item in measurementDataAccessObject.ReadSample())
            {
                Console.WriteLine(item.second + " " + item.sampleValue + " " + item.middelValue);
            }
        }
        public void testThread()
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