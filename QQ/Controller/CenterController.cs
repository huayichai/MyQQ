using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QQ.Entity;
using QQ.Router._Request;
using QQ.Router._Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Threading;

namespace QQ.Controller
{
    class CenterController
    {
        /// <summary>
        /// 控制登录信号量
        /// </summary>
        public static AutoResetEvent loginAutoResetEvent { get; set; } = new AutoResetEvent(false); // 初始无信号
        public static bool isLogin { get; set; }

        public static bool Handler(string message)
        {
            string type;
            try
            {
                JObject json = JObject.Parse(message);
                type = (string)json["type"]; // 提取返回消息类型，根据类型进行处理
            }
            catch (Exception)
            {
                return false;
            }

            if (type.Equals("message"))
            {
                return MessageHandler(message);
            }
            else if (type.Equals("status"))
            {
                return StatusHandler(message);
            } 
            else if (type.Equals("login"))
            {
                return LoginHandler(message);
            }
            else if (type.Equals("friend"))
            {
                return FriendHandler(message);
            }
            else if (type.Equals("addFriendRequest"))
            {
                return AddFriendHandler(message, true);
            }
            else if (type.Equals("addFriendResponse"))
            {
                return AddFriendHandler(message, false);
            }
            else if (type.Equals("search"))
            {
                SearchFriendHandler(message);
                return false;
            }
            else
            {
                return false;
            }
        }

        private static bool StatusHandler(string message)
        {
            try
            {
                StatusResponse res = (StatusResponse)JsonConvert.DeserializeObject(message, typeof(StatusResponse));
                if (res._status == 200) return true;
                else return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static bool LoginHandler(string message)
        {
            try
            {
                LoginResponse res = (LoginResponse)JsonConvert.DeserializeObject(message, typeof(LoginResponse));
                if (res.status == 200)
                {
                    isLogin = true;
                    Global.user = res.user;
                    loginAutoResetEvent.Set();
                    return true;
                }
                else
                {
                    isLogin = false;
                    loginAutoResetEvent.Set();
                    return false;
                }
            }
            catch (Exception)
            {
                isLogin = false;
                loginAutoResetEvent.Set();
                return false;
            }
        }

        private static bool FriendHandler(string message)
        {
            try
            {
                FriendResponse res = (FriendResponse)JsonConvert.DeserializeObject(message, typeof(FriendResponse));
                Global.FriendViewModel.FriendList = res.friends;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static bool MessageHandler(string message)
        {
            try
            {
                MessageResponse res = (MessageResponse)JsonConvert.DeserializeObject(message, typeof(MessageResponse));
                MessageController.ReceiveMessage(res);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static bool AddFriendHandler(string message, bool isRequest)
        {
            try
            {
                if (isRequest)
                {
                    AddFriendRequest request = (AddFriendRequest)JsonConvert.DeserializeObject(message, typeof(AddFriendRequest));
                    FriendController.ShowAddFriendRequest(request);
                }
                else
                {
                    AddFriendResponse response = (AddFriendResponse)JsonConvert.DeserializeObject(message, typeof(AddFriendResponse));
                    FriendController.ShowAddFriendResponse(response);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private static bool SearchFriendHandler(string message)
        {
            try
            {
                SearchResponse response = (SearchResponse)JsonConvert.DeserializeObject(message, typeof(SearchResponse));
                if (!response.isEmpty)
                {
                    Global.findFriendWindow.ViewModel.SerachResultList.Clear();
                    Global.findFriendWindow.ViewModel.SerachResultList.AddRange(response.friends);
                    Global.findFriendWindow.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                    {
                        Global.findFriendWindow.SearchResultListBox.Items.Refresh();
                    });                    
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public static void SendLoginMessage(string account, string password)
        {
            LoginRequest request = new LoginRequest(account, password);
            Global.Client.socket.Send(request.ToString());
        }

        public static void SendGetAllFriendMessage(string account)
        {
            FriendRequest request = new FriendRequest(account);
            Global.Client.socket.Send(request.ToString());
        }

        public static void SendLogoutMessage(string account)
        {
            LogoutRequest request = new LogoutRequest(account);
            Global.Client.socket.Send(request.ToString());
        }

        public static void SendMessage(string from, string to, string content)
        {
            MessageRequest request = new MessageRequest(from, to, content);
            Global.Client.socket.Send(request.ToString());
        }

        public static void SendSearchFriendRequest(string account)
        {
            SearchRequest request = new SearchRequest(account);
            Global.Client.socket.Send(request.ToString());
        }
        public static void SendAddFriendRequest(string userAccount, string friendAccount)
        {
            AddFriendRequest request = new AddFriendRequest(userAccount, friendAccount);
            Global.Client.socket.Send(request.ToString());
        }

        public static void SendAddFriendResponse(string userAccount, string friendAccount, bool res)
        {
            AddFriendResponse response = new AddFriendResponse(userAccount, friendAccount, res);
            Global.Client.socket.Send(response.ToString());
            Global.findFriendWindow.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                Global.findFriendWindow.ViewModel.RequestList.Clear();
                Global.findFriendWindow.AddFriendRequestListBox.Items.Refresh();
            });
        }
    }
}
