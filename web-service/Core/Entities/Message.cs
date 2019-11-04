using System;

namespace Notifier.Core.Entities
{
    public class Message
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public DateTime? DateTimeToSend { get; set; }
        public DateTime? WasSentOn { get; set; }
        public string CommunityId { get; set; }
        public string JobId { get; set; }
    }
}