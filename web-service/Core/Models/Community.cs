using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Notifier.Core.Models
{
    public class Community
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string Name { get; set; }
        public List<string> Admins { get; set; }
        public string Tag { get; set; }
        public List<string> Subscribers { get; set; }
        public string SendPhoneNumber { get; set; }
    }
    
}