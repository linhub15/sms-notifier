using System.Collections.Generic;
using Notifier.Core.Dtos;

namespace Notifier.Core.Interfaces
{
    public interface ICommunityService
    {
        void AddSubscriber(string phoneNumber, string communityTag);
        List<string> GetSubscribers(string communityTag);

        void RemoveSubscriber(SubscribeDto subscriber);
    }
    
}