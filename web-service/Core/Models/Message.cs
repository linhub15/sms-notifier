using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Notifier.Core.Models
{
    public class Message
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Content { get; set; }
        public DateTime DateTimeToSend { get; set; }
        public DateTime? WasSentOn { get; set; }
        public string CommunityId { get; set; }
    }
}