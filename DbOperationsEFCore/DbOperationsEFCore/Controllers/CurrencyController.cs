using DbOperationsEFCore.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DbOperationsEFCore.Controllers
{
    [Route("api/currencies")]
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

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCurrencyByIdAsync([FromRoute] int id)
        {
            var result = await _appDbContext.Currencies.FindAsync(id);
            return Ok(result);
        }

        //[HttpGet("{name}")]
        //public async Task<IActionResult> GetCurrencyByTitleAsync([FromRoute] string name)
        //{
        //    var result = await _appDbContext.Currencies.FirstOrDefaultAsync(x => x.Title == name);
        //    return Ok(result);
        //}

        [HttpGet("{name}")]
        public async Task<IActionResult> GetCurrencyByTitleAndDescriptionAsync([FromRoute] string name, [FromQuery] string? description)
        {
            var result = await _appDbContext.Currencies
                .FirstOrDefaultAsync(x => x.Title == name
                && (string.IsNullOrEmpty(description) || x.Description == description));
            return Ok(result);
        }

        //[HttpGet("all")]
        //public async Task<IActionResult> GetCurrencyBylistOfIdAsync()
        //{
        //    var ids = new List<int> { 1, 3 };
        //    var result = await _appDbContext.Currencies
        //        .Where(x => ids.Contains(x.Id)).ToListAsync();
        //    return Ok(result);
        //}

        //--we can convert the above method as POST and also pass the ids from body--
        [HttpPost("all")]
        public async Task<IActionResult> GetCurrencyBylistOfIdAsync([FromBody] List<int> ids)
        {
            //var ids = new List<int> { 1, 3 };
            var result = await _appDbContext.Currencies
                .Where(x => ids.Contains(x.Id)).ToListAsync();
            return Ok(result);
        }
    }
}
