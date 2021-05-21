using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Server.DAO;
using Server.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;

namespace Server.ViewModel
{
    public class ChatRecordViewModel : ViewModelBase
    {
        public ChatRecordViewModel()
        {
            messages = new ObservableCollection<Message>();
            DeleteCommand = new RelayCommand<string>((messageID) =>
            {
                foreach (Message m in messages)
                {
                    if (m.ID.Equals(messageID))
                    {
                        messages.Remove(m);
                        break;
                    }
                }
                MessageDAO.DeleteMessageByAccount(messageID);
            });
        }

        private string searchText;

        public string SearchText
        {
            get { return searchText; }
            set { searchText = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Message> messages;

        public ObservableCollection<Message> Messages
        {
            get { return messages; }
            set { messages = value; RaisePropertyChanged(); }
        }

        public RelayCommand<string> DeleteCommand { get; set; }

    }
}
