using DataLayer_PC;

namespace LogicLayer_PC
{
    /// <summary>
    /// Sørger for at målingen startes og vises på præsentationslaget
    /// </summary>
    public class MeasurementControlPC
    {
        private MeasurementDataAccess measurementDataAccessObject;

        public MeasurementControlPC()
        {
            
            measurementDataAccessObject = new MeasurementDataAccess();
            
        }
        public void getSamplesList()
        {
            foreach (var item in measurementDataAccessObject.ReadEksempel())
            {
                Console.WriteLine(item.Sekunder +" "+ item.Værdi);
            }
            
          
        }


    }
}