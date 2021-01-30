using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            var processStartInfo = new ProcessStartInfo("ConsoleApp1.exe");
            processStartInfo.WindowStyle = ProcessWindowStyle.Minimized;

            var process = Process.Start(processStartInfo);

            AppDomain.CurrentDomain.DomainUnload += (s, e) => { process.Kill(); process.WaitForExit(); };
            AppDomain.CurrentDomain.ProcessExit += (s, e) => { process.Kill(); process.WaitForExit(); };
            AppDomain.CurrentDomain.UnhandledException += (s, e) => { process.Kill(); process.WaitForExit(); };

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}