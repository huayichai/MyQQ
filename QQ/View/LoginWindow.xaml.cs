using QQ.Controller;
using QQ.Entity;
using QQ.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace QQ.View
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();            
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
            this.Close();
        }

        private void MinimizeClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void LoginButtonClick(object sender, RoutedEventArgs e)
        {
            // 另开一个线程去处理，避免页面不能动
            Thread thread = new Thread(new ThreadStart(LoginThread));
            thread.IsBackground = true;
            thread.Start();
        }

        private void LoginThread()
        {
            try
            {
                Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                {
                    loginButton.IsEnabled = false;
                    CenterController.SendLoginMessage(accountTextBox.Text, passwordTextBox.Password);
                    CenterController.loginAutoResetEvent.WaitOne();
                    if (CenterController.isLogin)
                    {
                        FriendWindow friendWindow = new FriendWindow();
                        Global.friendWindow = friendWindow;
                        friendWindow.Show();
                        this.Close();
                    }
                    else
                    {
                        Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                        {
                            loginButton.IsEnabled = true;
                            errorTextBlock.Text = "登陆失败";
                        });
                    }
                });
            } catch (Exception)
            {
                Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                {
                    loginButton.IsEnabled = true;
                    errorTextBlock.Text = "客户端异常";
                });
            }
        }
    }
}
