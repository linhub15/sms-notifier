using System;
using System.Linq.Expressions;
using Hangfire;
using Notifier.Core.Models;
using Notifier.Core.Interfaces;

namespace Notifier.Infrastructure 
{
    public class HangFireScheduler : IMessageScheduler
    {
        public string Schedule(Message message, Expression<Action> sendMessage)
        {                    
            // scheduled time has passed - send right away
            if (message.DateTimeToSend <= DateTime.UtcNow)
            {
                var executeSendMessage = sendMessage.Compile();
                executeSendMessage();
                return null;
            }
            var timeSpanToSend = message.DateTimeToSend - DateTime.UtcNow;
            var jobId = BackgroundJob.Schedule(sendMessage, timeSpanToSend);
            return jobId;
        }

        public void Unschedule(string jobId)
        {
            if (jobId != null) BackgroundJob.Delete(jobId);
        }
    }
}