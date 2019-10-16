using Notifier.Core.Entities;

namespace Notifier.Core.Interfaces
{
    public interface IMessageSender
    {
        void SendMessage();
        void SendMessage(Message message);
    }
}