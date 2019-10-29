using System;
using System.Collections.Generic;
using MongoDB.Driver;
using Notifier.Core.Models;
using Notifier.Core.Interfaces;
using System.Threading.Tasks;

namespace Notifier.Infrastructure.Data
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

            // Get subscribers
            var subscribers = _subscriberService
                .GetSubscribers(message.CommunityId);

            _scheduler.Schedule(
                message,
                () => SendToSubscribers(message, subscribers));
        }

        public async void SendToSubscribers(Message message, List<string> phoneNumbers)
        {
            List<Task> listOfTasks = new List<Task>();

            foreach (string phoneNumber in phoneNumbers)
            {
                listOfTasks.Add(_smsSender.SendAsync(message, phoneNumber, this));
            };
            await Task.WhenAll(listOfTasks);
        }
    }
}