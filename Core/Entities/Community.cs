using System.Collections.Generic;

namespace Notifier.Core.Entities
{
    public class Community
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IList<string> Admins { get; set; }
        public string Tag { get; set; }
        public IList<string> Subscribers { get; set;}
        public string SendPhoneNumber { get; set; }

        public Community()
        {
            Admins = new List<string>();
            Subscribers = new List<string>();
        }
    }
    
}