using Dms.Models;
using FileWebApplication.DatabaseSettings;
using FileWebApplication.Http;
using FileWebApplication.Repository;
using FileWebApplication.Repository.Interfaces;
using FileWebApplication.Services.Interfaces;

namespace FileWebApplication.Services
{
    public class UserServices : BaseServices<User>, IUserServices
    {
        public sealed override IBaseRepository<User> Repository { get; set; }
        
        public UserServices(IMongoContext mongoContext, FwaDatabaseSettings databaseSettings)
        {
            Repository = new UserRepository(mongoContext, databaseSettings.UserCollectionName);
        }
        
        public User GetByUsernameAndPassword(string username, string password)
        {
            var document = Repository.FindMany(x => x.Username == username);

            if(document == null)
                throw new HttpNotFound(username);

            foreach (var currentUser in document)
            {
                if (currentUser.Password == password)
                {
                    return currentUser;
                }
            }

            throw new HttpUnauthorized();
        }

    }
}