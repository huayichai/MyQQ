using Server.DAO;
using Server.Entity;
using Server.ViewModel;
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

namespace Server.View
{
    /// <summary>
    /// ChatRecordWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ChatRecordWindow : Window
    {
        public ChatRecordWindow()
        {
            InitializeComponent();
            ViewModel = new ChatRecordViewModel();
            DataContext = ViewModel;
        }

        public ChatRecordViewModel ViewModel { get; set; }

        private void SearchClick(object sender, RoutedEventArgs e)
        {
            List<Message> messages = MessageDAO.SelectMessageByAccount(ViewModel.SearchText);
            if (messages!=null)
            {
                ViewModel.Messages.Clear();
                foreach (Message m in messages)
                {
                    ViewModel.Messages.Add(m);
                }
                MessageListDataGrid.ItemsSource = ViewModel.Messages;
                //Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                //{
                //    MessageListDataGrid.Items.Refresh();
                //});
            }
        }
    }
}
