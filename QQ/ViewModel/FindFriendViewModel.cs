using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using QQ.Controller;
using QQ.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Threading;

namespace QQ.ViewModel
{
    public class FindFriendViewModel : ViewModelBase
    {
        public FindFriendViewModel()
        {
            serachResultList = new List<Friend>();

            requestList = new List<Friend>();
                
            AddFriendCommand = new RelayCommand<string>((account) =>
            {
                CenterController.SendAddFriendRequest(Global.user.account, account);
            });

            TrueReplayAddFriendCommand = new RelayCommand<string>((account) =>
            {
                Friend friend = null;
                foreach (Friend f in requestList)
                {
                    if (f.account.Equals(account))
                    {
                        friend = f;
                        break;
                    }
                }

                if (friend != null)
                {
                    Global.friendWindow.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                    {
                        Global.FriendViewModel.FriendList.Add(friend);
                        Global.friendWindow.UserInfoList.Items.Refresh();
                    });
                }
                CenterController.SendAddFriendResponse(Global.user.account, account, true);
            });

            FalseReplayAddFriendCommand = new RelayCommand<string>((account) =>
            {
                CenterController.SendAddFriendResponse(Global.user.account, account, false);
            });
        }

        public RelayCommand<string> AddFriendCommand { get; set; }

        public RelayCommand<string> TrueReplayAddFriendCommand { get; set; }
        public RelayCommand<string> FalseReplayAddFriendCommand { get; set; }

        private string searchText;

        public string SearchText
        {
            get { return searchText; }
            set { searchText = value; RaisePropertyChanged(); }
        }

        private List<Friend> serachResultList;

        public List<Friend> SerachResultList
        {
            get { return serachResultList; }
            set { serachResultList = value; RaisePropertyChanged(); }
        }

        private List<Friend> requestList;

        public List<Friend> RequestList
        {
            get { return requestList; }
            set { requestList = value; RaisePropertyChanged(); }
        }


    }
}
