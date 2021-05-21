using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Entity
{
    public class Message
    {
        public string ID { get; set; } // 消息ID
        public string fromAccount { get; set; } // 该消息谁发送的

        public string toAccount { get; set; }
        public string fromName { get; set; } // 发送者姓名

        public string toName { get; set; }
        public string content { get; set; }
        public string time { get; set; }

        private DateTime sendTime;

        public DateTime SendTime
        {
            get { return sendTime; }
            set { sendTime = value; time = value.ToString("G"); }
        }
    }
}
