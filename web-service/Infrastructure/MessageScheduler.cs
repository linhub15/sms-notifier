using System;
using System.Linq.Expressions;
using Hangfire;
using Notifier.Core.Entities;
using Notifier.Core.Interfaces;

namespace Notifier.Infrastructure 
{
    public class MessageScheduler : IMessageScheduler
    {
        public void Schedule(Message message, Expression<Action> sendMessage)
        {                    
            // scheduled time has passed - send right away
            if (message.DateTimeToSend <= DateTime.UtcNow)
            {
                var executeSendMessage = sendMessage.Compile();
                executeSendMessage();
                return;
            }
            var timeSpanToSend = message.DateTimeToSend - DateTime.UtcNow;
            BackgroundJob.Schedule(sendMessage, timeSpanToSend);
        }
    }
}