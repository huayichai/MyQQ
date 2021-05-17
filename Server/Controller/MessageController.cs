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
                Client toUser = null;
                if (CenterController.clientMap.ContainsKey(messageRequest.to)) // 在线情况
                {
                    toUser = CenterController.clientMap[messageRequest.to];
                    toUser.socket.Send(new MessageResponse(messageRequest).ToString());
                }
                else // 离线情况
                {
                    MessageDAO.InsertUnreadMessage(messageRequest);
                }
                MessageDAO.InsertMessage(messageRequest);
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
