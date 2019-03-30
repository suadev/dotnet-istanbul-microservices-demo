using System.Threading.Tasks;
using Shared.MessageHandlers;
using Shared.RabbitMq;
using Microsoft.Extensions.Logging;
using Services.Notifications.Events;

namespace Services.Notifications.Handlers
{
    public class OrderCompletedHandler : IEventHandler<OrderCompleted>
    {
        private readonly ILogger<OrderCompletedHandler> _logger;
        public OrderCompletedHandler(ILogger<OrderCompletedHandler> logger)
        {
            _logger = logger;
        }
        public async Task HandleAsync(OrderCompleted _event, ICorrelationContext context)
        {
            // Order completed sms/mail/push...
            _logger.LogInformation($"[Local Transaction] : Notification sent for Created Order. CorrelationId: {context.CorrelationId}");
            await Task.CompletedTask;
        }
    }
}