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
    /// ChatWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ChatWindow : Window
    {
        public ChatWindow(Friend friend)
        {
            InitializeComponent();
            InitViewModel(friend);
            DataContext = ViewModel;
            CenterController.SendUnreadMessageRequest(friend.account);
        }

        public ChatViewModel ViewModel { get; set; }


        /// <summary>
        /// 避免重复初始化
        /// </summary>
        /// <param name="friend"></param>
        private void InitViewModel(Friend friend)
        {
            //if (Global.ChatViewModelMap.ContainsKey(friend.account))
            //{
            //    ViewModel = Global.ChatViewModelMap[friend.account];
            //}
            //else
            //{
            //    ViewModel = new ChatViewModel(friend);
            //    Global.ChatViewModelMap.Add(friend.account, ViewModel);
            //}
            ViewModel = new ChatViewModel(friend);
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
            this.Visibility = Visibility.Hidden;
        }

        private void MinimizeClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void SendClick(object sender, RoutedEventArgs e)
        {
            ViewModel.SendMessage();
            MessageListBox.Items.Refresh();
            MessageListBox.ScrollIntoView(ViewModel.MessageList[ViewModel.MessageList.Count - 1]);
        }
    }

    
}
