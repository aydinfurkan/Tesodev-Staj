using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Gateway.Services.Interfaces
{
    public interface IBaseService<T>
    {
        public Task<T> GetModelAsync(string path);
        public Task<List<T>> GetModelArrayAsync(string path);
        public Task<T> PostAsync(string path, HttpContent content);
        public Task<T> PutAsync(string path, HttpContent content);
    }
}