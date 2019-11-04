using System;

namespace Notifier.Core.UseCases
{
    public class ScheduleMessageRequest
    {
        public string MessageContent { get; set; }
        public string CommunityId { get; set; }
        public DateTime DateTimeToSend { get; set; }
    }
}