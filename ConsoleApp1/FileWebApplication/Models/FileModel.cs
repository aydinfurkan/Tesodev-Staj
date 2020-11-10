using System;
using System.Collections.Generic;

namespace FileWebApplication.Models
{
    public class FileModel : BaseModel
    {
        public FileModel(string name, string contentType, Guid owner)
        {
            Name = name;
            ContentType = contentType;
            OwnerId = owner;
            UpdateIds = new List<Guid>();
        }

        public string Name { get; set; }
        public string ContentType { get; set; }
        public Guid OwnerId { get; set; }
        public List<Guid> UpdateIds { get; set; }

        public void UpdateBy(Guid userId)
        {
            UpdateIds.Add(userId);
        }
    }
}