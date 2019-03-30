using System;
using Shared.Messages;
using Newtonsoft.Json;

namespace Api.Commands.Orders
{
    [MessageNamespace("orders")]
    public class CreateOrder : ICommand
    {
        public Guid Id { get; private set; }

        public CreateOrder()
        {
            Id = Guid.NewGuid();
        }
    }
}