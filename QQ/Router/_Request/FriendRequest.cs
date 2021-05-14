using System;
using System.Collections.Generic;
using System.Text;

namespace QQ.Router._Request
{
    class FriendRequest : Request
    {
        public string account { get; set; }
        public FriendRequest(string acc)
        {
            type = "friend";
            account = acc;
        }
    }
}
