﻿using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Gateway.Client.Interfaces;

namespace Gateway.Client
{
    public class MyHttpClient : IMyHttpClient
    {
        private readonly HttpClient _httpClient;

        public MyHttpClient(string baseAddress)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            
            _httpClient = new HttpClient(clientHandler) {BaseAddress = new Uri(baseAddress)};
            
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

        }
        public async Task<HttpResponseMessage> GetAsync(string path)
        {
            return await _httpClient.GetAsync(path);
        }

        public async Task<HttpResponseMessage> PostAsync(string path, HttpContent content)
        {
            return await _httpClient.PostAsync(path, content);
        }

        public async Task<HttpResponseMessage> PutAsync(string path, HttpContent content)
        {
            return await _httpClient.PutAsync(path, content);
        }

        public async Task<HttpResponseMessage> DeleteAsync(string path)
        {
            return await _httpClient.DeleteAsync(path);
        }
        
    }
}