using System;
using System.Collections.Generic;
using System.Text;

namespace QQ.Entity
{
    public class MessageBase
    {
        public string ID { get; set; } // 消息ID
        public string fromAccount { get; set; } // 该消息谁发送的
        public string userName { get; set; } // 发送者姓名
        public DateTime sendTime { get; set; }
        public bool isMy { get; set; } // 是不是我发送的消息，是我发送的，则在右边显示

        public int width { get; set; }
    }
}
