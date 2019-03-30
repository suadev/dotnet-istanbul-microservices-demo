using System.Threading.Tasks;
using Api.Commands.Customers;
using Api.HttpServices;
using Shared.RabbitMq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductHttpService _productHttpService;

        public ProductController(IProductHttpService productHttpService)
        {
            _productHttpService = productHttpService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _productHttpService.GetList());
        }
    }
}
