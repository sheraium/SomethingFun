using System.Threading;

namespace WindowsFormsApp1.Service
{
    public class SomethingService
    {
        public SomethingService()
        {
        }

        public void SomethingVeryLong()
        {
            Thread.Sleep(10_000);
        }

        public void SomethingVeryImportant()
        {
            Thread.Sleep(5000);
        }
    }
}