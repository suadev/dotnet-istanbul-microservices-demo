using System.Threading.Tasks;
using Shared.Messages;
using Shared.RabbitMq;

namespace Shared.MessageHandlers
{
    public interface IEventHandler<in TEvent> where TEvent : IEvent
    {
        Task HandleAsync(TEvent _event, ICorrelationContext context);
    }
}