using Notifier.Core.Entities;
using Notifier.Core.Gateways;

namespace Notifier.Core.UseCases
{
    public class ListMessagesInteractor
        : IUseCaseInteractor<ListMessagesRequest, ListMessagesResponse>
    {
        IRepositoryGateway<string, Message> _messages;
        public ListMessagesInteractor(IRepositoryGateway<string, Message> messages)
        {
            _messages = messages;
        }
        public ListMessagesResponse Handle(ListMessagesRequest request)
        {
            var messages = _messages.List();
            return new ListMessagesResponse()
            {
                Messages = messages
            };
        }
    }
}