using System;
using System.Collections.Generic;
using System.IO;
using Dms.Models;
using FileWebApplication.Http;
using FileWebApplication.Models;
using FileWebApplication.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IFileServices _fileServices;
        private readonly IStorageServices _storageServices;
        private readonly IUserServices _userServices;

        public UserController(IUserServices userService, IFileServices fileServices, IStorageServices storageServices)
        {
            _userServices = userService;
            _fileServices = fileServices;
            _storageServices = storageServices;
        }

        [HttpGet("all")]
        public ActionResult<List<User>> GetAllUsers()
        {
            return _userServices.GetAll();
        }

        [HttpGet("file/{fileId}", Name = "GetFile")]
        public ActionResult<IFormFile> GetFile(Guid fileId)
        {
            var fileModel = _fileServices.GetById(fileId);

            var fileSrc = _storageServices.Get(fileModel);

            return File(fileSrc, fileModel.ContentType);
        }

        [HttpPost]
        public ActionResult<User> CreateUser(UserDto userDto)
        {
            var newUser = _userServices.Create(new User(userDto));

            return newUser;
        }

        [HttpPost("file/{userId}")]
        public ActionResult<FileModel> CreateFile(IFormFile file, Guid userId)
        {
            var user = _userServices.GetById(userId);
            var fileModel = CreateFileModel(user, new FileModel(file.FileName, file.ContentType, userId));

            _storageServices.Create(fileModel, FormFiletoBytes(file));

            return fileModel;
        }
        
        [HttpPut("file/{userId}")]
        public ActionResult<FileModel> CreateFile(IFormFile file, [FromForm]Guid fileId, Guid userId)
        {
            var user = _userServices.GetById(userId);
            var fileModel = _fileServices.GetById(fileId);
            UpdateFileModel(user, fileModel);
            
            _storageServices.Update(fileModel, FormFiletoBytes(file));

            return fileModel;
            
        }

        private FileModel CreateFileModel(User user, FileModel fileModel)
        {
            fileModel.UpdateBy(user.Id);
            var newFileModel = _fileServices.Create(fileModel);
            user.AddFileId(newFileModel.Id);
            _userServices.Update(user.Id, user);
            return newFileModel;
        }
        private void UpdateFileModel(User user, FileModel fileModel)
        {
            fileModel.UpdateBy(user.Id);
            _fileServices.Update(fileModel.Id, fileModel);
        }
        
        private byte[] FormFiletoBytes(IFormFile file)
        {
            var ms = new MemoryStream();
            file.CopyTo(ms);
            return ms.ToArray();
        }

    }
}