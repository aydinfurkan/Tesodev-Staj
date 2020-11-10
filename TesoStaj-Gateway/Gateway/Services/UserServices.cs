using System.Collections.Generic;
using System.IO;
using Gateway.Client;
using Gateway.Client.Interfaces;
using Gateway.DatabaseSettings;
using Gateway.Model;
using Gateway.Services.Interfaces;

namespace Gateway.Services
{
    public class UserServices : BaseServices<User>, IUserServices
    {
        public UserServices(ClientDatabaseSettings databaseSettings)
        {
            Client = new MyHttpClient(databaseSettings.UserBaseAddress);
        }
    }
}