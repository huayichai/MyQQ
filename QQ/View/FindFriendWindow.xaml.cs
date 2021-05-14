using QQ.Controller;
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
    /// FindFriendWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FindFriendWindow : Window
    {
        public FindFriendWindow()
        {
            InitializeComponent();
            ViewModel = new FindFriendViewModel();
            DataContext = ViewModel;
        }

        public FindFriendViewModel ViewModel { get; set; }

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

        private void SearchFriendClick(object sender, RoutedEventArgs e)
        {
            CenterController.SendSearchFriendRequest(ViewModel.SearchText);
        }
    }
}
