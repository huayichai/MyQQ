using System;
using System.Collections.Generic;
using System.Text;

namespace QQ.Router._Request
{
    class MessageRequest : Request
    {
        public string from { get; set; }

        public string to { get; set; }

        public string content { get; set; }

        public DateTime time { get; set; }

        public MessageRequest(string from, string to, string content)
        {
            type = "message";
            this.from = from;
            this.to = to;
            this.content = content;
            time = DateTime.Now;
        }
    }
}
