using System;
using System.Collections.Generic;
using UserApi.Http;
using UserApi.Models;
using UserApi.Repository.Interfaces;
using UserApi.Services.Interfaces;

namespace UserApi.Services
{
    public abstract class BaseServices<T> : IService<T> where T : BaseModel
    {
        public abstract IRepository<T> Repository { get; set; }

        public List<T> GetAll()
        {
            var documents = Repository.FindMany(x => true);
            
            if(documents == null)
                throw new HttpNotFound("Database");
            
            return documents;
        }

        public T GetById(Guid id)
        {
            var document = Repository.Find(x => x.Id==id);
            
            if(document == null)
                throw new HttpNotFound(id.ToString());
            
            return document;
        }

        public T Create(T document)
        {
            return Repository.Insert(document);
        }

        public void Update(Guid id, T document)
        {
            Repository.Replace(x => x.Id == id, document);
        }

        public void Remove(Guid id)
        {
            Repository.Delete(x => x.Id == id);
        }
        
        public void RemoveAll()
        {
            Repository.DeleteAll();
        }
    }
}