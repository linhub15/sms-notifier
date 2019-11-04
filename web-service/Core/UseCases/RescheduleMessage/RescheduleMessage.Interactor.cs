using System.Linq;
using Notifier.Core.Entities;
using Notifier.Core.Gateways;
using Notifier.Core.Interfaces;

namespace Notifier.Core.UseCases
{
    public class RescheduleMessageInteractor
        : IUseCaseInteractor<RescheduleMessageRequest, RescheduleMessageResponse>
    {
        private ISchedulerGateway _scheduler;
        private ISmsGateway _sms;
        private IRepository<string, Message> _messages;
        private IRepository<string, Community> _communities;
        public RescheduleMessageInteractor(
            ISchedulerGateway schedulerGateway,
            ISmsGateway smsGateway,
            IRepository<string, Message> messageRepository,
            IRepository<string, Community> communityRepository)
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
                request.Delay);

            return new RescheduleMessageResponse();
        }
    }
}