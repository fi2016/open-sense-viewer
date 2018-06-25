using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class DataApiResponse
    {
        private string status;
        private int id;
        private List<SensorData<Sensor>> values;

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

        public List<SensorData<Sensor>> Values
        {
            get
            {
                return values;
            }

            set
            {
                values = value;
            }
        }
    }
}
