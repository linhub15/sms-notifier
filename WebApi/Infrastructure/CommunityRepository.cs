using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using Notifier.Core.Entities;
using Notifier.Core.Interfaces;
using Notifier.Infrastructure.Data;

namespace Notifier.Infrastructure
{
    public class CommunityRepository : IRepository<string, Community>
    {
        IDbContext _db;
        public CommunityRepository(IDbContext dbContext)
        {
            _db = dbContext;
        }
        public Community Create(Community community)
        {
            community.Id = ObjectId.GenerateNewId().ToString();
            _db.Communities.InsertOne(community);
            return community;
        }

        public string Delete(string id)
        {
            var filter = Builders<Community>.Filter.Eq("Id", id);
            _db.Communities.DeleteOne(filter);
            return id;
        }

        public Community Get(string id)
        {
            return _db.Communities
                .Find<Community>(community => community.Id == id)
                .FirstOrDefault();
        }

        public IList<Community> List()
        {
            return _db.Communities
                .Find(community => true)
                .ToList();
        }

        public Community Update(Community community)
        {
            var filter = Builders<Community>.Filter.Eq("Id", community.Id);
            _db.Communities.ReplaceOne(filter, community);
            return community;
        }
    }
}