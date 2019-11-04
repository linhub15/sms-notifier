namespace Notifier.Core.UseCases
{
    public class ModifyMessageRequest
    {
        public string MessageId { get; set; }   
        public string NewMessageContent { get; set; }
    }
}