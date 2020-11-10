using Dms.Models;
using FileWebApplication.Repository.Interfaces;

namespace FileWebApplication.Repository
{
    public class UserRepository : MongoRepository<User>, IUserRepository
    {
        public UserRepository(IMongoContext mongoContext, string collectionName) : base(mongoContext, collectionName)
        {
        }
    }
}