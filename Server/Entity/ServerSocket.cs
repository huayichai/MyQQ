using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Threading;
using Fleck;
using Server.Controller;
using Server.Router._Response;

namespace Server.Entity
{
    class ServerSocket
    {
        private WebSocketServer _server;

        public WebSocketServer server
        {
            get { return _server; }
            set { _server = value; }
        }

        public TextBox LogBox = null;
        public Dispatcher dispatcher = null;


        public ServerSocket(string ip = "127.0.0.1", string port = "30000")
        {
            Init();
        }

        public void Init()
        {
            _server = new WebSocketServer("ws://127.0.0.1:30000");
            _server.RestartAfterListenError = true;
            _server.Start(Listen); //启动服务器监听程序，接收客户端连接
        }

        public void Listen(IWebSocketConnection socket)
        {
            socket.OnOpen = () =>
            {
                string clientUrl = socket.ConnectionInfo.ClientIpAddress + ":" + socket.ConnectionInfo.ClientPort;
                Log(DateTime.Now.ToString() + "|服务器:和客户端:" + clientUrl + " 建立WebSock连接！");
            };
            socket.OnClose = () =>  //连接关闭事件
            {
                string clientUrl = socket.ConnectionInfo.ClientIpAddress + ":" + socket.ConnectionInfo.ClientPort;
                Log(DateTime.Now.ToString() + "|服务器:和客户端:" + clientUrl + " 连接关闭！");
            };
            socket.OnMessage = message =>  //接受客户端网页消息事件
            {
                string clientUrl = socket.ConnectionInfo.ClientIpAddress + ":" + socket.ConnectionInfo.ClientPort;
                Log(DateTime.Now.ToString() + "|服务器:【收到】来客户端:" + clientUrl + "的信息：\n" + message);
                Response response = CenterController.Handler(socket, message);
                if (response != null)
                {
                    socket.Send(response.ToString());
                }               
            };
        }

        private void Log(string s)
        {
            try
            {
                dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                {
                    LogBox.Text += (s + "\r\n");
                    LogBox.ScrollToEnd();
                });
            }
            catch (Exception) { }
        }
    }
}
