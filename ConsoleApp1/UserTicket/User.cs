using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace ConsoleApp3
{
    public class User : UserLevel
    {
        [BsonId]
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public List<Guid> TicketIds { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }

        public User(Guid id, string username, string password, string email, string state) : base(state)
        {
            Id = id;
            Username = username;
            Password = password;
            Email = email;
            TicketIds = new List<Guid>();
            CreatedTime = DateTime.Now;
            UpdatedTime = DateTime.Now;
        }
        
        public User(Guid id, string username, string password, string email, string state, List<Guid> ticketIds,
            DateTime createdTime, DateTime updatedTime) : base(state)
        {
            Id = id;
            Username = username;
            Password = password;
            Email = email;
            if (ticketIds == null)
            {
                TicketIds = new List<Guid>();
            }
            else
            {
                TicketIds = ticketIds;
            }
            CreatedTime = createdTime;
            UpdatedTime = updatedTime;
        }

        public void AddTicketId(Guid newTicketId)
        {
            TicketIds.Add(newTicketId);
            UpdatedTime = DateTime.Now;
        }
    }
}