using System;
using System.Collections.Generic;
using System.Text;

namespace QQ.Router._Request
{
    class HistoryRequest : Request
    {

        public string from { get; set; }

        public string to { get; set; }
        public HistoryRequest()
        {
            type = "history";
        }
    }
}
