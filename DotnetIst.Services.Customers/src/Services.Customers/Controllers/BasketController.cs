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
    public class BasketController : ControllerBase
    {
        private readonly CustomerDBContext _dbContext;

        public BasketController(CustomerDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet("{customerId}")]
        public async Task<ActionResult> Get(Guid customerId)
        {
            return Ok(await _dbContext.Baskets.Include(i => i.Items)
                    .FirstOrDefaultAsync(s => s.CustomerId == customerId));
        }
    }
}