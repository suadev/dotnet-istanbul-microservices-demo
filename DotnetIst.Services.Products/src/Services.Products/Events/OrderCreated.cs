using System;
using System.Collections.Generic;
using Shared.Messages;
using Newtonsoft.Json;

namespace Services.Products.Events
{
    public class OrderCreated : IEvent
    {
        public Guid Id { get; }
        public IDictionary<Guid, int> Products { get; } // id, quantity

        public OrderCreated(Guid id, IDictionary<Guid, int> products)
        {
            Id = id;
            Products = products;
        }
    }
}