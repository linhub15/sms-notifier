using System;
using System.Collections.Generic;
using MongoDB.Driver;
using Notifier.Core.Entities;
using Notifier.Core.Interfaces;

namespace Notifier.Infrastructure.Data
{
    public class MessageService : IMessageService
    {
        private readonly IDbContext _db;
        private readonly IMessageScheduler _scheduler;
        private readonly IMessageSender _smsSender;

        public MessageService(
            IDbContext dbContext,
            IMessageScheduler messageScheduler,
            IMessageSender messageSender)
        {
            _db = dbContext;
            _scheduler = messageScheduler;
            _smsSender = messageSender;
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

        public void Schedule(Message message)
        {
            message = this.Create(message);

            _scheduler.Schedule(
                message,
                () => _smsSender.SendToSubscribers(message));
        }
    }
}