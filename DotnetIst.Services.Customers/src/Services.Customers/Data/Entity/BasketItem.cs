using System;
using Shared.Models;

namespace Services.Customers.Data
{
    public class BasketItem : BaseEntity
    {
        public Guid BasketId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}