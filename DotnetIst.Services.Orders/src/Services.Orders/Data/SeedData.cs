using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Services.Customers.Data;

namespace Services.Orders.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<OrderDBContext>();
            context.Database.EnsureCreated();
        }
    }
}