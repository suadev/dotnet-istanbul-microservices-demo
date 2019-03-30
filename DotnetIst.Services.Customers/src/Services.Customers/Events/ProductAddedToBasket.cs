using System;
using Shared.Messages;
using Newtonsoft.Json;

namespace Services.Customers.Events
{
    public class ProductAddedToBasket : IEvent
    {
        public Guid ProductId { get; }
        public int Quantity { get; }

        public ProductAddedToBasket(Guid productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }
    }
}