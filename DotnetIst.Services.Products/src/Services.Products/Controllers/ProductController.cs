using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Customers.Data;
using Services.Products.Data;

namespace Services.Products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductDBContext _dbContext;

        public ProductController(ProductDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            return Ok(await _dbContext.Products.FindAsync(id));
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _dbContext.Products.ToListAsync());
        }
    }
}
