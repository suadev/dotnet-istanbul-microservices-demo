using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Services.Orders.Models;

namespace Services.Orders.HttpServices
{
    public class CustomerHttpService : ICustomerHttpService
    {
        private HttpClient _client { get; }

        public CustomerHttpService(HttpClient client)
        {
            _client = client;
        }

        public async Task<Basket> GetBasket(Guid customerId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"basket/{customerId}");
            var response = await _client.SendAsync(request);

            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                response.Content.Dispose();
                return JsonConvert.DeserializeObject<Basket>(content);
            }
            throw new Exception("Customer service connection error");
        }
    }
}