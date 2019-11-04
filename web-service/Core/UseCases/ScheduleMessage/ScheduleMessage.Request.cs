using System;
using Notifier.Core.Entities;

namespace Notifier.Core.UseCases
{
    public class ScheduleMessageRequest
    {
        public Message Message { get; set; }
        public TimeSpan Delay
        {
            get => Message.DateTimeToSend - DateTime.UtcNow;
        }
    }
}