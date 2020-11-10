using System.Collections.Generic;
using System.Threading.Tasks;
using Gateway.Model;

namespace Gateway.Services.Interfaces
{
    public interface IFileServices : IBaseService<FileModel>
    {
        public Task<KeyValuePair<byte[], string>> GetFileSrcAsync(string path);
    }
}