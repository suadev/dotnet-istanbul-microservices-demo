
using System;
using Shared.Messages;
using Newtonsoft.Json;

namespace Services.Customers.Commands
{
    public class AddProductToBasket : ICommand
    {
        public Guid ProductId { get; }
        public int Quantity { get; }

        public AddProductToBasket(Guid productId,
            int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }
    }
}