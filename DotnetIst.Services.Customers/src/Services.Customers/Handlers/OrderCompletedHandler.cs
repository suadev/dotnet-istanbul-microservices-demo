using System.Threading.Tasks;
using Shared.MessageHandlers;
using Shared.RabbitMq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Services.Customers.Data;
using Services.Customers.Events;

namespace Services.Customers.Handlers
{
    public class OrderCompletedHandler : IEventHandler<OrderCompleted>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly CustomerDBContext _dbContext;
        private readonly ILogger<OrderCompletedHandler> _logger;

        public OrderCompletedHandler(IBusPublisher busPublisher,
                                    CustomerDBContext dbContext,
                                    ILogger<OrderCompletedHandler> logger)
        {
            _logger = logger;
            _busPublisher = busPublisher;
            _dbContext = dbContext;
        }
        public async Task HandleAsync(OrderCompleted _event, ICorrelationContext context)
        {
            var basket = await _dbContext.Baskets
                .Include(i => i.Items)
                .FirstOrDefaultAsync(q => q.CustomerId == context.CustomerId);

            _dbContext.BasketItems.RemoveRange(basket.Items);
            await _dbContext.SaveChangesAsync();
            //no event, just logging
            _logger.LogInformation($"[Local Transaction] : Basket items cleared. CorrelationId: {context.CorrelationId}");
        }
    }
}