namespace Notifier.Core.Interfaces
{
    public interface ICommunityService
    {
        void AddSubscriber(string phoneNumber, string communityTag);
    }
    
}