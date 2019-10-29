using System.Collections.Generic;
using Notifier.Core.Dtos;

namespace Notifier.Core.Interfaces
{
    public interface ISubscriberService
    {
        List<string> GetSubscribers(string communityTag);
        void AddSubscriber(string phoneNumber, string communityTag);
        void RemoveSubscriber(SubscribeDto subscriber);
    }
}