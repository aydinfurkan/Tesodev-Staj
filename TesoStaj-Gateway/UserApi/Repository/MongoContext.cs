using MongoDB.Driver;
using UserApi.DatabaseSettings;
using UserApi.Repository.Interfaces;

namespace UserApi.Repository
{
    public class MongoContext : IMongoContext
    {
        private readonly IMongoClient _mongoClient;
        private readonly IMongoDatabase _mongoDatabase;
        private IMongoContext _mongoContextImplementation;

        public MongoContext(UserDatabaseSettings databaseSettings)
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