using QQ.Controller;
using QQ.Entity;
using QQ.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QQ.View
{
    /// <summary>
    /// FriendWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FriendWindow : Window
    {
        public FriendWindow()
        {
            InitializeComponent();
            Global.FriendViewModel = new FriendViewModel();
            DataContext = Global.FriendViewModel;
            CenterController.SendGetAllFriendMessage(Global.user.account);
        }

        private void DragWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            CenterController.SendLogoutMessage(Global.user.account);
            System.Environment.Exit(0);
            this.Close();
        }

        private void MinimizeClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void DoubleClick(object sender, RoutedEventArgs e)
        {
            if (Global.ChatWindowMap.ContainsKey(Global.FriendViewModel.SelectedFriend.account))
            {
                Global.ChatWindowMap[Global.FriendViewModel.SelectedFriend.account].Visibility = Visibility.Visible;
            }
            else
            {
                ChatWindow chatWindow = new ChatWindow(Global.FriendViewModel.SelectedFriend);
                Global.ChatWindowMap.Add(Global.FriendViewModel.SelectedFriend.account, chatWindow);
                chatWindow.Show();
            }           
        }

        private void FindFreindClick(object sender, RoutedEventArgs e)
        {
            if (Global.findFriendWindow == null)
            {
                Global.findFriendWindow = new FindFriendWindow();
                Global.findFriendWindow.Show();
            }
            else
            {
                Global.findFriendWindow.Visibility = Visibility.Visible;
            }
        }
    }
}
