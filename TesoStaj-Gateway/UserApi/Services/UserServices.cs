using UserApi.DatabaseSettings;
using UserApi.Models;
using UserApi.Repository;
using UserApi.Repository.Interfaces;
using UserApi.Services.Interfaces;

namespace UserApi.Services
{
    public class UserServices : BaseServices<User>, IUserServices
    {
        public sealed override IRepository<User> Repository { get; set; }

        public UserServices(IMongoContext mongoContext, UserDatabaseSettings databaseSettings)
        {
            Repository = new UserRepository(mongoContext, databaseSettings.UserCollectionName);
        }


    }
    
}