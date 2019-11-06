using Notifier.Core.Entities;
using Notifier.Core.Gateways;

namespace Notifier.Core.UseCases
{
    public class GetMessageInteractor
        : IUseCaseInteractor<GetMessageRequest, GetMessageResponse>
    {
        private IRepositoryGateway<string, Message> _messages;
        public GetMessageInteractor(IRepositoryGateway<string, Message> messages)
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