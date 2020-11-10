using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MongoDB.Driver;
using UserApi.Models;
using UserApi.Repository.Interfaces;

namespace UserApi.Repository
{
    public abstract class MongoRepository<T> : IRepository<T> where T : BaseModel
    {
        protected IMongoContext _mongoContext;
        private IMongoCollection<T> _collection;

        protected MongoRepository(IMongoContext mongoContext, string collectionName)
        {
            _mongoContext = mongoContext;
            _collection = _mongoContext.GetCollection<T>(collectionName);
        }
        private void CreateNewDocument(T document)
        {
            document.CreatedTime = DateTime.Now;
            document.UpdatedTime = DateTime.Now;
            document.Id = Guid.NewGuid();
        }

        public T Find(Expression<Func<T, bool>> expression)
        {
            var filter = Builders<T>.Filter.Where(expression);
            return _collection.Find(filter).FirstOrDefault();
        }
        public List<T> FindMany(Expression<Func<T, bool>> expression)
        {
            var filter = Builders<T>.Filter.Where(expression);
            return _collection.Find(filter).ToList();
        }

        public T Insert(T document)
        {
            CreateNewDocument(document);
            _collection.InsertOne(document);
            return document;
        }

        public IEnumerable<T> InsertMany(IEnumerable<T> documents)
        {
            foreach (var document in documents)
            {
                CreateNewDocument(document);
            }
            _collection.InsertMany(documents);
            return documents;
        }

        public void Replace(Expression<Func<T, bool>> expression, T document)
        {
            document.UpdatedTime = DateTime.Now;
            var filter = Builders<T>.Filter.Where(expression);
            var result = _collection.ReplaceOne(filter,document);
            
        }
        
        public void Update(Expression<Func<T, bool>> expression, Expression<Func<T, object>> field, object value)
        {
            var filter = Builders<T>.Filter.Where(expression);
            var update = Builders<T>.Update.Set(field, value).Set(x => x.UpdatedTime, DateTime.Now);
            var result = _collection.UpdateOne(filter, update);
        }
        public void UpdateAll(Expression<Func<T, bool>> expression, Expression<Func<T, object>> field, object value)
        {
            var filter = Builders<T>.Filter.Where(expression);
            var update = Builders<T>.Update.Set(field, value).Set(x => x.UpdatedTime, DateTime.Now);;
            var result = _collection.UpdateMany(filter, update);
        }
        public void UpdateManyField(Expression<Func<T, bool>> expression,IEnumerable<KeyValuePair<Expression<Func<T, object>>, object>> updates)
        {
            var filter = Builders<T>.Filter.Where(expression);
            UpdateDefinition<T> update = new ObjectUpdateDefinition<T>(expression);
            foreach (var (key, value) in updates)
            {
                update = update.Set(key, value);
            }
            update = update.Set(x => x.UpdatedTime, DateTime.Now);
            var result = _collection.UpdateOne(filter, update);
        }

        public void Delete(Expression<Func<T, bool>> expression)
        {
            var filter = Builders<T>.Filter.Where(expression);
            var result = _collection.DeleteOne(filter);
        }
        public void DeleteAll()
        {
            _collection.DeleteMany(x => true);
        }

    }
}