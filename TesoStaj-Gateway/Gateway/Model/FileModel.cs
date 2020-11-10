using System;

namespace Gateway.Model
{
    public class FileModel : BaseModel
    {
        public FileModel(string name, string contentType, Guid owner)
        {
            Name = name;
            ContentType = contentType;
            OwnerId = owner;
        }

        public string Name { get; set; }
        public string ContentType { get; set; }
        public Guid OwnerId { get; set; }
    }
}