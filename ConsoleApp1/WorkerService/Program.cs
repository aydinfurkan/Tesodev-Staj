using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using WorkerService.Repository;
using WorkerService.Repository.Interfaces;

namespace WorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var mongoClient = new MongoClient("mongodb://localhost:27017");
                    var sqLiteConnection = new SQLiteConnection("Data Source=F:/RiderProjects/ConsoleApp1/UserTicket.db;Version=3;");
                    
                    var mongoContext = new MongoContext(mongoClient, "UserTicket");
                    var sqLiteContext = new SqLiteContext(sqLiteConnection);
                    
                    services.AddSingleton<IMongoContext>(x => mongoContext);
                    services.AddSingleton<ISqLiteContext>(x => sqLiteContext);
                    services.AddSingleton<IMongoClient>(x => mongoClient);
                    services.AddSingleton<IUserRepository, UserRepository>();
                    services.AddSingleton<ITicketRepository, TicketRepository>();
                    
                    services.AddHostedService<Worker>();
                });
    }
}