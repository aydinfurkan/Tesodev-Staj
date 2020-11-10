﻿using System;
using System.Collections.Generic;
using System.IO;
using FileApi.DatabaseSettings;
using FileApi.Http;
using FileApi.Models;
using FileApi.Services.Interfaces;

namespace FileApi.Services
{
    public class StorageServices : IStorageServices
    {
        private readonly string _baseFolderName;
        private readonly string _defaultFolderName;
        private readonly Dictionary<string, List<string>> _folderNames;

        public StorageServices(FileStorageSettings storageSettings)
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

        public string GetFilePath(FileModel fileModel)
        {
            return FindFilePath(fileModel, FindFolderNameByType(fileModel.ContentType));
        }
        
        private string FindFilePath(FileModel fileModel, string folderName)
        {
            var folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), _baseFolderName, fileModel.OwnerId.ToString(), folderName);
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
            var filePath = Path.Combine(folderPath, fileModel.Id + Path.GetExtension(fileModel.Name));
            return filePath;
        }
    }
}