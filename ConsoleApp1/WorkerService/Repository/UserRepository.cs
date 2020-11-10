using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;
using System.Runtime.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using WorkerService.Model;
using WorkerService.Repository.Interfaces;

namespace WorkerService.Repository
{
    public class UserRepository : IUserRepository
    {
        public MongoClass Mongo { get; set; }
        public SqLiteClass SqLite { get; set; }

        public UserRepository(IMongoContext mongoContext, ISqLiteContext sqLiteContext)
        {
            Mongo = new  MongoClass(mongoContext, "User");
            SqLite = new SqLiteClass(sqLiteContext);
        }
        public class MongoClass : MongoRepository<User>
        {
            public MongoClass(IMongoContext mongoContext, string collectionName) : base(mongoContext, collectionName)
            {
            }
        }
        public class SqLiteClass : SqLiteRepository<User>
        {
            public SqLiteClass(ISqLiteContext sqLiteContext) : base(sqLiteContext)
            {
            }
        }
    }
}