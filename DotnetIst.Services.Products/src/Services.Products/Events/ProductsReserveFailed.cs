using System;
using Shared.Messages;

namespace Services.Products.Events
{
    [MessageNamespace("orders")]
    public class ProductsReserveFailed : IEvent
    {
        public Guid OrderId { get; set; }
        public ProductsReserveFailed(Guid orderId)
        {
            this.OrderId = orderId;
        }
    }
}