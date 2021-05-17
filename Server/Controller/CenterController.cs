using Fleck;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Server.Entity;
using Server.Router;
using Server.Router._Request;
using Server.Router._Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Controller
{
    class CenterController
    {
        public static Dictionary<string, Client> clientMap { get; set; } = new Dictionary<string, Client>();

        public static Response Handler(IWebSocketConnection socket, string message)
        {
            string type;
            try
            {
                JObject json = JObject.Parse(message);
                type = (string)json["type"]; // 提取消息类型，根据类型进行处理
            } catch (Exception)
            {
                return ReturnErrorStatus("事件未注册");
            }
            if (type.Equals("login"))
            {
                return LoginHandler(socket, message);
            } 
            else if (type.Equals("message"))
            {
                MessageHandler(socket, message);
                return null;
            } 
            else if (type.Equals("friend"))
            {
                return FriendHandler(socket, message);
            }
            else if (type.Equals("logout"))
            {
                LogoutHandler(message);
                return null;
            } 
            else if (type.Equals("addFriendRequest"))
            {
                AddFriendHandler(message, true);
                return null;
            } 
            else if (type.Equals("addFriendResponse"))
            {
                AddFriendHandler(message, false);
                return null;
            }
            else if (type.Equals("search"))
            {
                return SearchHandler(message);
            }
            else if (type.Equals("unreadMessage"))
            {
                UnreadMessageHandler(message);
                return null;
            }
            else if (type.Equals("history"))
            {
                HistoryHandler(message);
                return null;
            }
            else
            {
                return ReturnErrorStatus(type + " 事件未注册");
            }
        }

        private static StatusResponse ReturnLoginStatus(bool status, string successMessage, string errorMessage)
        {
            if (status)
            {
                return new StatusResponse("login", 200, successMessage);
            }
            else
            {
                return new StatusResponse("login", 500, errorMessage);
            }
        }

        private static StatusResponse ReturnStatus(bool status, string successMessage, string errorMessage)
        {
            if (status)
            {
                return new StatusResponse(200, successMessage);
            }
            else
            {
                return ReturnErrorStatus(errorMessage);
            }
        }

        private static StatusResponse ReturnErrorStatus(string errorMessage)
        {
            return new StatusResponse(500, errorMessage);
        }

        private static LoginResponse LoginHandler(IWebSocketConnection socket, string message)
        {
            try
            {
                LoginRequest loginRequest = (LoginRequest)JsonConvert.DeserializeObject(message, typeof(LoginRequest));
                User user = UserController.login(loginRequest.account, loginRequest.password);
                if (user != null)
                {
                    Client client = new Client(user.account, user.password, socket);
                    clientMap.Add(user.account, client);
                    return new LoginResponse(user);
                }
                else
                {
                    return new LoginResponse("账号密码错误");
                }
            } 
            catch(Exception)
            {
                return new LoginResponse("账号密码错误");
            }
        }

        private static bool MessageHandler(IWebSocketConnection socket, string message)
        {
            try
            {
                MessageRequest messageRequest = (MessageRequest)JsonConvert.DeserializeObject(message, typeof(MessageRequest));
                if (MessageController.ForwardMessage(messageRequest))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static FriendResponse FriendHandler(IWebSocketConnection socket, string message)
        {
            try
            {
                LogoutRequest friendRequest = (LogoutRequest)JsonConvert.DeserializeObject(message, typeof(LogoutRequest));
                List<Friend> friends = FriendController.GetAllFriends(friendRequest.account);
                if (friends != null)
                {
                    return new FriendResponse(friends);
                }
                else
                {
                    return new FriendResponse(new List<Friend>());
                }
            }
            catch (Exception)
            {
                return new FriendResponse(new List<Friend>());
            }
        }

        private static void LogoutHandler(string message)
        {
            try
            {
                LogoutRequest logoutRequest = (LogoutRequest)JsonConvert.DeserializeObject(message, typeof(LogoutRequest));
                clientMap.Remove(logoutRequest.account);
            }
            catch (Exception)
            {
                
            }
        }

        private static void AddFriendHandler(string message, bool isRequest)
        {
            try
            {
                if (isRequest)
                {
                    AddFriendRequest request = (AddFriendRequest)JsonConvert.DeserializeObject(message, typeof(AddFriendRequest));
                    FriendController.AddFriendRequest(request);
                }
                else
                {
                    AddFriendResponse response = (AddFriendResponse)JsonConvert.DeserializeObject(message, typeof(AddFriendResponse));
                    FriendController.AddFriendResponse(response);
                }
                
            }
            catch (Exception e)
            {

            }
        }

        private static SearchResponse SearchHandler(string message)
        {
            try
            {
                SearchRequest request = (SearchRequest)JsonConvert.DeserializeObject(message, typeof(SearchRequest));
                Friend friend =  FriendController.SearchFriend(request.account);
                SearchResponse response;
                if (friend != null)
                {
                    response = new SearchResponse(false);
                    response.friends.Add(friend);
                }
                else
                {
                    response = new SearchResponse(true);
                }
                return response;
            }
            catch (Exception)
            {
                return new SearchResponse(true);
            }
        }

        private static void UnreadMessageHandler(string message)
        {
            try
            {
                UnreadMessageRequest request = (UnreadMessageRequest)JsonConvert.DeserializeObject(message, typeof(UnreadMessageRequest));
                MessageController.ForwardUnreadMessage(request);
            }
            catch (Exception)
            {
                return;
            }
        }

        private static void HistoryHandler(string message)
        {
            try
            {
                HistoryRequest request = (HistoryRequest)JsonConvert.DeserializeObject(message, typeof(HistoryRequest));
                MessageController.GetHistoryMessage(request);               
            }
            catch (Exception) { }
        }
    }
}
