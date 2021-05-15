using MySql.Data.MySqlClient;
using Server.Router._Request;
using Server.Router._Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.DAO
{
    class MessageDAO : BaseDAO
    {

        public static bool InsertUnreadMessage(MessageRequest request)
        {
            using (MySqlConnection msc = new MySqlConnection(constring))
            {
                string sql = "insert into unread_message (`from`,`to`,`content`,`time`) values (@from,@to,@content,@time)";
                MySqlCommand cmd = new MySqlCommand(sql, msc);
                cmd.Parameters.AddWithValue("from", request.from);
                cmd.Parameters.AddWithValue("to", request.to);
                cmd.Parameters.AddWithValue("content", request.content);
                cmd.Parameters.AddWithValue("time", request.time.ToString("G"));
                msc.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static bool InsertMessage(MessageRequest request)
        {
            using (MySqlConnection msc = new MySqlConnection(constring))
            {
                string sql = "insert into message (`from`,`to`,`content`,`time`) values (@from,@to,@content,@time)";
                MySqlCommand cmd = new MySqlCommand(sql, msc);
                cmd.Parameters.AddWithValue("from", request.from);
                cmd.Parameters.AddWithValue("to", request.to);
                cmd.Parameters.AddWithValue("content", request.content);
                cmd.Parameters.AddWithValue("time", request.time.ToString("G"));
                msc.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static List<MessageResponse> SelectUnreadMessageByAccount(string sendAccount, string receiveAccount)
        {           
            using (MySqlConnection msc = new MySqlConnection(constring))
            {
                string sql = "select *" +
                    "from unread_message" +
                    " where `to`=@receive and `from`=@send";
                MySqlCommand cmd = new MySqlCommand(sql, msc);
                cmd.Parameters.AddWithValue("send", sendAccount);
                cmd.Parameters.AddWithValue("receive", receiveAccount);
                msc.Open();
                try
                {
                    List<MessageResponse> messages = new List<MessageResponse>();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        MessageResponse response = new MessageResponse(reader);
                        messages.Add(response);
                    }
                    return messages;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }


        public static void DeleteUnreadMessageByAccount(string sendAccount, string receiveAccount)
        {
            using (MySqlConnection msc = new MySqlConnection(constring))
            {
                string sql = "delete " +
                    "from unread_message" +
                    " where `to`=@receive and `from`=@send";
                MySqlCommand cmd = new MySqlCommand(sql, msc);
                cmd.Parameters.AddWithValue("send", sendAccount);
                cmd.Parameters.AddWithValue("receive", receiveAccount);
                msc.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();                  
                }
                catch (Exception e) { }
            }
        }
    }
}
