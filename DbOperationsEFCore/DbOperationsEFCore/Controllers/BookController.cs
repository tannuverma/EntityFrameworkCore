using DbOperationsEFCore.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        //--inserting two or more than two records in one go(bulk insert)--
        [HttpPost("bulk")]
        public async Task<IActionResult> AddBooksAsync([FromBody] List<Book> books)
        {
            _appDbContext.Books.AddRange(books);
            await _appDbContext.SaveChangesAsync();
            return Ok(books);
        }


        //--update data in two queries--
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookAsync([FromRoute] int id, [FromBody] Book model)
        {
            var book = _appDbContext.Books.FirstOrDefault(x => x.Id == id);
            if(book == null)
            {
                return NotFound();
            }
            book.NoOfPages = model.NoOfPages;

            await _appDbContext.SaveChangesAsync();
            return Ok(book);
        }

        //--update data in one query--
        //--drawback of this approach is that it will make value as null if not passed from body--
        [HttpPut("update")]
        public async Task<IActionResult> UpdateBookInSingleQueryAsync([FromBody] Book book)
        {
            _appDbContext.Books.Update(book);
            await _appDbContext.SaveChangesAsync();
            return Ok(book);
        }

        //--updating two or more than two records in one go(bulk update)--
        [HttpPut("bulkupdate")]
        public async Task<IActionResult> UpdateBookInBulkAsync()
        {
            await _appDbContext.Books
                .Where(p => p.Id>3).ExecuteUpdateAsync(x => x
            .SetProperty(p => p.Title, "Updatedd Title")
            .SetProperty(p => p.Description, p => p.Description + " Updatedd"));
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookByIdAsync([FromRoute] int id)
        {
            var book = _appDbContext.Books.FirstOrDefault(x => x.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            _appDbContext.Books.Remove(book);
            await _appDbContext.SaveChangesAsync();
            return Ok(book);
        }

        //--bulk delete--
        [HttpDelete("bulkdelete")]
        public async Task<IActionResult> DeleteBookInBulkAsync()
        {
            await _appDbContext.Books
                .Where(p => p.Id >= 4).ExecuteDeleteAsync();
            return Ok();
        }
    }
}
