using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace QQ.Router._Request
{
    public class Request
    {
        public string type { get; set; }

        override
        public string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
