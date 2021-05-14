using System;
using System.Collections.Generic;
using System.Text;

namespace QQ.Router._Request
{
    class LoginRequest : Request
    {
        public string account { get; set; }

        public string password { get; set; }

        public LoginRequest(string acc, string pwd)
        {
            type = "login";
            account = acc;
            password = pwd;
        }
    }
}
