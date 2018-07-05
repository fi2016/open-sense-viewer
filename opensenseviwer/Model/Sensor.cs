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

        public override string ToString()
        {
            return Name;
        }
    }
}
