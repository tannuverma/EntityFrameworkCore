using DbOperationsEFCore.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DbOperationsEFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public CurrencyController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllCurrencies()
        {
            //--both are correct--

            //method 1 
            //var result = await _appDbContext.Currencies.ToListAsync();

            //method 2(SQl query format)
            var result = await (from currencies in _appDbContext.Currencies select currencies).ToListAsync();
            return Ok(result);
        }
    }
}
