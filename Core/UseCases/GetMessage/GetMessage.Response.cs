using Notifier.Core.Entities;

namespace Notifier.Core.UseCases
{
    public class GetMessageResponse
    {
        public Message Message { get; set; }
        public GetMessageResponse(Message message)
        {
            Message = message;
        }
    }
}