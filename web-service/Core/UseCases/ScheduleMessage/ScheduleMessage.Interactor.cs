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
            // TODO(Hubert): Create message from DTO because many properties are not required to pass in;
            var message = request.Message;

            message = _messages.Create(message);

            var phoneNumbers = _communities
                .Get(request.Message.CommunityId)
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