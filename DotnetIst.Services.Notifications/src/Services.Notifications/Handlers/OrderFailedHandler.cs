using System.Threading.Tasks;
using Shared.MessageHandlers;
using Shared.RabbitMq;
using Microsoft.Extensions.Logging;
using Services.Notifications.Events;

namespace Services.Notifications.Handlers
{
    public class OrderFailedHandler : IEventHandler<OrderFailed>
    {
        private readonly ILogger<OrderFailedHandler> _logger;
        public OrderFailedHandler(ILogger<OrderFailedHandler> logger)
        {
            _logger = logger;
        }
        public async Task HandleAsync(OrderFailed _event, ICorrelationContext context)
        {
            // Order failed sms/mail/push...
            _logger.LogInformation($"[Local Transaction] : Notification sent for Failed Order. CorrelationId: {context.CorrelationId}");
            await Task.CompletedTask;
        }
    }
}