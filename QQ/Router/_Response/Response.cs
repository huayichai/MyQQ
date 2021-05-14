using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace QQ.Router._Response
{
    public class Response
    {
        public string type { get; set; }

        override
        public string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
