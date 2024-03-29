using System.Linq;
using Notifier.Core.Entities;
using Notifier.Core.Gateways;

namespace Notifier.Core.UseCases
{
    public interface ISendMessage
        : IUseCaseInteractor<SendMessageRequest, SendMessageResponse>
    {}

    public class SendMessageInteractor : ISendMessage
    {
        private ISmsGateway _sms;
        private IRepositoryGateway<string, Message> _messages;
        private IRepositoryGateway<string, Community> _communities;
        public SendMessageInteractor(
            ISmsGateway smsGateway,
            IRepositoryGateway<string, Message> messages,
            IRepositoryGateway<string, Community> communities)
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