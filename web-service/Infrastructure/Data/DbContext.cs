using System;
using MongoDB.Driver;
using Notifier.Core.Entities;

namespace Notifier.Infrastructure.Data
{
    public class DbContext
    {
        public readonly IMongoCollection<Message> Messages;
        public readonly IMongoCollection<Community> Communities;

        public DbContext(string connectionString, string dbName)
        {
            var client = new MongoClient(connectionString);
            if (client == null)
                throw new ArgumentException("Mongo Db connection string invalid");

            var database = client.GetDatabase(dbName);

            Messages = database.GetCollection<Message>("message");
            Communities = database.GetCollection<Community>("community");
        }
    }
}