using System;
using MongoDB.Bson.Serialization.Attributes;

namespace ConsoleApp3
{
    public class Ticket
    {
        public string Message { get; }
        [BsonId]
        public Guid Id { get; }
        public string Username { get; }
        public bool IsOpen { get; set; }
        private string Response;
        public DateTime CreatedTime { get; }
        public DateTime UpdatedTime { get; set; }

        public Ticket(string message, string username)
        {
            IsOpen = true;
            Message = message;
            Username = username;
            Id = Guid.NewGuid();
            Response = "";
            CreatedTime = DateTime.Now;
            UpdatedTime = DateTime.Now;
        }
        
        public Ticket(Guid id, string username, string message, string response, bool isOpen, DateTime createdTime, DateTime updatedTime)
        {
            IsOpen = isOpen;
            Message = message;
            Username = username;
            Id = id;
            Response = response;
            CreatedTime = createdTime;
            UpdatedTime = updatedTime;
        }
        
        public void SetResponse(string response)
        {
            IsOpen = false;
            Response = response;
            UpdatedTime = DateTime.Now;
        }
        
        public string GetResponse()
        {
            return Response;
        }
        
    }
}