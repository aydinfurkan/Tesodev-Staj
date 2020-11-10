using MongoDB.Driver;

namespace FileApi.Repository.Interfaces
{
    public interface IMongoContext
    {
        public IMongoCollection<T> GetCollection<T>(string collectionName);
    }
}