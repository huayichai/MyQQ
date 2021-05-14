using System;
using System.Collections.Generic;
using System.Text;

namespace QQ.Entity
{
    public class Friend
    {
        public string account { get; set; } // 账号，唯一

        public string name { get; set; } // 用户名

        public string header { get; set; } // 姓

        public string introduction { get; set; } // 个性签名
    }
}
