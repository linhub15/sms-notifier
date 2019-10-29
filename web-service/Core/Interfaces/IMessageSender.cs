using System.Collections.Generic;
using System.Threading.Tasks;
using Notifier.Core.Dtos;
using Notifier.Core.Models;

namespace Notifier.Core.Interfaces
{
    public interface IMessageSender
    {
        Task SendAsync(Message message, string toPhoneNumber, IMessageService messageService);
    }
}