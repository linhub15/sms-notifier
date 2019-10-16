using System;
using System.Linq.Expressions;
using Hangfire;
using Notifier.Core.Entities;
using Notifier.Core.Interfaces;

namespace Notifier.Infrastructure 
{
    public class MessageScheduler : IMessageScheduler
    {
        private readonly IMessageService _messageService;
        public MessageScheduler(IMessageService messageService)
        {
            _messageService = messageService;
        }
        public void Schedule(Message message, Expression<Action> sendMessage)
        {
            message = _messageService.Create(message);
            
            if (message.DateTimeToSend <= DateTime.Now)
            {
                var executeSendMessage = sendMessage.Compile();
                executeSendMessage();
                return;
            }
            var timeSpanToSend = message.DateTimeToSend - DateTime.Now;
            BackgroundJob.Schedule(sendMessage, timeSpanToSend);
        }
    }
}