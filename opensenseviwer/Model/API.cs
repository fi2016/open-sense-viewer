using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Windows;

namespace Model
{
    public class Api
    {
        private HttpWebRequest webRequest;
        private HttpWebResponse webResponse;
        private StreamWriter writer;
        private StreamReader reader;
        private string platform;
        private string username;
        private string password;

        public string Username
        {
            get
            {
                return username;
            }

            set
            {
                username = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
            }
        }

        public string Platform
        {
            get
            {
                return platform;
            }

            set
            {
                platform = value;
            }
        }

        public Api(string platform)
        {
            Platform = platform;
        }

        public bool CheckAuth(string username, string password)
        {
            webRequest = (HttpWebRequest)WebRequest.Create("https://apps.mikolai-sebastian.de/api/v1/open_sense_viewer/auth");
            webRequest.ContentType = "application/json";
            webRequest.Method = "POST";
            writer = new StreamWriter(webRequest.GetRequestStream());
            string requestJson = "{\"platform\":\"" + Platform + "\", \"username\":\"" + username + "\", \"password\":\"" + password + "\"}";
            writer.Write(requestJson);
            writer.Flush();
            writer.Close();
            webResponse = (HttpWebResponse)webRequest.GetResponse();
            reader = new StreamReader(webResponse.GetResponseStream());
            string responseJson = reader.ReadToEnd();
            GenericApiResponse response = JsonConvert.DeserializeObject<GenericApiResponse>(responseJson);
            if (response.Status.Equals("success") && response.Id == 200 && response.Message.Equals("Authorized"))
            {
                Username = username;
                Password = password;
                return true;
            }
            return false;
        }

        public ProjectInfoApiResponse GetProjectInfo()
        {
            webRequest = (HttpWebRequest)WebRequest.Create("https://apps.mikolai-sebastian.de/api/v1/open_sense_viewer");
            webRequest.ContentType = "application/json";
            webRequest.Method = "GET";
            webResponse = (HttpWebResponse)webRequest.GetResponse();
            reader = new StreamReader(webResponse.GetResponseStream());
            string responseJson = reader.ReadToEnd();
            ProjectInfoApiResponse response = JsonConvert.DeserializeObject<ProjectInfoApiResponse>(responseJson);
            if (response.Status.Equals("success") && response.Id == 200)
            {
                Username = username;
                Password = password;
                return response;
            }
            return null;
        }

        public SensorsApiResponse GetAllSensors()
        {
            webRequest = (HttpWebRequest)WebRequest.Create("https://apps.mikolai-sebastian.de/api/v1/open_sense_viewer");
            webRequest.ContentType = "application/json";
            webRequest.Method = "GET";
            webResponse = (HttpWebResponse)webRequest.GetResponse();
            reader = new StreamReader(webResponse.GetResponseStream());
            string responseJson = reader.ReadToEnd();
            SensorsApiResponse response = JsonConvert.DeserializeObject<SensorsApiResponse>(responseJson);
            if (response.Status.Equals("success") && response.Id == 200)
            {
                Username = username;
                Password = password;
                return response;
            }
            return null;
        }

        public List<SensorData<Sensor>> GetData(string sensor)
        {
            try
            {
                    webRequest = (HttpWebRequest)WebRequest.Create("https://apps.mikolai-sebastian.de/api/v1/open_sense_viewer/data/get");
                    webRequest.ContentType = "application/json";
                    webRequest.Method = "POST";
                    Console.WriteLine(Username);
                    writer = new StreamWriter(webRequest.GetRequestStream());
                    string requestJson = "{\"platform\":\"" + Platform + "\", \"username\":\"" + Username + "\", \"password\":\"" + Password + "\", \"sensor\":\"" + sensor + "\"}";
                    Console.WriteLine(requestJson);
                    writer.Write(requestJson);
                    writer.Flush();
                    writer.Close();
                    webResponse = (HttpWebResponse)webRequest.GetResponse();
                    reader = new StreamReader(webResponse.GetResponseStream());
                    string responseJson = reader.ReadToEnd();
                    DataApiResponse response = JsonConvert.DeserializeObject<DataApiResponse>(responseJson);

                    if(response.Status.Equals("success") && response.Id == 200)
                    {
                        return response.Values;
                    }
                    else
                    {
                        MessageBox.Show("Fehler beim Anmelden");
                    }
            }
            catch(NullReferenceException)
            {
                MessageBox.Show("Es konnte keine verbindung mit dem Sensor aufgebaut werden");
            }
            return null;
        }
    }
}
