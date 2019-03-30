using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Api.HttpServices
{
    public class ProductHttpService : IProductHttpService
    {
        private HttpClient _client { get; }

        public ProductHttpService(HttpClient client)
        {
            _client = client;
        }

        public async Task<object> GetList()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"product");
            var response = await _client.SendAsync(request);

            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                response.Content.Dispose();
                return JsonConvert.DeserializeObject<List<object>>(content);
            }
            throw new Exception("Product service connection error");
        }
    }
}