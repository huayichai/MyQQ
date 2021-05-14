using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Server.Router._Response
{
    class Response
    {

        public string type { get; set; }

        override
        public string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
