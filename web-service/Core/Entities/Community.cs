using System.Collections.Generic;

namespace Notifier.Core.Entities
{
    public class Community
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IList<string> Admins { get; }
        public string Tag { get; set; }
        public IList<string> Subscribers { get; }
        public string SendPhoneNumber { get; set; }

        public Community()
        {
            Admins = new List<string>();
            Subscribers = new List<string>();
        }
    }
    
}