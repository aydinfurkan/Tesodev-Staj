using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Gateway.Model;
using Gateway.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Gateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GatewayController : ControllerBase
    {
        private readonly IFileServices _fileServices;
        private readonly IUserServices _userServices;
        
        public GatewayController(IFileServices fileServices, IUserServices userServices)
        {
            _fileServices = fileServices;
            _userServices = userServices;
        }

        [HttpGet("user/{userId}")]
        public async Task<User> GetUserModelAsync(Guid userId)
        {
            return await _userServices.GetModelAsync("api/user/" + userId);
        }
        [HttpGet("user/all")]
        public async Task<List<User>> GetAllUserAsync()
        {
            return await _userServices.GetModelArrayAsync("api/user/all");
        }
        [HttpGet("file/{fileId}")]
        public async Task<FileModel> GetFileModelAsync(Guid fileId)
        {
            return await _fileServices.GetModelAsync("api/file/" + fileId);
        }
        [HttpGet("file/src/{fileId}")]
        public async Task<ActionResult> GetFileSrcAsync(Guid fileId)
        {
            var fileSrc= await _fileServices.GetFileSrcAsync("api/file/src/" + fileId);

            return File(fileSrc.Key, fileSrc.Value);
        }
        [HttpPost("user")]
        public async Task<User> CreateUserAsync(UserDto userDto)
        {
            var httpContent = CreateStringContent(userDto);
            
            return await _userServices.PostAsync("api/user", httpContent);
        }

        [HttpPost("file/{userId}")]
        public async Task<FileModel> CreateFileAsync(IFormFile file, Guid userId)
        {
            var multipartFileContent = CreateMultipartContentByFile("file", file);
            var fileModel = await _fileServices.PostAsync("api/file/" + userId, multipartFileContent);
            
            var multipartStrContent = CreateMultipartContentByString("fileId", fileModel.Id.ToString());
            await _userServices.PutAsync("api/user/fileIds/" + userId, multipartStrContent);

            return fileModel;
        }

        [HttpPut("user/{userId}")]
        public async Task<User> UpdateUserAsync(Guid userId, UserDto userDto)
        {
            var httpContent = CreateStringContent(userDto);
            
            return await _userServices.PutAsync("api/user/" + userId, httpContent);
        }

        private HttpContent CreateStringContent(object obj)
        {
            var stringObj = JsonConvert.SerializeObject(obj);
            return new StringContent(stringObj, Encoding.UTF8, "application/json");
        }

        private HttpContent CreateMultipartContentByFile(string name, IFormFile formFile)
        {
            var fileContent = new ByteArrayContent(ConvertFormFiletoBytes(formFile));
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(formFile.ContentType);
            return new MultipartFormDataContent {{fileContent, name, formFile.FileName}};
        }
        
        private HttpContent CreateMultipartContentByString(string name, string str)
        {
            return new MultipartFormDataContent {{new StringContent(str), name}};
        }
        
        private byte[] ConvertFormFiletoBytes(IFormFile file)
        {
            var ms = new MemoryStream();
            file.CopyTo(ms);
            return ms.ToArray();
        }

        /*[HttpPost("login")]
        public IActionResult Login(string username, string password)
        {
            var user = _userServices.GetByUsernameAndPassword(username, password);
            
            var jwt = JwtHelper.CreateJwt(new []
            {
                new Claim("id", user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            }, 30);
            
            HttpContext.Session.SetString("jwt", jwt);

            return Ok();
        }
        
        private Guid GetUserIdFromJwt(string jwt)
        {
            var userIdStr = JwtHelper.GetClaims(jwt).First(x => x.Type == "id").Value;;
            return Guid.Parse(userIdStr);
        }*/
        
        
    }
}