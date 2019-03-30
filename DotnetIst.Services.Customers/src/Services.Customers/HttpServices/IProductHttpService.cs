using System;
using System.Threading.Tasks;
using Services.Customers.Data;
using Services.Customers.Models;

namespace Services.Customers.HttpServices
{
    public interface IProductHttpService
    {
        Task<Product> GetAsync(Guid id);
    }
}