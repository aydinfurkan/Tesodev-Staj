using FileWebApplication.Models;

namespace FileWebApplication.Services.Interfaces
{
    public interface IStorageServices
    {
        public byte[] Get(FileModel fileModel);
        public void Create(FileModel fileModel, byte[] fileSrc);
        public void Update(FileModel fileModel, byte[] fileSrc);
    }
}