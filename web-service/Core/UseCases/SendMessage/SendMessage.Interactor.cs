using Notifier.Core.Entities;
using Notifier.Core.Gateways;
using Notifier.Core.Interfaces;

namespace Notifier.Core.UseCases
{
    public class SendMessageInteractor
        : IUseCaseInteractor<SendMessageRequest, SendMessageResponse>
    {
        private ISmsGateway _sms;
        private IRepository<string, Message> _messages;
        public SendMessageInteractor(
            ISmsGateway smsGateway,
            IRepository<string, Message> repository)
        {
            _sms = smsGateway;
            _messages = repository;
        }

        public SendMessageResponse Handle(SendMessageRequest request)
        {
            _sms.SendMessageAsync(request.Message, request.ToPhoneNumber);
            _messages.Create(request.Message);
            return new SendMessageResponse();
        }
    }
}