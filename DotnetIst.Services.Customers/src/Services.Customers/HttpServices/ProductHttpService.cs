using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Services.Customers.Data;
using Services.Customers.Models;

namespace Services.Customers.HttpServices
{
    public class ProductHttpService : IProductHttpService
    {
        private HttpClient _client { get; }

        public ProductHttpService(HttpClient client)
        {
            _client = client;
        }

        public async Task<Product> GetAsync(Guid id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"product/{id}");
            var response = await _client.SendAsync(request);

            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                response.Content.Dispose();
                return JsonConvert.DeserializeObject<Product>(content);
            }
            throw new Exception("Product service connection error");
        }
    }
}