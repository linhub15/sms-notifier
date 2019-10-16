using System;
using System.Linq.Expressions;
using Notifier.Core.Entities;

namespace Notifier.Core.Interfaces
{
    public interface IMessageScheduler
    {
        void Schedule(Message message, Expression<Action> sendMessage);
    }
}