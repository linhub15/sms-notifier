using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using Notifier.Core.Dtos;
using Notifier.Core.Entities;
using Notifier.Core.Interfaces;

namespace Notifier.Infrastructure.Data
{
    public class CommunityService : ICommunityService
    {
        private readonly IDbContext _db;

        public CommunityService(IDbContext dbContext)
        {
            _db = dbContext;
        }

        public void AddSubscriber(string phoneNumber, string communityTag)
        {
            var filter = Builders<Community>.Filter
                .Eq("Tag", communityTag);
            var update = Builders<Community>.Update
                .Push("Subscribers", phoneNumber);
            _db.Communities.UpdateOne(filter, update);
        }

        public List<string> GetSubscribers(string communityTag)
        {
            var filter = Builders<Community>.Filter
                .Eq(community => community.Tag, communityTag);

            return  _db.Communities
                .Find<Community>(filter)
                .Limit(1)
                .Single()
                .Subscribers;
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