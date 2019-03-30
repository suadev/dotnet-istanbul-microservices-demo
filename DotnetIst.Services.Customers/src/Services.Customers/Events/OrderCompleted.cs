using System;
using Shared.Messages;
using Newtonsoft.Json;

namespace Services.Customers.Events
{
    public class OrderCompleted : IEvent
    {
        public Guid Id { get; }

        public OrderCompleted(Guid id)
        {
            Id = id;
        }
    }
}