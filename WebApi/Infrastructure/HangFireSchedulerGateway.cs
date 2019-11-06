using System;
using System.Linq.Expressions;
using Hangfire;
using Notifier.Core.Gateways;

namespace Notifier.Infrastructure 
{
    public class HangFireSchedulerGateway : ISchedulerGateway
    {
        public string Schedule(Expression<Action> action, DateTime onOrAfter)
        {
            var timeSpanToSend = onOrAfter - DateTime.UtcNow;
            var jobId = BackgroundJob.Schedule(action, timeSpanToSend);
            return jobId;
        }

        public void Unschedule(string jobId)
        {
            if (jobId != null) BackgroundJob.Delete(jobId);
        }
    }
}