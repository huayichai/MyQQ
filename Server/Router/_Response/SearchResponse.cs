using Server.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Router._Response
{
    class SearchResponse : Response
    {
        public bool isEmpty { get; set; }

        public List<Friend> friends { get; set; } = new List<Friend>();

        public SearchResponse(bool isEmpty)
        {
            type = "search";
            this.isEmpty = isEmpty;
        }
    }
}
