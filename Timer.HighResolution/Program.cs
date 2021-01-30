using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Timer.HighResolution
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var swElapsed = Spin(1);
            Console.WriteLine(swElapsed);
            swElapsed = Spin(1);
            Console.WriteLine(swElapsed);
            swElapsed = Spin(1);
            Console.WriteLine(swElapsed);
            swElapsed = Spin(1);
            Console.WriteLine(swElapsed);
            swElapsed = Spin(1);
            Console.WriteLine(swElapsed);
        }

        //SpinWait.SpinUntil(() => false, TimeSpan.FromMilliseconds(iterations));
        //SpinWait.SpinUntil(() => false, 0);
        private static string Spin(int iterations)
        {
            var isHighResolution = Stopwatch.IsHighResolution;
            var sw = new Stopwatch();

            sw.Start();
            while (sw.Elapsed.TotalMilliseconds * 1000 < iterations * 100 * 1)
            {
                Thread.SpinWait(1);
            }

            sw.Stop();
            var swElapsed = sw.Elapsed.TotalMilliseconds * 1000 + "us";
            return swElapsed;
        }
    }
}