using System.Collections.Generic;
using MongoDB.Driver;
using Notifier.Core.Entities;

namespace Notifier.Infrastructure.Data
{
    public class MessageService
    {
        private readonly IMongoCollection<Message> _messages;
        public MessageService()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("notifier");

            _messages = database.GetCollection<Message>("messages");
        }

        public List<Message> Get() => _messages
            .Find(message => true)
            .ToList();
        
        public Message Get(string id) => _messages
            .Find<Message>(message => message.Id == id)
            .FirstOrDefault();

        public Message Create(Message message)
        {
            _messages.InsertOne(message);
            return message;
        }
    }
}