﻿using FileApi.DatabaseSettings;
using FileApi.Models;
using FileApi.Repository;
using FileApi.Repository.Interfaces;
using FileApi.Services.Interfaces;

namespace FileApi.Services
{
    public class FileServices : BaseServices<FileModel>, IFileServices
    {
        public FileServices(IMongoContext mongoContext, FileDatabaseSettings databaseSettings)
        {
            Repository = new FileRepository(mongoContext, databaseSettings.FileCollectionName);
        }

        public sealed override IBaseRepository<FileModel> Repository { get; set; }
    }
}