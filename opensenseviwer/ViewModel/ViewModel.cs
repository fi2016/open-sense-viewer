using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using LiveCharts;
using System.Linq;

namespace ViewModel
{
    public class ViewModel
    {
        private Api api;

        public Boolean Login(String username, String password, String platform)
        {
            api = new Api(platform);
            if (api.CheckAuth(username, password))
            {
                SaveConfig(platform, username);
                return true;
            }
            else
            {
                MessageBox.Show("Anmeldung fehlgeschlagen!", "Open Sense Viewer", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        public Config LoadConfig()
        {
            if (File.Exists("config.json"))
            {
                StreamReader reader = new StreamReader("config.json");
                string json = reader.ReadToEnd();
                Config config = JsonConvert.DeserializeObject<Config>(json);
                return config;
            }
            return null;
        }

        private void SaveConfig(string platform, string username)
        {
            StreamWriter writer = new StreamWriter("config.json", false);
            writer.WriteLine("{\"platform\":\"" + platform + "\", \"username\":\"" + username + "\"}");
            writer.Flush();
            writer.Close();
        }

        public ChartValues<float> GetData(string sensor, string groupBy)
        {
            Console.WriteLine(groupBy);
            ChartValues<float> chartValues = new ChartValues<float>();
            List<SensorData<Sensor>> sensorData = api.GetData(sensor);
            if (sensorData != null)
            {
                List<SensorData<Sensor>> groupedSensorData = sensorData.GroupBy(data =>
                {
                    DateTime dateTime = data.Time;
                    if (groupBy.Equals("Stunden"))
                    {
                        dateTime = dateTime.AddMinutes(-(dateTime.Minute % 60));
                        dateTime = dateTime.AddHours(-(dateTime.Hour % 1));
                    }
                    else if (groupBy.Equals("6 Stunden"))
                    {
                        dateTime = dateTime.AddMinutes(-(dateTime.Minute % 60));
                        dateTime = dateTime.AddHours(-(dateTime.Hour % 6));
                    }
                    else if (groupBy.Equals("12 Stunden"))
                    {
                        dateTime = dateTime.AddMinutes(-(dateTime.Minute % 60));
                        dateTime = dateTime.AddHours(-(dateTime.Hour % 12));
                    }
                    else if (groupBy.Equals("Tage"))
                    {
                        dateTime = dateTime.AddMinutes(-(dateTime.Minute % 60));
                        dateTime = dateTime.AddHours(-(dateTime.Hour % 24));
                    }
                    else
                    {
                        dateTime = dateTime.AddMinutes(-(dateTime.Minute % 5));
                    }
                    dateTime = dateTime.AddMilliseconds(-dateTime.Millisecond - 1000 * dateTime.Second);
                    return dateTime;
                }).Select(groupedData => new SensorData<Sensor>(groupedData.Key, groupedData.Average(groupedAverageData => groupedAverageData.Value))).ToList();
                foreach (SensorData<Sensor> data in groupedSensorData)
                {
                    chartValues.Add(data.Value);
                }
            }
            return chartValues;
        }

        public List<Sensor> GetAllSensors()
        {
            return api.GetAllSensors();
        }

        public ProjectInfoApiResponse GetProjectInfo()
        {
            return api.GetProjectInfo();
        }
    }
}
