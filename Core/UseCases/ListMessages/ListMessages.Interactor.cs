using Notifier.Core.Entities;
using Notifier.Core.Interfaces;

namespace Notifier.Core.UseCases
{
    public class ListMessagesInteractor
        : IUseCaseInteractor<ListMessagesRequest, ListMessagesResponse>
    {
        IRepository<string, Message> _messages;
        public ListMessagesInteractor(IRepository<string, Message> messages)
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