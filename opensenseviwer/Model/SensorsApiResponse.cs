using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class SensorsApiResponse
    {
        private string status;
        private int id;
        private List<Sensor> sensors;

        public string Status
        {
            get
            {
                return status;
            }

            set
            {
                status = value;
            }
        }

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public List<Sensor> Sensors
        {
            get
            {
                return sensors;
            }

            set
            {
                sensors = value;
            }
        }
    }
}
