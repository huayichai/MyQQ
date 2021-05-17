using System;
using System.Collections.Generic;
using System.Text;

namespace QQ.Router._Response
{
    class HistoryResponse : Response
    {
        public HistoryResponse()
        {
            type = "history";
        }
        public string fromAccount { get; set; } // 谁请求历史记录
        public string toAccount { get; set; } // 请求和谁的历史记录
        public string name { get; set; } // 该记录的用户名字
        public string content { get; set; }
        public bool isMy { get; set; }
        public DateTime time { get; set; }
    }
}
