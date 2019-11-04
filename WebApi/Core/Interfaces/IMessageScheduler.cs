using System;
using System.Linq.Expressions;
using Notifier.Core.Models;

namespace Notifier.Core.Interfaces
{
    public interface IMessageScheduler
    {
        string Schedule(Message message, Expression<Action> sendMessage);
        void Unschedule(string jobId);
    }
}