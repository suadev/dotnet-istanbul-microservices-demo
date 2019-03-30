using System;
using System.Collections.Generic;

namespace Services.Orders.Models
{
    public class Basket
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<BasketItem> Items { get; set; }
    }
}