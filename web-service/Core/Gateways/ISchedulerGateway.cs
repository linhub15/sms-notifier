using System;
using System.Linq.Expressions;

namespace Notifier.Core.Gateways
{
    public interface ISchedulerGateway
    {
        string Schedule(Expression<Action> action, TimeSpan delay);
    }
}