using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;

namespace WorkerService.Model
{
    public class User : BaseModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public List<Guid> TicketIds { get; set; }

        public User(string username, string password, string email)
        {
            Username = username;
            Password = password;
            Email = email;
            TicketIds = new List<Guid>();
        }
        
        public User(string username, string password, string email, List<Guid> ticketIds)
        {
            Username = username;
            Password = password;
            Email = email;
            TicketIds = ticketIds;
        }

        public override bool Equals(object? obj)
        {
            User usr = obj as User;
            if (usr == null)
                return false;

            bool e;
            if (TicketIds == null || usr.TicketIds == null)
            {
                e = true;
            }
            else
            {
                e = TicketIds.SequenceEqual(usr.TicketIds);
            }
            
            return Id == usr.Id 
                   && Username == usr.Username
                   && Password == usr.Password
                   && Email == usr.Email
                   && e
                   && CreatedTime == usr.CreatedTime
                   && UpdatedTime == usr.UpdatedTime;
        }
    }
}