using System;
using System.Collections.Generic;
using FileApi.Repository.Interfaces;

namespace FileApi.Services.Interfaces
{
    public interface IBaseService<T>
    {
        public IBaseRepository<T> Repository { set; get; }
        public List<T> GetAll();
        public T GetById(Guid id);
        public T Create(T document);
        public void Update(Guid id, T document);
        public void Remove(Guid id);

        public void RemoveAll();
    }
}