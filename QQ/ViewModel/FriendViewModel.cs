using GalaSoft.MvvmLight;
using QQ.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace QQ.ViewModel
{
    class FriendViewModel : ViewModelBase
    {
        public FriendViewModel()
        {
            this.user = Global.user;
        }

        private List<Friend> friendList;

        public List<Friend> FriendList
        {
            get { return friendList; }
            set { friendList = value; RaisePropertyChanged(); }
        }

        private User user;

        public User User
        {
            get { return user; }
            set { user = value; RaisePropertyChanged(); }
        }

        private Friend selectedFriend;

        public Friend SelectedFriend
        {
            get { return selectedFriend; }
            set { selectedFriend = value; RaisePropertyChanged(); }
        }



    }
}
