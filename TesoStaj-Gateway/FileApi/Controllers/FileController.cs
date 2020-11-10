﻿using System;
using System.IO;
using FileApi.Http;
using FileApi.Models;
using FileApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileServices _fileServices;
        private readonly IStorageServices _storageServices;

        public FileController(IFileServices fileServices, IStorageServices storageServices)
        {
            _fileServices = fileServices;
            _storageServices = storageServices;
        }

        [HttpGet("{fileId}", Name = "GetFile")]
        public ActionResult<FileModel> GetFile(Guid fileId)
        {
            return _fileServices.GetById(fileId);
        }
        
        [HttpGet("src/{fileId}", Name = "GetFileSrc")]
        public ActionResult<IFormFile> GetFileSrc(Guid fileId)
        {
            var fileModel = _fileServices.GetById(fileId);

            var fileSrc = _storageServices.Get(fileModel);

            return File(fileSrc, fileModel.ContentType);
        }
        
        [HttpPost("{userId}")]
        public ActionResult<FileModel> CreateFile(IFormFile file, Guid userId)
        {
            if(file == null) throw new HttpBadRequest(); // TODO validator ?
            
            var fileModel = _fileServices.Create(new FileModel(file.FileName, file.ContentType, userId));

            _storageServices.Create(fileModel, ConvertFormFiletoBytes(file));

            return fileModel;
        }
        
        [HttpGet("path/{fileId}")]
        public ActionResult<string> GetFilePath(Guid fileId)
        {
            var fileModel = _fileServices.GetById(fileId);

            return _storageServices.GetFilePath(fileModel);
        }
        

        private byte[] ConvertFormFiletoBytes(IFormFile file)
        {
            var ms = new MemoryStream();
            file.CopyTo(ms);
            return ms.ToArray();
        }
        

        
    }
}