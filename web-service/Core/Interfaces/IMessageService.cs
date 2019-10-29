using System.Collections.Generic;
using Notifier.Core.Models;

namespace Notifier.Core.Interfaces
{
    public interface IMessageService
    {
        List<Message> Get();
        Message Get(string id);
        Message Create(Message message);
        Message MarkAsSent(Message message);
        void Schedule(Message message);
        void SendToSubscribers(Message message, List<string> subscribers);
    }
}