using System.Linq;
using Notifier.Core.Entities;
using Notifier.Core.Gateways;

namespace Notifier.Core.UseCases
{
    public interface IRescheduleMessage
        : IUseCaseInteractor<RescheduleMessageRequest, RescheduleMessageResponse>
    {}

    public class RescheduleMessageInteractor : IRescheduleMessage
    {
        private ISchedulerGateway _scheduler;
        private ISmsGateway _sms;
        private IRepositoryGateway<string, Message> _messages;
        private IRepositoryGateway<string, Community> _communities;
        public RescheduleMessageInteractor(
            ISchedulerGateway schedulerGateway,
            ISmsGateway smsGateway,
            IRepositoryGateway<string, Message> messageRepository,
            IRepositoryGateway<string, Community> communityRepository)
        {
            _scheduler = schedulerGateway;
            _sms = smsGateway;
            _messages = messageRepository;
            _communities = communityRepository;
        }
        public RescheduleMessageResponse Handle(RescheduleMessageRequest request)
        {
            var message = _messages.Get(request.MessageId);
            message.DateTimeToSend = request.NewDateTimeToSend;
            _messages.Update(message);

            var phoneNumbers = _communities
                .Get(message.CommunityId)
                .Subscribers
                .ToList();

            _scheduler.Schedule(
                () => phoneNumbers.ForEach(
                    phoneNumber => _sms.SendMessageAsync(
                        message,
                        phoneNumber)),
                request.NewDateTimeToSend);

            return new RescheduleMessageResponse();
        }
    }
}