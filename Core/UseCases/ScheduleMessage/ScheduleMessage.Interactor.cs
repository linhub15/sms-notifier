using System.Linq;
using Notifier.Core.Entities;
using Notifier.Core.Gateways;

namespace Notifier.Core.UseCases
{
    public interface IScheduleMessage
        : IUseCaseInteractor<ScheduleMessageRequest, ScheduleMessageResponse>
    {}

    public class ScheduleMessageInteractor : IScheduleMessage
    {
        private ISchedulerGateway _scheduler;
        private ISmsGateway _sms;
        private IRepositoryGateway<string, Message> _messages;
        private IRepositoryGateway<string, Community> _communities;
        public ScheduleMessageInteractor(
            ISchedulerGateway scheduler,
            ISmsGateway _smsGateway,
            IRepositoryGateway<string, Message> messageRepository,
            IRepositoryGateway<string, Community> communityRepository)
        {
            _scheduler = scheduler;
            _sms = _smsGateway;
            _messages = messageRepository;
            _communities = communityRepository;
        }
        public ScheduleMessageResponse Handle(ScheduleMessageRequest request)
        {
            var message = new Message()
            {
                Content = request.MessageContent,
                DateTimeToSend = request.DateTimeToSend,
                CommunityId = request.CommunityId
            };

            message = _messages.Create(message);

            var phoneNumbers = _communities
                .Get(request.CommunityId)
                .Subscribers
                .ToList();

            var jobId = _scheduler.Schedule(
                () => phoneNumbers.ForEach(
                    phoneNumber => _sms.SendMessageAsync(
                        message,
                        phoneNumber)),
                request.DateTimeToSend);

            message.JobId = jobId;
            _messages.Update(message);

            return new ScheduleMessageResponse();
        }
    }
}