using System;
using System.Threading.Tasks;
using Services.Orders.Models;

namespace Services.Orders.HttpServices
{
    public interface ICustomerHttpService
    {
        Task<Basket> GetBasket(Guid CustomerId);
    }
}