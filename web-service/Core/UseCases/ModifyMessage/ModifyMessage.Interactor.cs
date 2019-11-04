using Notifier.Core.Entities;
using Notifier.Core.Interfaces;

namespace Notifier.Core.UseCases
{
    public class ModifyMessageInteractor
        : IUseCaseInteractor<ModifyMessageRequest, ModifyMessageResponse>
    {
        private IRepository<string, Message> _messages;
        public ModifyMessageInteractor(IRepository<string, Message> repository)
        {
            _messages = repository;
        }
        public ModifyMessageResponse Handle(ModifyMessageRequest request)
        {
            var message = _messages.Get(request.MessageId);
            _messages.Update(message);

            return new ModifyMessageResponse();
        }
    }
}