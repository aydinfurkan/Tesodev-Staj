using System;
using System.Collections.Generic;
using System.Data.SQLite;
using WorkerService.Repository.Interfaces;

namespace WorkerService.Repository
{
    public class SqLiteContext : ISqLiteContext
    {
        private SQLiteConnection _connection;

        public SqLiteContext(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public SQLiteConnection GetConnection()
        {
            return _connection;
        }
        
    }
}