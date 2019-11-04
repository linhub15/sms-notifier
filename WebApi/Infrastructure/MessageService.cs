using System;
using System.Collections.Generic;
using MongoDB.Driver;
using Notifier.Core.Models;
using Notifier.Core.Interfaces;
using Notifier.Infrastructure.Data;

namespace Notifier.Infrastructure
{
    public class MessageService : IMessageService
    {
        private readonly IDbContext _db;
        private readonly IMessageScheduler _scheduler;
        private readonly IMessageSender _smsSender;
        private readonly ISubscriberService _subscriberService;

        public MessageService(
            IDbContext dbContext,
            IMessageScheduler messageScheduler,
            IMessageSender messageSender,
            ISubscriberService subscriberService)
        {
            _db = dbContext;
            _scheduler = messageScheduler;
            _smsSender = messageSender;
            _subscriberService = subscriberService;
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

        public void Delete(string messageId)
        {
            var message = Get(messageId);
            var filter = Builders<Message>.Filter.Eq("Id", messageId);
            _db.Messages.DeleteOne(filter);
            _scheduler.Unschedule(message.JobId);
        }

        public void SetJobId(string id, string jobId)
        {
            var message = Get(id);

            var filter = Builders<Message>.Filter.Eq("Id", id);
            var update = Builders<Message>.Update.Set("JobId", jobId);
            _db.Messages.UpdateOne(filter, update);
        }

        public Message UpdateContent(string messageId, string content)
        {
            var message = Get(messageId);
            // if the message was already sent, you can't update content
            if (message.WasSentOn.HasValue)
            {
                return null;
            }
            var filter = Builders<Message>.Filter.Eq("Id", messageId);
            var update = Builders<Message>.Update.Set("Content", content);
            _db.Messages.UpdateOne(filter, update);

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
            message = Create(message);

            // TODO(Hubert): potential circular dependency 
            // Use private method, or move code else where
            // Repository pattern??
            var subscribers = _subscriberService
                .GetSubscribers(message.CommunityId);

            var jobId = _scheduler.Schedule(
                message,
                () => SendToSubscribers(message.Id, subscribers));

            SetJobId(message.Id, jobId);
        }

        public void SendToSubscribers(string messageId, List<string> phoneNumbers)
        {
            var message = Get(messageId);

            foreach (string phoneNumber in phoneNumbers)
            {
                _smsSender.SendAsync(message, phoneNumber, this);
            }
        }
    }
}