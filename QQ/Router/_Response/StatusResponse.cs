using System;
using System.Collections.Generic;
using System.Text;

namespace QQ.Router._Response 
{ 
    class StatusResponse : Response
    {
        public int _status { get; set; } // 状态码

        public string _message { get; set; }

        public StatusResponse(int status = 200, string message = "")
        {
            _status = status;
            _message = message;
        }
    }
}
