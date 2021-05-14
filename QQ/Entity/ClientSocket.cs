using QQ.Controller;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using WebSocket4Net;

namespace QQ.Entity
{
    class ClientSocket
    {
        public WebSocket socket { get; set; }

        public ClientSocket()
        {
            socket = new WebSocket("ws://127.0.0.1:30000");
            socket.Opened += SocketOpen;
            socket.MessageReceived += SocketMessageReceived;
            socket.Error += SocketError;
            socket.Closed += SocketClosed;
            socket.Open();
            //Thread thread = new Thread(new ThreadStart(SendMessage));
            //thread.IsBackground = true;
            //thread.Start();
        }

        private void SocketOpen(object sender, EventArgs e) { }

        private void SocketMessageReceived(object sender, MessageReceivedEventArgs e)
        {
            try
            {
                CenterController.Handler(e.Message);
            } catch (Exception) { }
        }

        private void SocketError(object sender, EventArgs e)
        {
            if (socket.State != WebSocketState.Open && socket.State != WebSocketState.Connecting)
            {
                socket.Close();
            }
        }

        private void SocketClosed(object sender, EventArgs e)
        {
            if (socket.State != WebSocketState.Open && socket.State != WebSocketState.Connecting)
            {
                socket.Close();
            }
        }
    }
}
