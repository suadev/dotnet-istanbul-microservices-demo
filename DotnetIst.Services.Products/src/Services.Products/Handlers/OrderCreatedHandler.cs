using System;
using System.Threading.Tasks;
using Shared.MessageHandlers;
using Shared.RabbitMq;
using Microsoft.Extensions.Logging;
using Services.Customers.Data;
using Services.Products.Events;

namespace Services.Products.Handlers
{
    public class OrderCreatedHandler : IEventHandler<OrderCreated>
    {
        private readonly ProductDBContext _dbContext;
        private readonly IBusPublisher _busPublisher;
        private readonly ILogger<OrderCreatedHandler> _logger;

        public OrderCreatedHandler(ProductDBContext dbContext,
                            IBusPublisher busPublisher,
                            ILogger<OrderCreatedHandler> logger)
        {
            _logger = logger;
            _dbContext = dbContext;
            _busPublisher = busPublisher;
        }
        public async Task HandleAsync(OrderCreated _event, ICorrelationContext context)
        {
            var isReserved = true;

            foreach ((Guid productId, int quantity) in _event.Products)
            {
                var product = await _dbContext.Products.FindAsync(productId);

                if (product == null)
                {
                    isReserved = false;
                    _logger.LogInformation($"[Local Transaction] : Product '{productId}' not found. CorrelationId: {context.CorrelationId}");
                    break;
                }

                if (quantity > product.Quantity)
                {
                    isReserved = false;
                    _logger.LogInformation($"[Local Transaction] : Not available {product.Quantity} {product.Name}. CorrelationId: {context.CorrelationId}");
                    break;
                }
                else
                {
                    product.Quantity -= quantity;
                    product.UpdateDate = DateTime.Now;
                }
            }

            if (isReserved)
            {
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation($"[Local Transaction] : Products reserved. CorrelationId: {context.CorrelationId}");
                await _busPublisher.PublishAsync(new ProductsReserved(_event.Id, _event.Products), context);
            }
            else
            {
                _logger.LogInformation($"[Local Transaction] : Products could not be reserved. CorrelationId: {context.CorrelationId}");
                await _busPublisher.PublishAsync(new ProductsReserveFailed(_event.Id), context);
            }
        }
    }
}