using System;
using Shared.Messages;

namespace Services.Orders.Events
{
    [MessageNamespace("notifications")]
    public class OrderFailed : IEvent
    {
        public Guid OrderId { get; set; }
        public OrderFailed(Guid orderId)
        {
            this.OrderId = orderId;
        }
    }
}