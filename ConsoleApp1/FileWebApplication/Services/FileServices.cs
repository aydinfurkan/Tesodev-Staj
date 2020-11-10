using FileWebApplication.DatabaseSettings;
using FileWebApplication.Models;
using FileWebApplication.Repository;
using FileWebApplication.Repository.Interfaces;
using FileWebApplication.Services.Interfaces;

namespace FileWebApplication.Services
{
    public class FileServices : BaseServices<FileModel>, IFileServices
    {
        public FileServices(IMongoContext mongoContext, FwaDatabaseSettings databaseSettings)
        {
            Repository = new FileRepository(mongoContext, databaseSettings.FileCollectionName);
        }

        public sealed override IBaseRepository<FileModel> Repository { get; set; }
    }
}