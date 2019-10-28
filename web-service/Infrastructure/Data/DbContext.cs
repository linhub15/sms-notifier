using System;
using MongoDB.Driver;
using Notifier.Core.Entities;

namespace Notifier.Infrastructure.Data
{
    public interface IDbContext
    {
        IMongoCollection<Message> Messages { get; }
        IMongoCollection<Community> Communities { get; }
    }

    public class DbContext : IDbContext
    {
        public IMongoCollection<Message> Messages { get; }
        public IMongoCollection<Community> Communities { get; }

        public DbContext(string connectionString, string dbName)
        {
            var client = new MongoClient(connectionString);
            if (client == null)
                throw new ArgumentException("Mongo Db connection string invalid");

            var database = client.GetDatabase(dbName);

            Messages = database.GetCollection<Message>("messages");
            Communities = database.GetCollection<Community>("communities");
        }
    }
}