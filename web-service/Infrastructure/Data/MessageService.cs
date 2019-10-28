using System;
using System.Collections.Generic;
using MongoDB.Driver;
using Notifier.Core.Entities;
using Notifier.Core.Interfaces;

namespace Notifier.Infrastructure.Data
{
    public class MessageService : IMessageService
    {
        private readonly DbContext _db;

        public MessageService(DbContext dbContext)
        {
            _db = dbContext;
        }

        public List<Message> Get() => _db.Messages
            .Find(message => true)
            .ToList();
        
        public Message Get(string id) => _db.Messages
            .Find<Message>(message => message.Id == id)
            .FirstOrDefault();

        public Message Create(Message message)
        {
            _db.Messages.InsertOne(message);
            return message;
        }

        public Message MarkAsSent(Message message)
        {
            var filter = Builders<Message>.Filter.Eq("Id", message.Id);
            var update = Builders<Message>.Update.Set("WasSentOn", DateTime.Now);
            _db.Messages.UpdateOne(filter, update);
            return message;
        }
    }
}