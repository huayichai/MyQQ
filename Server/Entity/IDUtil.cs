using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Entity
{
    class IDUtil
    {
        public static string GenerateMessageID()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssff");
        }
    }
}
