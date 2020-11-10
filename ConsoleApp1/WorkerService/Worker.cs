using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WorkerService.Model;
using WorkerService.Repository;
using WorkerService.Repository.Interfaces;

namespace WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private IUserRepository _userRepository;
        private ITicketRepository _ticketRepository;

        public Worker(ILogger<Worker> logger, IUserRepository userRepository, ITicketRepository ticketRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _ticketRepository = ticketRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _userRepository.Mongo.DeleteAll();
                
                var user1 = new User( "FURKAN", "1111", "furkan@1111");
                var user2 = new User( "AHMET", "2222", "ahmet@2222");
                var user3 = new User( "VELI", "3333", "ahmet@3333");
                var user4 = new User( "FURKAN", "4444", "furkan@4444");
                
                _userRepository.Mongo.InsertMany(new []{user1, user2, user3, user4});
                var result1 = _userRepository.Mongo.FindMany(user => user.Username == "FURKAN");
                _userRepository.Mongo.UpdateManyField(x => x.Username == "FURKAN",
                    new []
                    {
                        new KeyValuePair<Expression<Func<User, object>>, object>(x => x.Email, "Update@Many"),
                        new KeyValuePair<Expression<Func<User, object>>, object>(x => x.Password, "NewPassss")
                    });
                
                
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
                
            }

        }
        
    }
}