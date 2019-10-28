using System.Collections.Generic;

namespace Notifier.Core.Interfaces
{
    public interface ICommunityService
    {
        void AddSubscriber(string phoneNumber, string communityTag);
         List<string> GetSubscribers(string communityTag);

    }
    
}