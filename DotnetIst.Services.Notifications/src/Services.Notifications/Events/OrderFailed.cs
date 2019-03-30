using System;
using Shared.Messages;
using Newtonsoft.Json;

namespace Services.Notifications.Events
{
    public class OrderFailed : IEvent
    {
        public Guid Id { get; }

        public OrderFailed(Guid id)
        {
            Id = id;
        }
    }
}