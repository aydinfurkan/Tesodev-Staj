﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Gateway.Model
{
    public class User : BaseModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Roles Role { get; set; }
        public List<Guid> FileIds { get; set; }

        public User()
        {
            
        }
        
        public User(UserDto userDto)
        {
            Username = userDto.Username;
            Password = userDto.Password;
            Email = userDto.Email;
            FileIds = new List<Guid>();
            
            Enum.TryParse(userDto.Role, out Roles role);
            Role = role;
        }

        public void Update(UserDto userDto)
        {
            Username = userDto.Username;
            Password = userDto.Password;
            Email = userDto.Email;
            
            Enum.TryParse(userDto.Role, out Roles role);
            Role = role;
        }
    }
}