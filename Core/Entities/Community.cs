using System.Collections.Generic;

namespace Notifier.Core.Entities
{
    public class Community
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Tag { get; set; }
        public IList<string> Subscribers { get; set;}

        public Community()
        {
            Subscribers = new List<string>();
        }
    }
    
}