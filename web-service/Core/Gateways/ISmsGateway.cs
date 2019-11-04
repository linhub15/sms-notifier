using System.Threading.Tasks;
using Notifier.Core.Entities;

namespace Notifier.Core.Gateways
{
    public interface ISmsGateway
    {
        Task SendMessageAsync(Message message, string toPhoneNumber);
    }
}