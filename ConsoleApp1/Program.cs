using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                throw new ArgumentNullException("Test");
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e);
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            var subject = new Subject<string>();
            subject
                .Select(s => Observable.FromAsync(async () =>
                {
                    await Task.Delay(2000);
                    Console.WriteLine($"Subscribe: {s}, ThreadId: {Thread.CurrentThread.ManagedThreadId}");
                }))
                .Concat()
                .Subscribe();

            subject.OnNext("1");
            Console.WriteLine("Main: 1");
            subject.OnNext("2");
            Console.WriteLine("Main: 2");
            subject.OnNext("3");
            Console.WriteLine("Main: 3");

            Console.ReadKey();
        }
    }
}