using MongoDB.Driver;

namespace UserApi.Repository.Interfaces
{
    public interface IMongoContext
    {
        public IMongoCollection<T> GetCollection<T>(string collectionName);
    }
}