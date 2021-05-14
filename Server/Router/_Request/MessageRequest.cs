using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Router._Request
{
    class MessageRequest:Request
    {
        public string from { get; set; }

        public string to { get; set; }

        public string content { get; set; }

        public DateTime time { get; set; }
    }
}
