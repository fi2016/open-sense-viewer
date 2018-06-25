using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Sensor
    {
        private string uuid;
        private string name;

        public Sensor(string uuid, string name)
        {
            Uuid = uuid;
            Name = name;
        }

        public string Uuid
        {
            get
            {
                return uuid;
            }

            set
            {
                uuid = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }
    }
}
