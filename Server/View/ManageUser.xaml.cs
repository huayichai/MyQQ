using Server.ViewModel;
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

namespace Server.View
{
    /// <summary>
    /// ManageUser.xaml 的交互逻辑
    /// </summary>
    public partial class ManageUser : Window
    {
        public ManageUser()
        {
            InitializeComponent();
            ViewModel = new ManageUserViewModel(this);
            DataContext = ViewModel;
            UserListDataGrid.ItemsSource = ViewModel.Users;
        }

        public ManageUserViewModel ViewModel { get; set; }
    }
}
