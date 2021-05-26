using Server.Entity;
using Server.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Server
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ServerSocket server = new ServerSocket();
            server.LogBox = LogBox;
            server.dispatcher = Dispatcher;
        }

        private void MessageManagementClick(object sender, RoutedEventArgs e)
        {
            ChatRecordWindow window = new ChatRecordWindow();
            window.Show();
        }

        private void UserManageClick(object sender, RoutedEventArgs e)
        {
            ManageUser window = new ManageUser();
            window.Show();
        }
    }
}
