using MongoDB.Driver;
using Notifier.Core.Entities;
using Notifier.Core.Interfaces;

namespace Notifier.Infrastructure.Data
{
    public class CommunityService : ICommunityService
    {
        private readonly DbContext _db;
        
        public CommunityService(DbContext dbContext)
        {
            _db = dbContext;
        }

        public void AddSubscriber(string phoneNumber, string communityTag)
        {
            var filter = Builders<Community>.Filter.Eq("Tag", communityTag);
            var update = Builders<Community>.Update.Push("Subscribers", phoneNumber);
            _db.Communities.UpdateOne(filter, update);
        }
    }
}