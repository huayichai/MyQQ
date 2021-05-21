using GalaSoft.MvvmLight;
using Server.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Server.ViewModel
{
    public class ChatRecordViewModel : ViewModelBase
    {
        public ChatRecordViewModel()
        {
            messages = new ObservableCollection<Message>();
            messages.Add(new Message() { fromName = "柴华溢", toName = "郭世祺", content = "加油", time = "20210521" });
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


    }
}
