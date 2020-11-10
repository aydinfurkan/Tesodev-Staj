using System;
using System.Collections.Generic;
using UserApi.Models;
using UserApi.Repository.Interfaces;

namespace UserApi.Services.Interfaces
{
    public interface IService<T> where T : BaseModel
    {
        public IRepository<T> Repository { set; get; }
        public List<T> GetAll();
        public T GetById(Guid id);
        public T Create(T document);
        public void Update(Guid id, T document);
        public void Remove(Guid id);

        public void RemoveAll();

    }
}