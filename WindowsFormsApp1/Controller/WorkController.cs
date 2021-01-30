using System.Threading.Tasks;
using WindowsFormsApp1.Service;

namespace WindowsFormsApp1.Controller
{
    public class WorkController
    {
        private readonly SomethingService _somethingService;

        public WorkController(SomethingService somethingService)
        {
            _somethingService = somethingService;
        }

        public Task DoSomethingAsync(int second)
        {
            return Task.Run(() =>
            {
                if (second > 10)
                {
                    _somethingService.SomethingVeryLong();
                }

                _somethingService.SomethingVeryImportant();
            });
        }

        public void DoSomethingSync()
        {
            _somethingService.SomethingVeryImportant();
        }
    }
}