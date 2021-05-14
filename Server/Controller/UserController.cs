using Server.DAO;
using Server.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Controller
{
    class UserController
    {
        public static User login(string account, string password)
        {
            User user = UserDAO.SelectUserByAccount(account);

            if (user != null)
            {
                if (user.password.Equals(password))
                {
                    return user;
                } 
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
