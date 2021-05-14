using System;
using System.Collections.Generic;
using System.Text;

namespace QQ.Router._Request
{
    class LogoutRequest : Request
    {
        public LogoutRequest(string account)
        {
            type = "logout";
            this.account = account;
        }

        public string account { get; set; }
    }
}
