using System;
using System.Collections.Generic;
using System.Text;
using QQ.Router._Request;
using QQ.Router._Response;
using QQ.Entity;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Threading;
using QQ.View;

namespace QQ.Controller
{
    public class FriendController
    {
        public static void ShowAddFriendRequest(AddFriendRequest request)
        {
            if (Global.findFriendWindow != null)
            {
                Global.findFriendWindow.ViewModel.RequestList.Add(request.GetFriend());
            }
            else
            {
                Global.friendWindow.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                {
                    FindFriendWindow window = new FindFriendWindow();
                    window.ViewModel.RequestList.Add(request.GetFriend());
                    Global.findFriendWindow = window;
                });
            }
            Global.findFriendWindow.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                Global.findFriendWindow.AddFriendRequestListBox.Items.Refresh();
            });
        }

        public static void ShowAddFriendResponse(AddFriendResponse response)
        {
            Global.friendWindow.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                Global.FriendViewModel.FriendList.Add(response.GetFriend());
                Global.friendWindow.UserInfoList.Items.Refresh();
            });
        }
    }
}
