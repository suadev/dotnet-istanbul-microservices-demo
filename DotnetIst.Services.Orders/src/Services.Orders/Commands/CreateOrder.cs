using System;
using Shared.Messages;
using Newtonsoft.Json;

namespace Services.Orders.Commands
{
    public class CreateOrder : ICommand
    {
        public Guid Id { get; }

        public CreateOrder(Guid id)
        {
            this.Id = id;
        }
    }
}