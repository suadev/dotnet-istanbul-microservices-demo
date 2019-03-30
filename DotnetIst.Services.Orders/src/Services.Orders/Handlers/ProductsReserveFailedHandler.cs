using System;
using System.Threading.Tasks;
using Shared.MessageHandlers;
using Shared.RabbitMq;
using Microsoft.Extensions.Logging;
using Services.Customers.Data;
using Services.Orders.Data;
using Services.Orders.Events;

namespace Services.Orders.Handlers
{
    public class ProductsReserveFailedHandler : IEventHandler<ProductsReserveFailed>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly OrderDBContext _dbContext;
        private readonly ILogger<ProductsReserveFailedHandler> _logger;

        public ProductsReserveFailedHandler(IBusPublisher busPublisher,
                                         OrderDBContext dbContext,
                                         ILogger<ProductsReserveFailedHandler> logger)
        {
            _logger = logger;
            _busPublisher = busPublisher;
            _dbContext = dbContext;
        }
        public async Task HandleAsync(ProductsReserveFailed _event, ICorrelationContext context)
        {
            var order = await _dbContext.Orders.FindAsync(_event.OrderId);
            order.Status = OrderStatus.Failed;
            order.UpdateDate = DateTime.Now;

            await _dbContext.SaveChangesAsync();
            _logger.LogInformation($"[Local Transaction] : Order failed. CorrelationId: {context.CorrelationId}");
            await _busPublisher.PublishAsync(new OrderFailed(_event.OrderId), context);
        }
    }
}