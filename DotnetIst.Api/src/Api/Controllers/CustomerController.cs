using System.Threading.Tasks;
using Api.Commands.Customers;
using Shared.RabbitMq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : BaseController
    {
        public CustomerController(IBusPublisher busPublisher) : base(busPublisher)
        {

        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateCustomer command)
        {
            var context = GetContext(command.Id);
            await BusPublisher.SendAsync(command, context);
            return Accepted();
        }
    }
}
