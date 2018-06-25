using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using LiveCharts;

namespace ViewModel
{
    public class ViewModel
    {
        Model.Api api;

        public Boolean Login(String username, String password, String platform)
        {
            api = new Model.Api(platform);
            if (api.CheckAuth(username, password))
            {
                //SavePlatform(platform);
                return true;
            }
            else
            {
                MessageBox.Show("Anmeldung fehlgeschlagen!");
                //SavePlatform(platform);
                return false;
            }
        }
        private void SavePlatform(String platform)
        {
            StreamWriter sw;
            sw = new StreamWriter("platform.json");
            sw.WriteLine(platform);
        }

       public ChartValues<float> GetData(string sensor)
       {
            ChartValues<float> values = new ChartValues<float>();
            try
            {
                List<SensorData<Sensor>> temp = api.GetData(sensor);
                for (int i = 0; i < 2000; i++)
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
                MessageBox.Show("Es konnten keine Daten aus dem Sensor ausgelesen werden");
            }
            return values;
       }
    }
}
