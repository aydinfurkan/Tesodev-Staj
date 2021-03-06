﻿using System;
using System.Collections.Generic;
using FileApi.Http;
using FileApi.Models;
using FileApi.Repository.Interfaces;
using FileApi.Services.Interfaces;

namespace FileApi.Services
{
    public abstract class BaseServices<T> : IBaseService<T> where T : BaseModel
    {
        public abstract IBaseRepository<T> Repository { get; set; }

        public List<T> GetAll()
        {
            return Repository.FindMany(x => true);
        }

        public T GetById(Guid id)
        {
            var document = Repository.Find(x => x.Id == id);

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