using System;
using System.Threading.Tasks;
using Api.Models;

namespace Api.HttpServices
{
    public interface ICustomerHttpService
    {
        Task<Customer> GetCustomerByEmail(string CustomerId);
    }
}