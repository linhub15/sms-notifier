using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Notifier.Core.Entities
{
    public class Message
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string PhoneNumber { get; set; }

        public string Content { get; set; }
    }
}