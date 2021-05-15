using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Router._Request
{
    class UnreadMessageRequest : Request
    {
        public string sendAccount { get; set; }

        public string receiveAccount { get; set; }

        public UnreadMessageRequest(string sendAccount, string receiveAccount)
        {
            this.sendAccount = sendAccount;
            this.receiveAccount = receiveAccount;
        }
    }
}
