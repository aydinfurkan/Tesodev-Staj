using System.Collections.Generic;
using MongoDB.Driver;

namespace WorkerService.Repository.Interfaces
{
    public interface IMongoContext
    {
        public IMongoCollection<T> GetCollection<T>(string collectionName);
    }
}