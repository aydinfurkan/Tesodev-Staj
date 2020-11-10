using WorkerService.Model;

namespace WorkerService.Repository.Interfaces
{
    public interface IUserRepository
    {
        public UserRepository.MongoClass Mongo { get; set; }
        public UserRepository.SqLiteClass SqLite { get; set; }
    }
}