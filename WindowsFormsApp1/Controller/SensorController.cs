using System;
using WindowsFormsApp1.ViewModel;

namespace WindowsFormsApp1.Controller
{
    public class SensorController
    {
        public SensorData GetSensorData()
        {
            var sensorData = new SensorData();
            var random = new Random(Guid.NewGuid().GetHashCode());
            sensorData.Temperature = random.NextDouble() * 30;
            sensorData.Humidity = random.NextDouble() * 80;
            return sensorData;
        }
    }
}