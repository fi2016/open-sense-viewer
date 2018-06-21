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
        
        public Boolean login(String username, String password, String platform)
        {
            Model.Api api = new Model.Api(platform);
            Console.WriteLine("HALLO");
            if (api.checkAuth(username, password))
            {
                Console.WriteLine("Ich wurde angemeldet");
                savePlatform(platform);
                return true;
            }
            else
            {
                MessageBox.Show("Anmeldung fehlgeschlagen!");
                savePlatform(platform);
                return false;
            }
        }
        private void savePlatform(String platform)
        {
            StreamWriter sw;
            sw = new StreamWriter("platform.json");
            sw.WriteLine(platform);
        }
    }
}
