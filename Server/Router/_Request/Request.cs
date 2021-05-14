using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Router._Request
{
    public abstract class Request
    {
        public string type { get; set; }

        override
        public string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
