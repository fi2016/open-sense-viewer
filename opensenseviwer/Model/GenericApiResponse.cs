using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    class GenericApiResponse
    {
        private string status;
        private int id;
        private string message;

        public string Status
        {
            get{return status;}
        }

        public int Id
        {
            get{return id;}
        }

        public string Message
        {
            get{return message;}
        }
    }
}
