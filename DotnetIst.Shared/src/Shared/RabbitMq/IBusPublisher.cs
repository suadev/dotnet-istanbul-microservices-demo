using System.Threading.Tasks;
using Shared.Messages;

namespace Shared.RabbitMq
{
    public interface IBusPublisher
    {
        Task SendAsync<TCommand>(TCommand command, ICorrelationContext context)
            where TCommand : ICommand;

        Task PublishAsync<TEvent>(TEvent _event, ICorrelationContext context)
            where TEvent : IEvent;
    }
}