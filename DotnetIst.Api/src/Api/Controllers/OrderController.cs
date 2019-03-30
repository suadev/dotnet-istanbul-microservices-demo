using System.Threading.Tasks;
using Api.Commands.Orders;
using Shared.RabbitMq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : BaseController
    {
        public OrderController(IBusPublisher busPublisher) : base(busPublisher)
        {

        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateOrder command)
        {
            var context = GetContext();
            await BusPublisher.SendAsync(command, context);
            return Accepted();
        }
    }
}
