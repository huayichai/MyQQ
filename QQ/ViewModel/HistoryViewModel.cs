using GalaSoft.MvvmLight;
using QQ.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace QQ.ViewModel
{
    public class HistoryViewModel : ViewModelBase
    {

        public HistoryViewModel()
        {
            historyMessageList = new List<TextMessage>();
        }

        private List<TextMessage> historyMessageList;

        public List<TextMessage> HistoryMessageList
        {
            get { return historyMessageList; }
            set { historyMessageList = value; RaisePropertyChanged(); }
        }

    }
}
