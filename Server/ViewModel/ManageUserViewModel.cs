using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Server.DAO;
using Server.Entity;
using Server.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;

namespace Server.ViewModel
{

    public class ManageUserViewModel : ViewModelBase
    {

        public ManageUserViewModel(ManageUser window)
        {
            this.window = window;
            var temp = UserDAO.SelectAllUser();
            users = new ObservableCollection<User>();
            foreach (User u in temp)
            {
                users.Add(u);
            }
            StopCommand = new RelayCommand<string>((account) =>
            {
                foreach (User u in users)
                {
                    if (u.account.Equals(account))
                    {
                        if (u.isLock.Equals("解禁"))
                        {
                            u.isLock = "禁止";
                            UserDAO.UpdateUserIsLock(account, "禁止");
                        }
                        else
                        {
                            u.isLock = "解禁";
                            UserDAO.UpdateUserIsLock(account, "解禁");
                        }
                        break;
                    }
                }
                this.window.UserListDataGrid.ItemsSource = null;
                this.window.UserListDataGrid.ItemsSource = Users;
            });
        }

        public ManageUser window { get; set; }

        private ObservableCollection<User> users;

        public ObservableCollection<User> Users
        {
            get { return users; }
            set { users = value; RaisePropertyChanged(); }
        }

        public RelayCommand<string> StopCommand { get; set; }
    }
}
