using Server.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Router._Response
{
    class LoginResponse : Response
    {
        public int status { get; set; }

        public string message { get; set; }

        public User user { get; set; }

        public LoginResponse(string message)
        {
            type = "login";
            status = 500;
            this.message = message;
        }

        public LoginResponse(User user)
        {
            type = "login";
            status = 200;
            this.user = user;
        }
    }
}
