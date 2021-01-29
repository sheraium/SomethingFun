using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace RxNet.Interval
{
    internal class Program
    {
        private static void Main(string[] args)
        {
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
    }

    public class Target : IDisposable
    {
        private IDisposable _disposable;

        public Target()
        {
            _disposable = Observable
                .Interval(TimeSpan.FromMilliseconds(1_000))
                .ObserveOn(TaskPoolScheduler.Default.DisableOptimizations(typeof(ISchedulerLongRunning)))
                .Subscribe(l =>
                {
                    Console.WriteLine(l);
                });
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}