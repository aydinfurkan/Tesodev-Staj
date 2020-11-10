using Dms.Models;

namespace FileWebApplication.Services.Interfaces
{
    public interface IUserServices : IBaseService<User>
    {
        public User GetByUsernameAndPassword(string username, string password);
    }
    
}