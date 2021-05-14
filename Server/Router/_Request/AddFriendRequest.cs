using System;
using System.Collections.Generic;
using System.Text;
using Server.Entity;

namespace Server.Router._Request
{
    public class AddFriendRequest : Request
    {
        public string userAccount{ get; set; }

        public string friendAccount { get; set; }

        // 下面属于Friend部分
        public string account { get; set; } // 账号，唯一

        public string name { get; set; } // 用户名

        public string header { get; set; } // 姓

        public string introduction { get; set; } // 个性签名


        public AddFriendRequest(string user, string friend)
        {
            type = "addFriendRequest";
            userAccount = user;
            friendAccount = friend;
        }
        public void SetFriend(Friend friend)
        {
            account = friend.account;
            name = friend.name;
            header = friend.header;
            introduction = friend.introduction;
        }

        public Friend GetFriend()
        {
            return new Friend() { account = account, name = name, header = header, introduction = introduction };
        }
    }
}
