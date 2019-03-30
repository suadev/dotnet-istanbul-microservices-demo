using System;

namespace Services.Orders.Models
{
    public class BasketItem
    {
        public Guid Id { get; set; }
        public Guid BasketId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}