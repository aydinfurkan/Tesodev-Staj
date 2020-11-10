using UserApi.Models;
using UserApi.Repository.Interfaces;

namespace UserApi.Repository
{
    public class UserRepository : MongoRepository<User>, IUserRepository
    {

        public UserRepository(IMongoContext mongoContext, string collectionName) : base(mongoContext, collectionName)
        {
        }
    }
}