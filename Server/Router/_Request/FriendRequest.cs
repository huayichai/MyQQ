using Server.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Router._Request
{
    class FriendRequest : Request
    {
        public string account { get; set; } // 请求谁的朋友
    }
}
