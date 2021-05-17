using System;
using System.Collections.Generic;
using System.Text;

namespace QQ.Router._Response
{
    class MessageResponse : Response
    {

        public string from { get; set; }

        public string to { get; set; }

        public string fromName { get; set; }

        public string content { get; set; }

        public DateTime time { get; set; }
    }
}
