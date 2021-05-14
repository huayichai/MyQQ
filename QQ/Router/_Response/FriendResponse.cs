using QQ.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace QQ.Router._Response
{
    class FriendResponse : Response
    {
        public List<Friend> friends { get; set; }

        public FriendResponse(List<Friend> f)
        {
            type = "friend";
            friends = f;
        }
    }
}
