using System;
using System.Collections.Generic;
using System.Text;
using QQ.Entity;

namespace QQ.Router._Response
{
    class LoginResponse : Response
    {
        public int status { get; set; }

        public string message { get; set; }

        public User user { get; set; }
    }
}
