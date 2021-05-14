using Fleck;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Entity
{
    class Client
    {
        public string account { get; set; }
        public string password { get; set; }

        public IWebSocketConnection socket { get; set; }

        public Client(string acc, string pwd)
        {
            account = acc;
            password = pwd;
        }

        public Client(string acc, string pwd, IWebSocketConnection soc)
        {
            account = acc;
            password = pwd;
            socket = soc;
        }
    }
}
