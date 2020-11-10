using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FileWebApplication.Repository.Interfaces
{
    public interface IBaseRepository<T>
    {
        public T Find(Expression<Func<T, bool>> expression);
        public List<T> FindMany(Expression<Func<T, bool>> expression);
        public T Insert(T document);
        public IEnumerable<T> InsertMany(IEnumerable<T> documents);
        public void Replace(Expression<Func<T, bool>> expression, T document);
        public void Update(Expression<Func<T, bool>> expression, Expression<Func<T, object>> field, object value);
        public void UpdateAll(Expression<Func<T, bool>> expression, Expression<Func<T, object>> field, object value);

        public void UpdateManyField(Expression<Func<T, bool>> expression,
            IEnumerable<KeyValuePair<Expression<Func<T, object>>, object>> updates);

        public void Delete(Expression<Func<T, bool>> expression);
        public void DeleteAll();
    }
}