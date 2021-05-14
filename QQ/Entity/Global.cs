using QQ.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using QQ.Entity;
using QQ.View;

namespace QQ.Entity
{
    class Global
    {
        public static ClientSocket Client { get; set; } = new ClientSocket();
        public static User user { get; set; } = null;

        public static FriendWindow friendWindow { get; set; } = null;
        public static FriendViewModel FriendViewModel { set; get; } = null;

        // public static Dictionary<string, ChatViewModel> ChatViewModelMap { get; set; } = new Dictionary<string, ChatViewModel>();
        public static Dictionary<string, ChatWindow> ChatWindowMap { get; set; } = new Dictionary<string, ChatWindow>();

        public static FindFriendWindow findFriendWindow { get; set; } = null;
    }
}
