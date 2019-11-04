using System.Collections.Generic;
using Notifier.Core.Entities;
using Notifier.Core.Gateways;
using Notifier.Core.Interfaces;

namespace Notifier.Core.UseCases
{
    public class ScheduleMessageInteractor
        : IUseCaseInteractor<ScheduleMessageRequest, ScheduleMessageResponse>
    {
        private ISchedulerGateway _scheduler;
        private ISmsGateway _sms;
        private IRepository<string, Message> _messages;
        private IRepository<string, Community> _communities;
        public ScheduleMessageInteractor(
            ISchedulerGateway scheduler,
            ISmsGateway _smsGateway,
            IRepository<string, Message> messageRepository,
            IRepository<string, Community> communityRepository)
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
                .Subscribers;

            _scheduler.Schedule(
                () => SendToSubscribers(message, phoneNumbers),
                request.Delay);

            return new ScheduleMessageResponse();
        }

        private void SendToSubscribers(Message message, IList<string> phoneNumbers)
        {
            foreach(var number in phoneNumbers)
            {
                _sms.SendMessageAsync(message, number);
            }
        }
    }
}