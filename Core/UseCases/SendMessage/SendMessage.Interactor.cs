using System.Linq;
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
        private IRepository<string, Community> _communities;
        public SendMessageInteractor(
            ISmsGateway smsGateway,
            IRepository<string, Message> messages,
            IRepository<string, Community> communities)
        {
            _sms = smsGateway;
            _messages = messages;
            _communities = communities;
        }

        public SendMessageResponse Handle(SendMessageRequest request)
        {
            _messages.Create(request.Message);

            var phoneNumbers = _communities
                .Get(request.Message.CommunityId)
                .Subscribers
                .ToList();

            phoneNumbers.ForEach(phoneNumber =>
                _sms.SendMessageAsync(request.Message, phoneNumber)
            );
            return new SendMessageResponse();
        }
    }
}