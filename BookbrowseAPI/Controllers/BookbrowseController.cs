using BookbrowseAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookbrowseAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookbrowseController : ControllerBase
    {
        private readonly ILogger<BookbrowseController> _logger;
        private List<Book> _books = new List<Book>();

        public BookbrowseController(ILogger<BookbrowseController> logger)
        {
            _logger = logger;

            _books = new List<Book>
            {
                new Book { Id = 1, Title = "Book 1", Author = "Author 1", Year = 2020 },
                new Book { Id = 2, Title = "Book 2", Author = "Author 2", Year = 2021 },
                new Book { Id = 3, Title = "Book 3", Author = "Author 3", Year = 2022 }
            };
        }

        [HttpGet]
        public ActionResult<IEnumerable<Book>> Get()
        {
            return Ok(_books);
        }

        [HttpGet("{id}")]
        public ActionResult<Book> Get(int id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            return book == null ? NotFound() : Ok(book);
        }

        [HttpPost]
        public ActionResult<Book> Post([FromBody] Book newBook)
        {
            _books.Add(newBook);
            return CreatedAtAction(nameof(Get), new { id = newBook.Id }, newBook);
        }

        [HttpPut("{id}")]
        public ActionResult<Book> Put(int id, [FromBody] Book updatedBook)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            if (book == null)
                return NotFound();

            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.Year = updatedBook.Year;

            return Ok(book);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            if (book == null)
                return NotFound();

            _books.Remove(book);
            return NoContent();
        }
    }
}
