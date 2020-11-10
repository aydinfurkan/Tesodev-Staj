﻿using System;
using System.Collections.Generic;
 using FileWebApplication.Models;

 namespace Dms.Models
{
    public class User : BaseModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public List<Guid> FileIds { get; set; }

        public User(UserDto userDto)
        {
            Username = userDto.Username;
            Password = userDto.Password;
            Email = userDto.Email;
            FileIds = new List<Guid>();
        }
        
        public void AddFileId(Guid fileId)
        {
            FileIds.Add(fileId);
        }
    }
}