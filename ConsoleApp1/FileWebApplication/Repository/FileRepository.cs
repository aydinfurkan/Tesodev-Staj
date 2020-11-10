using Dms.Models;
using FileWebApplication.Models;
using FileWebApplication.Repository.Interfaces;

namespace FileWebApplication.Repository
{
    public class FileRepository : MongoRepository<FileModel>, IFileRepository
    {
        public FileRepository(IMongoContext mongoContext, string collectionName) : base(mongoContext, collectionName)
        {
        }
    }
}