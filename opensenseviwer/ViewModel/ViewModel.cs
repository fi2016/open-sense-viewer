using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using LiveCharts;

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

       public ChartValues<float> GetData(string sensor)
       {
            ChartValues<float> values = new ChartValues<float>();
            try
            {
                List<SensorData<Sensor>> temp = api.GetData(sensor);
                for (int i = 0; i < 1500; i++)
                {
                    float f = temp[i].Value;
                    if (f > 4)
                    {
                        values.Add(f);
                    }
                }
            }
            catch(NullReferenceException)
            {
                MessageBox.Show("Es konnten keine Daten aus dem Sensor ausgelesen werden!", "Open Sense Viewer", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return values;
       }

       public string getSensors()
       {
            return api.GetSensor();
       }

        public ProjectInfoApiResponse GetProjectInfo()
        {
            return api.GetProjectInfo();
        }
    }
}
