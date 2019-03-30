using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Services.Customers.Data;
using Services.Products.Data;

namespace Services.Products.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ProductDBContext>();
            context.Database.EnsureCreated();
            if (!context.Products.Any())
            {
                var products = new List<Product>()
                {
                    new Product { Name = "Printer", Price = 200, Quantity = 500 },
                    new Product() { Name = "Mouse", Price = 20, Quantity = 500 },
                    new Product() { Name = "Keyboard", Price = 50, Quantity = 500 }
                };

                context.Products.AddRange(products);
                context.SaveChanges();
            }
        }
    }
}