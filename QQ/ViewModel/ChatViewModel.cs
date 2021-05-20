using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using QQ.Controller;
using QQ.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace QQ.ViewModel
{
    public class ChatViewModel : ViewModelBase
    {

        public ChatViewModel(Friend friend)
        {
            this.friend = friend;
            title = friend.name;


            messageList = new List<TextMessage>();

            SendMessageCommand = new RelayCommand(SendMessage);

        }


        private Friend friend;

        public Friend Friend
        {
            get { return friend; }
            set { friend = value; RaisePropertyChanged(); }
        }

        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; RaisePropertyChanged(); }
        }

        private List<TextMessage> messageList;

        public List<TextMessage> MessageList
        {
            get { return messageList; }
            set { messageList = value; RaisePropertyChanged(); }
        }

        private string sendContent;

        public string SendContent
        {
            get { return sendContent; }
            set { sendContent = value; RaisePropertyChanged(); }
        }



        public RelayCommand SendMessageCommand { get; set; }

        public void SendMessage()
        {           
            CenterController.SendMessage(Global.user.account, friend.account, SendContent);
            TextMessage textMessage = new TextMessage(Global.user.account,Global.user.header ,SendContent, DateTime.Now, true);
            messageList.Add(textMessage);
            SendContent = "";
        }
    }
}
