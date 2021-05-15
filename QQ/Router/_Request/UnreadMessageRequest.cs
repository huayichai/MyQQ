using QQ.Router._Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace QQ.Router._Request
{
    class UnreadMessageRequest:Request
    {
        public string sendAccount { get; set; }

        public string receiveAccount { get; set; }

        public UnreadMessageRequest(string sendAccount, string receiveAccount)
        {
            type = "unreadMessage";
            this.sendAccount = sendAccount;
            this.receiveAccount = receiveAccount;
        }
    }
}
