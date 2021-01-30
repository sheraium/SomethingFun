using System;
using System.Windows.Forms;
using WindowsFormsApp1.Controller;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private readonly SensorController _sensorController;
        private readonly WorkController _workController;

        public Form1(SensorController sensorController, WorkController workController)
        {
            InitializeComponent();
            _sensorController = sensorController;
            _workController = workController;
        }

        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            var sensorData = _sensorController.GetSensorData();

            textBoxTemperature.Text = $"{sensorData.Temperature:F1} C";
            textBoxHumidity.Text = $"{sensorData.Humidity:F1} %";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timerRefresh.Enabled = true;
        }

        private async void buttonAsync_Click(object sender, EventArgs e)
        {
            await _workController.DoSomethingAsync(5);
            textBoxAsync.Text = $"{DateTime.Now:HH:mm:ss}: 非同步工作完成";
        }

        private void buttonSync_Click(object sender, EventArgs e)
        {
            _workController.DoSomethingSync();
            textBoxSync.Text = $"{DateTime.Now:HH:mm:ss}: 同步工作完成";
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            new Form2().Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //userControl11.SetText("AAA");
        }
    }
}