using System.Collections.Generic;
using Notifier.Core.Entities;

namespace Notifier.Core.UseCases
{
    public class ListMessagesResponse
    {
        public IList<Message> Messages { get; set; }
    }
}