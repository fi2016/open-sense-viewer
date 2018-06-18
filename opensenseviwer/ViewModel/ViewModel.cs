using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ViewModel
{
    public class ViewModel
    {
        Model.Api api;
        public void login(String username, String password, String platform)
        {
            Console.WriteLine("HALLO");
            if(api.checkAuth(username, password))
            {

            }
            else
            {
                MessageBox.Show("Anmeldung fehlgeschlagen!");
            }
            savePlatform(platform);
        }
        private void savePlatform(String platform)
        {
            StreamWriter sw;
            sw = new StreamWriter("platform.json");
            sw.WriteLine(platform);
        }
    }
}
