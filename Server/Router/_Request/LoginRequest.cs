using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Router._Request
{
    class LoginRequest : Request
    {
        public string account { get; set; }

        public string password { get; set; }
    }
}
