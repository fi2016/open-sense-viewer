using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    class API
    {
        StreamWriter sw;
        StreamReader sr;

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://apps.mikolai-sebastian.de/api/v1/open_sense_viewer/auth");

            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (sw = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"platform\":\"c9e5eb90-6c15-11e8-b35f-b0e87cb20b1d\", \"username\":\"admin\", \"password\":\"fi2016#\"}";

                sw.Write(json);
                sw.Flush();
                sw.Close();
            }

            HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (sr = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                Console.WriteLine(result);
            }
            Console.ReadLine();
    }
}
