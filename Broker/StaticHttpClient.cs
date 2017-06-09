using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Broker.Business
{
    public class StaticHttpClient:IDisposable
    {
        private HttpClient _client;

        public StaticHttpClient()
        {
            _client = new HttpClient();
        }   

        public void Dispose()
        {
            _client.Dispose();
        }

        public async Task<HttpResponseMessage> PostAsync(string url, object message)
        {
            string jsonObject = JsonConvert.SerializeObject(message);


            var response = await _client.PostAsync(url, new StringContent(jsonObject, Encoding.UTF8, "application/json"));

            var responseString = await response.Content.ReadAsStringAsync();

            return response;
        }

        public async Task<HttpResponseMessage> PutAsync(string url, object message)
        {
            string jsonObject = JsonConvert.SerializeObject(message);


            var response = await _client.PutAsync(url, new StringContent(jsonObject, Encoding.UTF8, "application/json"));

            var responseString = await response.Content.ReadAsStringAsync();

            return response;
        }

        public async Task<string> GetAsync(string url)
        {
            var response = await _client.GetAsync(url);

           string responseContent = await response.Content.ReadAsStringAsync(); //right!

            return responseContent;
        }
    }
}
