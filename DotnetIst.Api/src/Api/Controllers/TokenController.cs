using System.Threading.Tasks;
using Api.Authentication;
using Api.HttpServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IAuthenticationService _authService;
        private readonly ICustomerHttpService _customerHttpService;

        public TokenController(IAuthenticationService authService,
                ICustomerHttpService customerHttpService)
        {
            _authService = authService;
            _customerHttpService = customerHttpService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> GenerateToken(LoginModel reqeust)
        {
            var customer = await _customerHttpService.GetCustomerByEmail(reqeust.Email);

            if (customer == null)
            {
                return BadRequest("Customer not found.");
            }
            if (customer.Password != reqeust.Password)
            {
                return BadRequest("Wrong password.");
            }

            return Ok(_authService.GetToken(customer));
        }
    }
}