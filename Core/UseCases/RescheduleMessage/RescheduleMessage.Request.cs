using System;

namespace Notifier.Core.UseCases
{
    public class RescheduleMessageRequest
    {
        public string MessageId { get; set; }
        public DateTime NewDateTimeToSend { get; set; }
    }
}