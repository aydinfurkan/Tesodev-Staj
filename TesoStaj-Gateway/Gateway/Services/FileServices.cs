using System.Collections.Generic;
using System.Threading.Tasks;
using Gateway.Client;
using Gateway.Client.Interfaces;
using Gateway.DatabaseSettings;
using Gateway.Model;
using Gateway.Services.Interfaces;

namespace Gateway.Services
{
    public class FileServices : BaseServices<FileModel>, IFileServices
    {
        public FileServices(ClientDatabaseSettings databaseSettings)
        {
            Client = new MyHttpClient(databaseSettings.FileBaseAddress);
        }

        public async Task<KeyValuePair<byte[], string>> GetFileSrcAsync(string path)
        {
            var response = await Client.GetAsync(path);
            
            var bytesDocument = await response.Content.ReadAsByteArrayAsync();
            var typeDocument = response.Content.Headers.ContentType.ToString();

            return new KeyValuePair<byte[], string>(bytesDocument, typeDocument); // TODO return IFormFile
        }
        
    }
}