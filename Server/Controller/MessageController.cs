using Server.DAO;
using Server.Entity;
using Server.Router._Request;
using Server.Router._Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Controller
{
    class MessageController
    {
        public static bool ForwardMessage(MessageRequest messageRequest)
        {
            try
            {
                // 转发单人消息
                if (messageRequest.to[0]!='-')
                {
                    User user = UserDAO.SelectUserByAccount(messageRequest.from);
                    Client toUser = null;
                    if (CenterController.clientMap.ContainsKey(messageRequest.to)) // 在线情况
                    {
                        toUser = CenterController.clientMap[messageRequest.to];
                        MessageResponse res = new MessageResponse(messageRequest);
                        res.name = user.name;
                        res.header = user.header;
                        toUser.socket.Send(res.ToString());
                    }
                    else // 离线情况
                    {
                        MessageDAO.InsertUnreadMessage(messageRequest, messageRequest.from);
                    }
                    MessageDAO.InsertMessage(messageRequest, messageRequest.from);
                }
                else // 群聊消息
                {
                    List<Friend> group_friend = UserDAO.SelectGroupFriend(messageRequest.to);
                    User user = UserDAO.SelectUserByAccount(messageRequest.from);
                    foreach(Friend f in group_friend)
                    {
                        if (!f.account.Equals(messageRequest.from))
                        {
                            Client toUser = null;
                            MessageRequest request = new MessageRequest();
                            request.from = messageRequest.to;
                            request.to = f.account;
                            request.content = messageRequest.content;
                            request.time = messageRequest.time;
                            if (CenterController.clientMap.ContainsKey(f.account)) // 在线情况
                            {
                                toUser = CenterController.clientMap[f.account];
                                MessageResponse res = new MessageResponse(request);
                                res.name = user.name;
                                res.header = user.header;
                                toUser.socket.Send(res.ToString());
                            }
                            else // 离线情况
                            {
                                MessageDAO.InsertUnreadMessage(request, messageRequest.from);
                            }
                            MessageDAO.InsertMessage(request, messageRequest.from);
                        }                       
                    }
                }
                return true;
            } catch (Exception)
            {
                return false;
            }
        }

        public static void ForwardUnreadMessage(UnreadMessageRequest request)
        {
            try
            {
                Client toUser = null;
                if (CenterController.clientMap.ContainsKey(request.receiveAccount)) // 在线情况
                {
                    toUser = CenterController.clientMap[request.receiveAccount];
                    List<MessageResponse> messages = MessageDAO.SelectUnreadMessageByAccount(request.sendAccount, request.receiveAccount);
                    foreach (MessageResponse message in messages)
                    {
                        toUser.socket.Send(message.ToString());
                        MessageDAO.DeleteUnreadMessageByAccount(request.sendAccount, request.receiveAccount);
                    }                    
                }

            } catch (Exception e)
            {

            }
        }
    
        public static void GetHistoryMessage(HistoryRequest request)
        {
            try
            {
                Client toUser = null;
                if (CenterController.clientMap.ContainsKey(request.from)) // 在线情况
                {
                    toUser = CenterController.clientMap[request.from];
                    List<HistoryResponse> messages = MessageDAO.SelectHistoryMessage(request.from, request.to);
                    if (messages != null)
                    {
                        foreach (HistoryResponse message in messages)
                        {
                            toUser.socket.Send(message.ToString());
                        }
                    }
                }
            } catch(Exception) { }
        }
    }
}
