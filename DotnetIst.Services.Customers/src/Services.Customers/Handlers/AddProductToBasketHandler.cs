using System;
using System.Linq;
using System.Threading.Tasks;
using Shared.MessageHandlers;
using Shared.RabbitMq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Services.Customers.Commands;
using Services.Customers.Data;
using Services.Customers.Events;
using Services.Customers.HttpServices;

namespace Services.Customers.Handlers
{
    public class AddProductToBasketHandler : ICommandHandler<AddProductToBasket>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly CustomerDBContext _dbContext;
        private readonly IProductHttpService _productHttpService;
        private readonly ILogger<AddProductToBasketHandler> _logger;

        public AddProductToBasketHandler(IBusPublisher busPublisher,
            CustomerDBContext dbContext,
            IProductHttpService productHttpService,
            ILogger<AddProductToBasketHandler> logger
            )
        {
            _productHttpService = productHttpService;
            _logger = logger;
            _busPublisher = busPublisher;
            _dbContext = dbContext;
        }

        public async Task HandleAsync(AddProductToBasket command, ICorrelationContext context)
        {
            // Warning: Customer service needs Product service's data.
            // What if Product service can't response for a while? (assume no retry policy)
            // DDD - sharing data betwwen bounded cotext

            var product = await _productHttpService.GetAsync(command.ProductId);
            if (product == null)
                throw new Exception($"Product not found. Id: {command.ProductId}");

            var basket = await _dbContext.Baskets.FirstOrDefaultAsync(q => q.CustomerId == context.CustomerId);
            if (basket == null)
                throw new Exception($"Basket not found for customer: {context.CustomerId}");

            var basketItem = await _dbContext.BasketItems.FirstOrDefaultAsync(q => q.ProductId == command.ProductId);

            if (basketItem != null)
            {
                basketItem.Quantity += command.Quantity;
            }
            else
            {
                _dbContext.BasketItems.Add(
                    new BasketItem
                    {
                        BasketId = basket.Id,
                        ProductId = command.ProductId,
                        Quantity = command.Quantity,
                        ProductName = product.Name,
                        UnitPrice = product.Price
                    });
            }

            await _dbContext.SaveChangesAsync();

            // no event, just logging.
            _logger.LogInformation($"[Local Transaction] : {command.Quantity} {product.Name} added to basket. CorrelationId: {context.CorrelationId}");
        }
    }
}