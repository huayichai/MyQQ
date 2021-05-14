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
                return true;
            } catch (Exception)
            {
                return false;
            }
        }
    }
}
