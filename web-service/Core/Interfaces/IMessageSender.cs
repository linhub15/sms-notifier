using System.Threading.Tasks;
using Notifier.Core.Entities;

namespace Notifier.Core.Interfaces
{
    public interface IMessageSender
    {
        void SendToSubscribers(Message message);

        Task SendAsync(Message message, string toPhoneNumber, IMessageService messageService);
    }
}