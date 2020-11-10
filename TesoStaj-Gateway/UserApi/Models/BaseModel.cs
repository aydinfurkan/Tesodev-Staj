using System;
using MongoDB.Bson.Serialization.Attributes;

namespace UserApi.Models
{
    public class BaseModel
    {
        [BsonId]
        public Guid Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }

        protected BaseModel()
        {
            CreatedTime = DateTime.SpecifyKind(CreatedTime, DateTimeKind.Utc);
            UpdatedTime = DateTime.SpecifyKind(UpdatedTime, DateTimeKind.Utc);
        }
    }
}