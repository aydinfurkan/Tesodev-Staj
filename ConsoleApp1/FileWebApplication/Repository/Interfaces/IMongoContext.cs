using MongoDB.Driver;

namespace FileWebApplication.Repository.Interfaces
{
    public interface IMongoContext
    {
        public IMongoCollection<T> GetCollection<T>(string collectionName);
    }
}