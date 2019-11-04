namespace Notifier.Core.UseCases
{
    public class UnsubscribeRequest
    {
        public string CommunityId { get; set; }
        public string PhoneNumber { get; set; }
    }
}