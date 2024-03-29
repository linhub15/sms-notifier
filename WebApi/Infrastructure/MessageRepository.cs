using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using Notifier.Core.Entities;
using Notifier.Core.Gateways;
using Notifier.Infrastructure.Data;

namespace Notifier.Infrastructure
{
    public class MessageRepository : IRepositoryGateway<string, Message>
    {
        IDbContext _db;
        public MessageRepository(IDbContext dbContext)
        {
            _db = dbContext;
        }
        public Message Create(Message message)
        {
            message.Id = ObjectId.GenerateNewId().ToString();
            _db.Messages.InsertOne(message);
            return message;
        }

        public string Delete(string id)
        {
            var filter = Builders<Message>.Filter.Eq("Id", id);
            _db.Messages.DeleteOne(filter);
            return id;
        }

        public Message Get(string id)
        {
            return _db.Messages
                .Find<Message>(message => message.Id == id)
                .FirstOrDefault();
        }

        public IList<Message> List()
        {
            return _db.Messages
                .Find(message => true)
                .ToList();
        }

        public Message Update(Message message)
        {
            var filter = Builders<Message>.Filter.Eq("Id", message.Id);
            _db.Messages.ReplaceOne(filter, message);
            return message;
        }
    }
}