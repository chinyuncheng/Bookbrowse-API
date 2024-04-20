using Microsoft.AspNetCore.Mvc;

using BookbrowseAPI.Models;
using BookbrowseAPI.Services;

namespace BookbrowseAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookbrowseController : ControllerBase
    {
        private readonly IBookDbService _bookDbService;
        private readonly ILogger<BookbrowseController> _logger;

        public BookbrowseController(ILogger<BookbrowseController> logger, IBookDbService bookDbService)
        {
            _bookDbService = bookDbService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> Get()
        {
            var books = await _bookDbService.GetBooksAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> Get(int id)
        {
            var book = await _bookDbService.GetBookAsync(id);
            return book == null ? NotFound() : Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> Post([FromBody] Book newBook)
        {
            var addedBook = await _bookDbService.AddBookAsync(newBook);
            if (addedBook == null)
                return BadRequest("The book ID is existing already.");

            return CreatedAtAction(nameof(Get), new { id = addedBook.Id }, addedBook);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Book>> Put(int id, [FromBody] Book updatedBook)
        {
            var result = await _bookDbService.UpdateBookAsync(id, updatedBook);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _bookDbService.DeleteBookAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}
