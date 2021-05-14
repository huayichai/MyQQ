using QQ.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace QQ.View
{
    public class ChatMessageDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate LeftMessageTemplate { get; set; }
        public DataTemplate RightMessageTemplate { get; set; }
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            TextMessage message = item as TextMessage; //CombinedEnity为绑定数据对象
            bool isSendByMe = message.isMy;
            if (isSendByMe)//是否为我发送的，我发送的则是右侧消息样式，他人发送则为左侧样式。
            {
                return RightMessageTemplate;
            }
            else
            {
                return LeftMessageTemplate;
            }
        }
    }
}
