using MySql.Data.MySqlClient;
using Server.Entity;
using Server.Router._Request;
using Server.Router._Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.DAO
{
    class MessageDAO : BaseDAO
    {

        public static bool InsertUnreadMessage(MessageRequest request, string realAccount)
        {
            using (MySqlConnection msc = new MySqlConnection(constring))
            {
                string sql = "insert into unread_message (`messageID`,`from`,`to`,`content`,`time`,`realAccount`) values (@ID,@from,@to,@content,@time,@realAccount)";
                MySqlCommand cmd = new MySqlCommand(sql, msc);
                cmd.Parameters.AddWithValue("ID", IDUtil.GenerateMessageID());
                cmd.Parameters.AddWithValue("from", request.from);
                cmd.Parameters.AddWithValue("to", request.to);
                cmd.Parameters.AddWithValue("content", request.content);
                cmd.Parameters.AddWithValue("time", request.time);
                cmd.Parameters.AddWithValue("realAccount", realAccount);
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

        public static bool InsertMessage(MessageRequest request, string realAccount)
        {
            using (MySqlConnection msc = new MySqlConnection(constring))
            {
                string sql = "insert into message (`messageID`,`from`,`to`,`content`,`time`,`realAccount`) values (@ID,@from,@to,@content,@time,@realAccount)";
                MySqlCommand cmd = new MySqlCommand(sql, msc);
                cmd.Parameters.AddWithValue("ID", IDUtil.GenerateMessageID());
                cmd.Parameters.AddWithValue("from", request.from);
                cmd.Parameters.AddWithValue("to", request.to);
                cmd.Parameters.AddWithValue("content", request.content);
                cmd.Parameters.AddWithValue("time", DateTime.Now);
                cmd.Parameters.AddWithValue("realAccount", realAccount);
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
                    "from unread_message,user" +
                    " where `to`=@receive and `from`=@send and user.`account`=unread_message.`realAccount`";
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

        public static List<HistoryResponse> SelectHistoryMessage(string fromAccount, string toAccount)
        {
            using (MySqlConnection msc = new MySqlConnection(constring))
            {
                string sql = "select message.from,message.time,message.content,user.name " +
                    "from message,user" +
                    " where ((message.`to`=@to1 and message.`from`=@from1) or (message.`to`=@to2 and message.`from`=@from2)) and (message.`realAccount`=user.`account`)";
                MySqlCommand cmd = new MySqlCommand(sql, msc);
                cmd.Parameters.AddWithValue("to1", fromAccount);
                cmd.Parameters.AddWithValue("from1", toAccount);
                cmd.Parameters.AddWithValue("to2", toAccount);
                cmd.Parameters.AddWithValue("from2", fromAccount);
                msc.Open();
                try
                {
                    List<HistoryResponse> messages = new List<HistoryResponse>();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        HistoryResponse response = new HistoryResponse();
                        response.fromAccount = fromAccount;
                        response.toAccount = toAccount;
                        response.name = reader.GetString("name");
                        response.time = reader.GetDateTime("time");
                        response.isMy = fromAccount.Equals(reader.GetString("from"));
                        response.content = reader.GetString("content");
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
    
        public static List<Message> SelectMessageByAccount(string account)
        {
            using (MySqlConnection msc = new MySqlConnection(constring))
            {
                string sql = "select message.*, u1.name u1Name, u2.name u2Name " +
                    "from message,user u1, user u2 " +
                    "where (message.`to`=@to or message.`from`=@from) and (message.`realAccount`=u1.`account`) and (message.`to`=u2.`account`)";
                MySqlCommand cmd = new MySqlCommand(sql, msc);
                cmd.Parameters.AddWithValue("to", account);
                cmd.Parameters.AddWithValue("from", account);
                msc.Open();
                try
                {
                    List<Message> messages = new List<Message>();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Message m = new Message();
                        m.ID = reader.GetString("messageID");
                        m.fromAccount = reader.GetString("realAccount");
                        m.toAccount = reader.GetString("to");
                        m.fromName = reader.GetString("u1Name");
                        m.toName = reader.GetString("u2Name");
                        m.content = reader.GetString("content");
                        m.SendTime = reader.GetDateTime("time");
                        messages.Add(m);
                    }
                    return messages;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }
    
        public static bool DeleteMessageByAccount(string messageID)
        {
            using (MySqlConnection msc = new MySqlConnection(constring))
            {
                string sql = "delete " +
                    "from message " +
                    "where `messageID`=@messageID";
                MySqlCommand cmd = new MySqlCommand(sql, msc);
                cmd.Parameters.AddWithValue("messageID", messageID);
                msc.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception e) 
                {
                    return false;
                }
            }
        }
    }
}
