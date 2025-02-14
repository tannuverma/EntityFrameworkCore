using DbOperationsEFCore.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DbOperationsEFCore.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public BookController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost("")]
        public async Task<IActionResult> AddNewBookAsync([FromBody] Book book)
        {
            _appDbContext.Books.Add(book);
            await _appDbContext.SaveChangesAsync();
            return Ok(book);
        }

        //--inserting two or more than two records in one go--
        [HttpPost("bulk")]
        public async Task<IActionResult> AddBooksAsync([FromBody] List<Book> books)
        {
            _appDbContext.Books.AddRange(books);
            await _appDbContext.SaveChangesAsync();
            return Ok(books);
        }
    }
}
