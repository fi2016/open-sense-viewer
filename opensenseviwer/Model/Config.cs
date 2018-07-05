namespace Model
{
    public class Config
    {
        private string platform;
        private string username;

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
    }
}
