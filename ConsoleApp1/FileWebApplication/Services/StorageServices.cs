using System.Collections.Generic;
using System.IO;
using FileWebApplication.DatabaseSettings;
using FileWebApplication.Http;
using FileWebApplication.Models;
using FileWebApplication.Services.Interfaces;

namespace FileWebApplication.Services
{
    public class StorageServices : IStorageServices
    {
        private readonly string _baseFolderName;
        private readonly string _defaultFolderName;
        private readonly Dictionary<string, List<string>> _folderNames;

        public StorageServices(FwaStorageSettings storageSettings)
        {
            _baseFolderName = storageSettings.BaseFolderName;
            _defaultFolderName = storageSettings.DefaultFolderName;
            _folderNames = storageSettings.FolderNames;
        }

        public byte[] Get(FileModel fileModel)
        {
            var filePath = FindFilePath(fileModel, FindFolderNameByType(fileModel.ContentType));
            
            if (!File.Exists(filePath)) throw new HttpNotFound("File");

            return File.ReadAllBytes(filePath);

        }

        public void Create(FileModel fileModel, byte[] fileSrc)
        {
            var filePath = FindFilePath(fileModel, FindFolderNameByType(fileModel.ContentType));

            File.WriteAllBytes(filePath, fileSrc);
        }

        public void Update(FileModel fileModel, byte[] fileSrc)
        {
            var filePath = FindFilePath(fileModel, FindFolderNameByType(fileModel.ContentType));
            
            if (!File.Exists(filePath)) throw new HttpNotFound("File");
            
            File.WriteAllBytes(filePath, fileSrc);
        }

        private string FindFolderNameByType(string mimeType)
        {
            foreach (var (key,value) in _folderNames)
            {
                if (value.Contains(mimeType))
                {
                    return key;
                }
            }

            return _defaultFolderName;
        }
        
        private string FindFilePath(FileModel fileModel, string folderName)
        {
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), _baseFolderName, fileModel.OwnerId.ToString(), folderName);
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
            var filePath = Path.Combine(folderPath, fileModel.Id + Path.GetExtension(fileModel.Name));
            return filePath;
        }
    }
}