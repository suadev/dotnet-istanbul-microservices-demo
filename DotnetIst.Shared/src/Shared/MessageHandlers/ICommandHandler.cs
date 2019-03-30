using System.Threading.Tasks;
using Shared.Messages;
using Shared.RabbitMq;

namespace Shared.MessageHandlers
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task HandleAsync(TCommand command, ICorrelationContext context);
    }
}