using System;
using System.Diagnostics;

namespace SubProcessStartExit
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //Process notePad = new Process();
            //// FileName 是要執行的檔案
            //notePad.StartInfo.FileName = "notepad.exe";
            //notePad.Start();

            var processStartInfo = new ProcessStartInfo("ConsoleApp1.exe");

            var process = Process.Start(processStartInfo);

            AppDomain.CurrentDomain.DomainUnload += (s, e) => { process.Kill(); process.WaitForExit(); };
            AppDomain.CurrentDomain.ProcessExit += (s, e) => { process.Kill(); process.WaitForExit(); };
            AppDomain.CurrentDomain.UnhandledException += (s, e) => { process.Kill(); process.WaitForExit(); };
            Console.ReadLine();
            //notePad.Kill(true);
        }
    }
}