using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using WorkerService.Model;
using WorkerService.Repository.Interfaces;

namespace WorkerService.Repository
{
    public class SqLiteRepository<T> : IRepository<T> where T : BaseModel
    {
        protected ISqLiteContext _sqLiteContext;
        private string DateTimeFormat = "dd-MM-yyyy HH:mm:ss";
        public SqLiteRepository(ISqLiteContext sqLiteContext)
        {
            _sqLiteContext = sqLiteContext;
        }

        /*public T GetAll()
        {
            var sqLiteConnection = _sqLiteContext.GetConnection();
            sqLiteConnection.Open();
        
            string stm = "SELECT * FROM users";
            SQLiteCommand cmd = new SQLiteCommand(stm, sqLiteConnection);
        
            using SQLiteDataReader rdr = cmd.ExecuteReader();
        
            var users = new T();
            while (rdr.Read())
            {
                var ticketIds = JsonConvert.DeserializeObject<List<Guid>>(rdr.GetString(5));
                DateTime createdTime = DateTime.ParseExact(rdr.GetString(6), DateTimeFormat, CultureInfo.InvariantCulture);
                DateTime updatedTime = DateTime.ParseExact(rdr.GetString(7), DateTimeFormat, CultureInfo.InvariantCulture);
            
                var user = new User(rdr.GetGuid(0),rdr.GetString(1), rdr.GetString(2),
                    rdr.GetString(3), rdr.GetString(4), ticketIds,
                    createdTime, updatedTime);
            
                users.Add(user);
            }

            sqLiteConnection.Close();
            return users;
        }*/
        
        /*var sqLiteConnection = _sqLiteContext.GetConnection();
        sqLiteConnection.Open();
        
        string stm = "SELECT * FROM tickets";
        SQLiteCommand cmd = new SQLiteCommand(stm, sqLiteConnection);
        
            using SQLiteDataReader rdr = cmd.ExecuteReader();
        
        var tickets = new List<Ticket>();
            while (rdr.Read())
        {
            bool isOpen = rdr.GetInt32(4) != 0;
            DateTime createdTime = DateTime.ParseExact(rdr.GetString(5), DateTimeFormat, CultureInfo.InvariantCulture);
            DateTime updatedTime = DateTime.ParseExact(rdr.GetString(6), DateTimeFormat, CultureInfo.InvariantCulture);
            
            var ticket = new Ticket(rdr.GetGuid(0), rdr.GetString(1),
                rdr.GetString(2), rdr.GetString(3), isOpen,
                createdTime, updatedTime);
            
            tickets.Add(ticket);
        }

        sqLiteConnection.Close();
        return tickets;
        }

        return null;*/

        public T Find(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public List<T> FindMany(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public void Insert(T document)
        {
            throw new NotImplementedException();
        }

        public void InsertMany(IEnumerable<T> documents)
        {
            throw new NotImplementedException();
        }

        public void Replace(Expression<Func<T, bool>> expression, T document)
        {
            throw new NotImplementedException();
        }

        public void ReplaceMany(Expression<Func<T, bool>> expression, T document)
        {
            throw new NotImplementedException();
        }

        public void Update(Expression<Func<T, bool>> expression, Expression<Func<T, object>> field, object value)
        {
            throw new NotImplementedException();
        }

        public void UpdateAll(Expression<Func<T, bool>> expression, Expression<Func<T, object>> field, object value)
        {
            throw new NotImplementedException();
        }

        public void UpdateManyField(Expression<Func<T, bool>> expression, IEnumerable<KeyValuePair<Expression<Func<T, object>>, object>> updates)
        {
            throw new NotImplementedException();
        }

        public void Delete(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public void DeleteAll()
        {
            throw new NotImplementedException();
        }
    }
}