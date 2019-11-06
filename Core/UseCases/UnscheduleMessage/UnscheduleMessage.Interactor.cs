using Notifier.Core.Entities;
using Notifier.Core.Gateways;

namespace Notifier.Core.UseCases
{
    public class UnscheduleMessageInteractor
        : IUseCaseInteractor<UnscheduleMessageRequest, UnscheduleMessageResponse>
    {
        private ISchedulerGateway _scheduler;
        private IRepositoryGateway<string, Message> _messages;
        public UnscheduleMessageInteractor(
            ISchedulerGateway schedulerGateway,
            IRepositoryGateway<string, Message> messageRepository)
        {
            _scheduler = schedulerGateway;
            _messages = messageRepository;
        }
        public UnscheduleMessageResponse Handle(UnscheduleMessageRequest request)
        {
            var message = _messages.Get(request.MessageId);
            _scheduler.Unschedule(message.JobId);

            message.DateTimeToSend = null;
            _messages.Update(message);

            return new UnscheduleMessageResponse();
        }
    }
}