using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using UserApi.Models;
using UserApi.Services.Interfaces;

namespace UserApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userService;

        public UserController(IUserServices userService)
        {
            _userService = userService;
        }

        [HttpGet("all")]
        public ActionResult<List<User>> GetAll()
        {
            return _userService.GetAll();
        }
        
        [HttpGet("{userId}", Name = "GetUser")]
        public ActionResult<User> GetById(Guid userId)
        {
            return _userService.GetById(userId);
        }
        
        [HttpPost]
        public ActionResult<User> Create(UserDto userDto)
        {
            var newUser = _userService.Create(new User(userDto));

            return CreatedAtRoute("GetUser", new { userId = newUser.Id.ToString() }, newUser);
        }
        
        [HttpPut("{userId}")]
        public ActionResult<User> Update(Guid userId, UserDto userDto)
        {
            var user = _userService.GetById(userId);

            user.Update(userDto);
            _userService.Update(userId, user);

            return user;
        }
        
        [HttpPut("fileIds/{userId}")]
        public ActionResult<User> AddFileId(Guid userId, [FromForm]Guid fileId)
        {
            var user = _userService.GetById(userId);

            user.FileIds.Add(fileId);
            _userService.Update(userId, user);

            return user;
        }
        
        [HttpDelete("{userId}")]
        public IActionResult Delete(Guid userId)
        {
            var user = _userService.GetById(userId);

            _userService.Remove(user.Id);

            return NoContent();
        }
        
        [HttpDelete("all")]
        public IActionResult Delete()
        {
            _userService.RemoveAll();
            return NoContent();
        }
        
    }
}