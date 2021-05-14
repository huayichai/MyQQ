using MySql.Data.MySqlClient;
using Server.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.DAO
{
    class UserDAO : BaseDAO
    {
        public static User SelectUserByAccount(string account)
        {
            using (MySqlConnection msc = new MySqlConnection(constring))
            {
                string sql = "select * from user where account=@account";
                MySqlCommand cmd = new MySqlCommand(sql, msc);
                cmd.Parameters.AddWithValue("account", account);
                msc.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        User user = new User();
                        user.account = reader.GetString("account");
                        user.password = reader.GetString("password");
                        user.name = reader.GetString("name");
                        user.header = reader.GetString("header");
                        user.introduction = reader.GetString("introduction");
                        return user;
                    }
                    return null;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        public static List<Friend> SelectFriends(string userAccount)
        {
            using (MySqlConnection msc = new MySqlConnection(constring))
            {
                string sql = "select user.*" +
                    "from friend,user" +
                    " where friend.userAccount=@userAccount and friend.friendAccount=user.account";
                MySqlCommand cmd = new MySqlCommand(sql, msc);
                cmd.Parameters.AddWithValue("userAccount", userAccount);
                msc.Open();
                try
                {
                    List<Friend> friends = new List<Friend>();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Friend friend = new Friend();
                        friend.account = reader.GetString("account");
                        friend.name = reader.GetString("name");
                        friend.header = reader.GetString("header");
                        friend.introduction = reader.GetString("introduction");
                        friends.Add(friend);
                    }
                    return friends;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    
    }
}
