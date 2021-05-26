using Fleck;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Entity
{
    public class User
    {
        public string account { get; set; }
        public string name { get; set; }
        public string password { get; set; }

        public string isLock { get; set; }
        public string header { get; set; }
        public string introduction { get; set; }


        public User(string acc, string pwd, string name)
        {
            account = acc;
            password = pwd;
            this.name = name;
        }

        public User()
        {

        }
    }
}
