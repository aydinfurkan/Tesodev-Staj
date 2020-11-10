using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Gateway.Client.Interfaces
{
    public interface IMyHttpClient
    {
        public Task<HttpResponseMessage> GetAsync(string path);
        public Task<HttpResponseMessage> PostAsync(string path, HttpContent content);
        public Task<HttpResponseMessage> PutAsync(string path, HttpContent content);
        public Task<HttpResponseMessage> DeleteAsync(string path);
        
    }
}