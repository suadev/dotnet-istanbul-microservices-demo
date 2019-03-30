using System;
using System.Collections.Generic;
using Shared.Messages;

namespace Services.Products.Events
{
    [MessageNamespace("orders")]
    public class ProductsReserved : IEvent
    {
        public Guid OrderId { get; set; }
        public IDictionary<Guid, int> Products { get; set; }
        public ProductsReserved(Guid orderId, IDictionary<Guid, int> products)
        {
            OrderId = orderId;
            Products = products;
        }
    }
}