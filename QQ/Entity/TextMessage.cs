using System;
using System.Collections.Generic;
using System.Text;

namespace QQ.Entity
{
    public class TextMessage : MessageBase
    {
        public TextMessage() { }
        public TextMessage(string name, string header, string con, DateTime time, bool isMy)
        {
            this.name = name;
            this.header = header;
            _content = con;
            SendTime = time;
            this.isMy = isMy;
            width = content.Length > 20 ? 300 : content.Length * 14 + 20;
        }

        private string _content;

        public string content
        {
            get { return _content; }
            set 
            {
                _content = value; 
                width = content.Length > 20 ? 300 : content.Length * 14 + 20;
            }
        }

    }
}
