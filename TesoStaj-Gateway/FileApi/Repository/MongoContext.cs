using FileApi.DatabaseSettings;
using FileApi.Repository.Interfaces;
using MongoDB.Driver;

namespace FileApi.Repository
{
    public class MongoContext : IMongoContext
    {
        private readonly IMongoClient _mongoClient;
        private readonly IMongoDatabase _mongoDatabase;
        private IMongoContext _mongoContextImplementation;

        public MongoContext(FileDatabaseSettings databaseSettings)
        {
            _mongoClient = new MongoClient(databaseSettings.ConnectionString);
            _mongoDatabase = _mongoClient.GetDatabase(databaseSettings.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _mongoDatabase.GetCollection<T>(collectionName);
        }
    }
}