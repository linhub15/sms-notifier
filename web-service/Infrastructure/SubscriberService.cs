using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using Notifier.Core.Dtos;
using Notifier.Core.Models;
using Notifier.Core.Interfaces;
using Notifier.Infrastructure.Data;

namespace Notifier.Infrastructure
{
    public class SubscriberService : ISubscriberService
    {
        private readonly IDbContext _db;

        public SubscriberService(IDbContext dbContext)
        {
            _db = dbContext;
        }

        public List<string> GetSubscribers(string communityTag)
        {
            var filter = Builders<Community>.Filter
                .Eq(community => community.Tag, communityTag);

            return _db.Communities
                .Find<Community>(filter)
                .Limit(1)
                .Single()
                .Subscribers;
        }

        public void AddSubscriber(string phoneNumber, string communityTag)
        {
            var filter = Builders<Community>.Filter
                .Eq("Tag", communityTag);
            var update = Builders<Community>.Update
                .Push("Subscribers", phoneNumber);
            _db.Communities.UpdateOne(filter, update);
        }


        public void RemoveSubscriber(SubscribeDto subscriber)
        {
            var communityFilter = Builders<Community>.Filter
                .Eq("Tag", subscriber.CommunityTag);
            var pullFilter = Builders<Community>.Update
                .Pull("Subscribers", subscriber.PhoneNumber);

            _db.Communities.UpdateOne(communityFilter, pullFilter);
        }
    }
}