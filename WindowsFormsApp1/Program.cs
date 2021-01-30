using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Controller;
using WindowsFormsApp1.Service;

namespace WindowsFormsApp1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 參考物件
            var somethingService = new SomethingService();

            // Controller
            var sensorController = new SensorController();
            var workController = new WorkController(somethingService);

            Application.Run(new Form1(sensorController, workController));
        }
    }
}
