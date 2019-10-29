using System;
using System.Linq.Expressions;
using Notifier.Core.Models;

namespace Notifier.Core.Interfaces
{
    public interface IMessageScheduler
    {
        void Schedule(Message message, Expression<Action> sendMessage);
    }
}