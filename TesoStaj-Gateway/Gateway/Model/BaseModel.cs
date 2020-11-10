using System;

namespace Gateway.Model
{
    public class BaseModel
    {
        //[BsonId]
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