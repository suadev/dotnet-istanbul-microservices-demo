using System;
using System.Threading.Tasks;
using Shared.MessageHandlers;
using Shared.RabbitMq;
using Microsoft.Extensions.Logging;
using Services.Customers.Commands;
using Services.Customers.Data;

namespace Services.Customers.Handlers
{
    public class CreateCustomerdHandler : ICommandHandler<CreateCustomer>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly CustomerDBContext _dbContext;
        private readonly ILogger<CreateCustomerdHandler> _logger;

        public CreateCustomerdHandler(IBusPublisher busPublisher,
                                        CustomerDBContext dbContext,
                                        ILogger<CreateCustomerdHandler> logger)
        {
            _logger = logger;
            _busPublisher = busPublisher;
            _dbContext = dbContext;
        }
        public async Task HandleAsync(CreateCustomer _event, ICorrelationContext context)
        {
            _dbContext.Customers.Add(new Customer
            {
                Id = _event.Id,
                Password = _event.Password,
                Email = _event.Email,
                FirstName = _event.FirstName,
                LastName = _event.LastName,
                Address = _event.Address
            });

            _dbContext.Baskets.Add(new Basket
            {
                CustomerId = _event.Id
            });

            await _dbContext.SaveChangesAsync();
            _logger.LogInformation($"[Local Transaction] : Customer created. CorrelataionId: {context.CorrelationId}");
        }
    }
}