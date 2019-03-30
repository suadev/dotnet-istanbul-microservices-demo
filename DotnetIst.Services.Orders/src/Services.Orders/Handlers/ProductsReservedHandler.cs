using System.Threading.Tasks;
using Shared.MessageHandlers;
using Shared.RabbitMq;
using Microsoft.Extensions.Logging;
using Services.Customers.Data;
using Services.Orders.Data;
using Services.Orders.Events;

namespace Services.Orders.Handlers
{
    public class ProductsReservedHandler : IEventHandler<ProductsReserved>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly OrderDBContext _dbContext;
        private readonly ILogger<ProductsReservedHandler> _logger;

        public ProductsReservedHandler(IBusPublisher busPublisher,
                         OrderDBContext dbContext,
                         ILogger<ProductsReservedHandler> logger)
        {
            _logger = logger;
            _busPublisher = busPublisher;
            _dbContext = dbContext;
        }
        public async Task HandleAsync(ProductsReserved _event, ICorrelationContext context)
        {
            var order = await _dbContext.Orders.FindAsync(_event.OrderId);
            order.Status = OrderStatus.Completed;

            await _dbContext.SaveChangesAsync();
            _logger.LogInformation($"[Local Transaction] : Order completed. CorrelationId: {context.CorrelationId}");
            await _busPublisher.PublishAsync(new OrderCompleted(_event.OrderId), context);
        }
    }
}