using MongoDB.Driver;
using Notifier.Core.Entities;
using Notifier.Core.Interfaces;

namespace Notifier.Infrastructure.Data
{
    public class CommunityService : ICommunityService
    {
        private readonly IMongoCollection<Community> _communities;
        
        public CommunityService()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var databse = client.GetDatabase("notifier");

            _communities = databse.GetCollection<Community>("communities");
        }

        public void AddSubscriber(string phoneNumber, string communityTag)
        {
            var filter = Builders<Community>.Filter.Eq("Tag", communityTag);
            var update = Builders<Community>.Update.Push("Subscribers", phoneNumber);
            _communities.UpdateOne(filter, update);
        }
    }
}