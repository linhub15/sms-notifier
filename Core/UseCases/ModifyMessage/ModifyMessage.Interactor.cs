using Notifier.Core.Entities;
using Notifier.Core.Gateways;

namespace Notifier.Core.UseCases
{
    public class ModifyMessageInteractor
        : IUseCaseInteractor<ModifyMessageRequest, ModifyMessageResponse>
    {
        private IRepositoryGateway<string, Message> _messages;
        public ModifyMessageInteractor(IRepositoryGateway<string, Message> repository)
        {
            _messages = repository;
        }
        public ModifyMessageResponse Handle(ModifyMessageRequest request)
        {
            var message = _messages.Get(request.MessageId);
            message.Content = request.NewMessageContent;
            _messages.Update(message);

            return new ModifyMessageResponse();
        }
    }
}