using System;
using System.Collections.Generic;
using Shared.Models;

namespace Services.Customers.Data
{
    public class Basket : BaseEntity
    {
        public Guid CustomerId { get; set; }
        public List<BasketItem> Items { get; set; }
    }
}