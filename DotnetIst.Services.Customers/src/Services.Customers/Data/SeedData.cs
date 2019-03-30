using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Services.Customers.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<CustomerDBContext>();
            context.Database.EnsureCreated();
        }
    }
}