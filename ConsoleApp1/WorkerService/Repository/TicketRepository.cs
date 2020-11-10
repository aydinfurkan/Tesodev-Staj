using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;
using System.Runtime.Serialization;
using MongoDB.Driver;
using WorkerService.Model;
using WorkerService.Repository.Interfaces;

namespace WorkerService.Repository
{
    public class TicketRepository : MongoRepository<Ticket>, ITicketRepository
    {

        public TicketRepository(IMongoContext mongoContext) : base(mongoContext, "Ticket")
        {
        }
        
    }
}