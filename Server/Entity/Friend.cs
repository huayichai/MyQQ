using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Entity
{
    public class Friend
    {
        public string account { get; set; } // 账号，唯一

        public string name { get; set; } // 用户名

        public string header{ get; set; } // 姓

        public string introduction { get; set; }

        public Friend()
        {

        }

        public Friend(User user)
        {
            account = user.account;
            name = user.name;
            header = user.header;
            introduction = user.introduction;
        }
    }
}
