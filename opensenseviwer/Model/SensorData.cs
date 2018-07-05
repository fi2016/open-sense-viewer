using System;

namespace Model
{
    public class SensorData<Sensor>
    {
        private DateTime time;
        private float value;

        public SensorData(DateTime time, float value)
        {
            Time = time;
            Value = value;
        }

        public DateTime Time
        {
            get
            {
                return time;
            }

            set
            {
                time = value;
            }
        }

        public float Value
        {
            get
            {
                return value;
            }

            set
            {
                this.value = value;
            }
        }
    }
}
