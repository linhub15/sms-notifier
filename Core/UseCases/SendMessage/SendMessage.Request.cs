using Notifier.Core.Entities;

namespace Notifier.Core.UseCases
{
    public class SendMessageRequest
    {
        public string ToPhoneNumber { get; set; }
        public Message Message { get; set; }
    }
}