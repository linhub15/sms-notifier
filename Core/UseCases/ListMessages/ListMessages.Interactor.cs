using Notifier.Core.Entities;
using Notifier.Core.Gateways;

namespace Notifier.Core.UseCases
{
    public interface IListMessages
        : IUseCaseInteractor<ListMessagesRequest, ListMessagesResponse>
    {}
    public class ListMessagesInteractor : IListMessages
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