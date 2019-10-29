using System.Collections.Generic;
using Notifier.Core.Entities;

namespace Notifier.Core.Interfaces
{
    public interface IMessageService
    {
        List<Message> Get();
        Message Get(string id);
        Message Create(Message message);
        Message MarkAsSent(Message message);
        void Schedule(Message message);
    }
}