using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Gateway.Client.Interfaces;
using Gateway.Model;
using Gateway.Services.Interfaces;
using Newtonsoft.Json;

namespace Gateway.Services
{
    public abstract class BaseServices<T> : IBaseService<T> where T : BaseModel
    {
        protected IMyHttpClient Client { get; set; }

        public async Task<T> GetModelAsync(string path)
        {
            var response = await Client.GetAsync(path);

            // TODO exceptions

            var stringDocument = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(stringDocument);
        }
        
        public async Task<List<T>> GetModelArrayAsync(string path)
        {
            var response = await Client.GetAsync(path);
            
            var stringDocument = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<T>>(stringDocument);
        }

        public async Task<T> PostAsync(string path, HttpContent content)
        {
            var response = await Client.PostAsync(path, content);
            
            var stringDocument = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(stringDocument);
        }
        
        public async Task<T> PutAsync(string path, HttpContent content)
        {
            var response = await Client.PutAsync(path, content);
            
            var stringDocument = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(stringDocument);
        }
    }
}