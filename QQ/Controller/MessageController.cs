using QQ.Entity;
using QQ.Router._Response;
using QQ.View;
using QQ.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Threading;

namespace QQ.Controller
{
    class MessageController
    {
        public static bool ReceiveMessage(MessageResponse response)
        {
            try
            {
                // 若该界面已生成过
                if (Global.ChatWindowMap.ContainsKey(response.from))
                {
                    ChatWindow chat = Global.ChatWindowMap[response.from];
                    chat.ViewModel.MessageList.Add(new TextMessage(response.name, response.header, response.content, response.time, false));
                    chat.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                    {
                        chat.MessageListBox.Items.Refresh();
                        chat.MessageListBox.ScrollIntoView(chat.ViewModel.MessageList[chat.ViewModel.MessageList.Count - 1]);
                    });                   
                }
                else
                {
                    Friend friend = Global.FriendViewModel.FriendList.Find(item => item.account.Equals(response.from));
                    Global.friendWindow.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                    {
                        ChatWindow chat = new ChatWindow(friend);
                        chat.ViewModel.MessageList.Add(new TextMessage(response.name, response.header, response.content, response.time, false));
                        Global.ChatWindowMap.Add(friend.account, chat);
                    });                   
                }

                //// 若该界面已生成过
                //if (Global.ChatViewModelMap.ContainsKey(response.from))
                //{
                //    ChatViewModel chat = Global.ChatViewModelMap[response.from];
                //    chat.MessageList.Add(new TextMessage(response.content, response.time, false));
                //}
                //else
                //{
                //    Friend friend = Global.FriendViewModel.FriendList.Find(item => item.account.Equals(response.from));
                //    ChatViewModel chat = new ChatViewModel(friend);
                //    chat.MessageList.Add(new TextMessage(response.content, response.time, false));
                //    Global.ChatViewModelMap.Add(friend.account, chat);
                //}
                return true;
            } catch (Exception e)
            {
                return false;
            }
        }


        public static bool ReceiveHistoryMessage(HistoryResponse response)
        {
            try
            {
                ChatWindow chat = Global.ChatWindowMap[response.toAccount];
                TextMessage message = new TextMessage();
                message.name = response.name;
                message.isMy = response.isMy;
                message.time = response.time.ToString("G");
                message.content = response.content;
                chat.history.ViewModel.HistoryMessageList.Add(message);
                chat.history.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                {
                    chat.history.HistoryMessageListBox.Items.Refresh();
                });
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
