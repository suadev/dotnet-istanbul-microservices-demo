using System;
using System.Linq;
using System.Threading.Tasks;
using Shared.MessageHandlers;
using Shared.RabbitMq;
using Microsoft.Extensions.Logging;
using Services.Customers.Data;
using Services.Orders.Commands;
using Services.Orders.Data;
using Services.Orders.Events;
using Services.Orders.HttpServices;

namespace Services.Orders.Handlers
{
    public class CreateOrderHandler : ICommandHandler<CreateOrder>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly ICustomerHttpService _customerHttpService;
        private readonly OrderDBContext _dbContext;
        private readonly ILogger<CreateOrderHandler> _logger;

        public CreateOrderHandler(IBusPublisher busPublisher,
               ICustomerHttpService customerHttpService,
               OrderDBContext dbContext,
               ILogger<CreateOrderHandler> logger)
        {
            _busPublisher = busPublisher;
            _customerHttpService = customerHttpService;
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task HandleAsync(CreateOrder command, ICorrelationContext context)
        {
            // Warning: Order service needs Customer service's data.
            // What if Customer service can't response for a while? (assume no retry policy)   

            var basket = await _customerHttpService.GetBasket(context.CustomerId);

            var items = basket.Items.Select(i =>
                new OrderItem
                {
                    Id = Guid.NewGuid(),
                    OrderId = command.Id,
                    ProductId = i.ProductId,
                    Name = i.ProductName,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList();

            var order = new Order
            {
                Status = OrderStatus.Created,
                Id = command.Id,
                CustomerId = context.CustomerId,
                Items = items,
                TotalAmount = items.Sum(s => s.TotalPrice)
            };

            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"[Local Transaction] : Order created. CorrelationId: {context.CorrelationId}");
            await _busPublisher.PublishAsync(new OrderCreated(
                command.Id, items.ToDictionary(i => i.ProductId, i => i.Quantity)), context);


        }
    }
}