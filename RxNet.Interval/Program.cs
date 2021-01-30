using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using System.Threading.Tasks;

namespace RxNet.Interval
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Observable
                .Interval(TimeSpan.FromMilliseconds(1))
                .Select(l => Observable.FromAsync(() => GetInt(l)))
                .Concat()
                .Subscribe();
            Console.ReadLine();

            var disposables = new List<IDisposable>();
            foreach (var i in Enumerable.Range(1, 10))
            {
                var target = new Target();
                disposables.Add(target);
            }

            Console.WriteLine("Create OK");
            Console.ReadLine();
            disposables.ForEach(disposable => disposable.Dispose());

            Console.WriteLine("Wait Exit");
            Console.ReadLine();
        }

        private static async Task GetInt(long value)
        {
            Console.WriteLine(value);
            Console.WriteLine(DateTime.Now.ToString("hh:mm:ss.fff"));
            await Task.Delay(2000);
            //return default;
            //SpinWait.SpinUntil(() => false, 2000);
        }
    }

    public class Target : IDisposable
    {
        private IDisposable _disposable;

        public Target()
        {
            _disposable = Observable
                .Interval(TimeSpan.FromMilliseconds(1_000))
                .ObserveOn(TaskPoolScheduler.Default.DisableOptimizations(typeof(ISchedulerLongRunning)))
                .Subscribe(l => { Console.WriteLine(l); });
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}