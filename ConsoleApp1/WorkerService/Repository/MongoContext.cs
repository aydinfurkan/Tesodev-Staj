using System.Collections.Generic;
using MongoDB.Driver;
using WorkerService.Repository.Interfaces;

namespace WorkerService.Repository
{
    public class MongoContext : IMongoContext
    {
        private readonly IMongoClient _mongoClient;
        private readonly IMongoDatabase _mongoDatabase;
        private IMongoContext _mongoContextImplementation;

        public MongoContext(IMongoClient mongoClient, string databaseName)
        {
            _mongoClient = mongoClient;
            _mongoDatabase = _mongoClient.GetDatabase(databaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _mongoDatabase.GetCollection<T>(collectionName);
        }
    }
}