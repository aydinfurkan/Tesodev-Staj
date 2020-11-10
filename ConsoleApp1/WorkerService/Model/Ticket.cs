using System;
using MongoDB.Bson.Serialization.Attributes;

namespace WorkerService.Model
{
    public class Ticket : BaseModel
    {
        public string Message { get; set; }
        public string Username { get; set; }
        public bool IsOpen { get; set; }
        public string Response;

        public Ticket(string message, string username)
        {
            IsOpen = true;
            Message = message;
            Username = username;
            Response = "";
        }
        
        public Ticket(string username, string message, string response, bool isOpen)
        {
            IsOpen = isOpen;
            Message = message;
            Username = username;
            Response = response;
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
        
        public override bool Equals(object? obj)
        {
            Ticket tck = obj as Ticket;
            if (tck == null)
                return false;

            return Id == tck.Id 
                   && Username == tck.Username
                   && Message == tck.Message
                   && IsOpen == tck.IsOpen
                   && Response == tck.Response
                   && CreatedTime == tck.CreatedTime
                   && UpdatedTime == tck.UpdatedTime;
        }
        
    }
}