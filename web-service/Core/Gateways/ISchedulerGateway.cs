using System;
using System.Linq.Expressions;

namespace Notifier.Core.Gateways
{
    public interface ISchedulerGateway
    {
        string Schedule(Expression<Action> action, DateTime onOrAfter);
        void Unschedule(string jobId);
    }
}