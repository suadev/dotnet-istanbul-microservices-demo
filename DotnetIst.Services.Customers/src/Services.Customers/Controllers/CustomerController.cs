using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Customers.Data;

namespace Services.Customers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerDBContext _dbContext;

        public CustomerController(CustomerDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet("customerbyemail/{email}")]
        public async Task<ActionResult> Get(string email)
        {
            return Ok(await _dbContext.Customers
                    .FirstOrDefaultAsync(s => s.Email == email));
        }
    }
}