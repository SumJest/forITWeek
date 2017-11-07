using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWeek
{
    public class ConnInfo
    {

        public string Server { get { return server; } set { server = value; } }
        private string server;
        public string Username { get { return username; } set { username = value; } }
        private string username;
        public string Password { get { return password; } set { password = value; } }
        private string password;

        public ConnInfo(string server, string username, string password)
        {
            this.server = server;
            this.username = username;
            this.password = password;
        }


    }
}
