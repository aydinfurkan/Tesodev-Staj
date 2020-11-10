using FileApi.Models;
using FileApi.Repository.Interfaces;

namespace FileApi.Repository
{
    public class FileRepository : MongoRepository<FileModel>, IFileRepository
    {
        public FileRepository(IMongoContext mongoContext, string collectionName) : base(mongoContext, collectionName)
        {
        }
    }
}