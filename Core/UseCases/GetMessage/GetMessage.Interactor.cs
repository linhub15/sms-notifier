using Notifier.Core.Entities;
using Notifier.Core.Interfaces;

namespace Notifier.Core.UseCases
{
    public class GetMessageInteractor
        : IUseCaseInteractor<GetMessageRequest, GetMessageResponse>
    {
        private IRepository<string, Message> _messages;
        public GetMessageInteractor(IRepository<string, Message> messages)
        {
            _messages = messages;
        }
        public GetMessageResponse Handle(GetMessageRequest request)
        {
            var message = _messages.Get(request.MessageId);
            return new GetMessageResponse(message);
        }
    }
}