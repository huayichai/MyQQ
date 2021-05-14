using QQ.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace QQ.Router._Response
{
    class SearchResponse : Response
    {
        public bool isEmpty { get; set; }

        public List<Friend> friends { get; set; }
    }
}
