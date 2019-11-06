using System.Collections.Generic;
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
            _db.Communities.InsertOne(community);
            return community;
        }

        public string Delete(string tag)
        {
            var filter = Builders<Community>.Filter.Eq("Tag", tag);
            _db.Communities.DeleteOne(filter);
            return tag;
        }

        public Community Get(string tag)
        {
            var filter = Builders<Community>.Filter.Eq("Tag", tag);
            return _db.Communities
                .Find<Community>(filter)
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
            var filter = Builders<Community>.Filter.Eq("Tag", community.Tag);
            _db.Communities.ReplaceOne(filter, community);
            return community;
        }
    }
}