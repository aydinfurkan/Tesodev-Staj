using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using WorkerService.Model;

namespace WorkerService.Repository.Interfaces
{
    public interface IRepository<T>
    {
        public T Find(Expression<Func<T, bool>> expression);
        public List<T> FindMany(Expression<Func<T, bool>> expression);
        public void Insert(T document);
        public void InsertMany(IEnumerable<T> documents);
        public void Replace(Expression<Func<T, bool>> expression, T document);
        public void Update(Expression<Func<T, bool>> expression, Expression<Func<T, object>> field, object value);
        public void UpdateAll(Expression<Func<T, bool>> expression, Expression<Func<T, object>> field, object value);
        public void UpdateManyField(Expression<Func<T, bool>> expression,IEnumerable<KeyValuePair<Expression<Func<T, object>>, object>> updates);
        public void Delete(Expression<Func<T, bool>> expression);
        public void DeleteAll();
    }
}