using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace Model
{
    public class Api
    {
        private HttpWebRequest webRequest;
        private HttpWebResponse webResponse;
        private StreamWriter writer;
        private StreamReader reader;
        private string platform;

        private string Platform
        {
            get { return platform; }
            set { platform = value; }
        }

        public Api(string platform)
        {
            Platform = platform;
        }

        public bool checkAuth(string username, string password)
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
                return true;
            }
            return false;
        }

        public void read()
        {

        }
    }
}
