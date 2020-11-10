using FileApi.Models;

namespace FileApi.Services.Interfaces
{
    public interface IStorageServices
    {
        public byte[] Get(FileModel fileModel);
        public void Create(FileModel fileModel, byte[] fileSrc);

        public string GetFilePath(FileModel fileModel);
    }
}