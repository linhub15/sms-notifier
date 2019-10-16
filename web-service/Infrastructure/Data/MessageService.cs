using System;
using System.Collections.Generic;
using MongoDB.Driver;
using Notifier.Core.Entities;
using Notifier.Core.Interfaces;

namespace Notifier.Infrastructure.Data
{
    public class MessageService : IMessageService
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

        public Message MarkAsSent(Message message)
        {
            var filter = Builders<Message>.Filter.Eq("Id", message.Id);
            var update = Builders<Message>.Update.Set("WasSentOn", DateTime.Now);
            _messages.UpdateOne(filter, update);
            return message;
        }
    }
}