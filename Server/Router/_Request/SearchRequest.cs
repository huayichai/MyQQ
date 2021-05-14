using Server.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Router._Request
{
    class SearchRequest : Request
    {
        public SearchRequest(string account)
        {
            type = "search";
            this.account = account;
        }

        public string account { get; set; }
    }
}
