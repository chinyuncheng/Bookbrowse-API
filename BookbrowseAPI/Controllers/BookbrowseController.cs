using Microsoft.AspNetCore.Mvc;

using BookbrowseAPI.Models;
using BookbrowseAPI.Services;

namespace BookbrowseAPI.Controllers
{
    /// <summary>
    /// Represents the controller of the web API. />.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class BookbrowseController : ControllerBase
    {
        private readonly IBookDbService _bookDbService;
        private readonly ILogger<BookbrowseController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookbrowseController" /> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="bookDbService"></param>
        public BookbrowseController(ILogger<BookbrowseController> logger, IBookDbService bookDbService)
        {
            _bookDbService = bookDbService;
            _logger = logger;
        }

        /// <summary>
        /// Gets a list of existing books.
        /// </summary>
        /// <returns>A list of existing books.</returns>
        /// <response code="200">Success.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Book>>> Get()
        {
            var books = await _bookDbService.GetBooksAsync();
            return Ok(books);
        }

        /// <summary>
        /// Gets a book by ID.
        /// </summary>
        /// <param name="id">The identification of the book that wants to get.</param>
        /// <returns>The book with the ID if gets book successfully; otherwise, null.</returns>
        /// <response code="200">Success.</response>
        /// <response code="404">The book of specific ID is not existing.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Book>> Get(int id)
        {
            var book = await _bookDbService.GetBookAsync(id);
            return book == null ? NotFound() : Ok(book);
        }

        /// <summary>
        /// Creates a new book.
        /// </summary>
        /// <param name="newBook">The new book.</param>
        /// <returns>The added book if adds new book successfully; otherwise, null.</returns>
        /// <response code="201">Success.</response>
        /// <response code="400">The ID of the new book is existing already.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Book>> Post([FromBody] Book newBook)
        {
            var addedBook = await _bookDbService.AddBookAsync(newBook);
            if (addedBook == null)
                return BadRequest("The book ID is existing already.");

            return CreatedAtAction(nameof(Get), new { id = addedBook.Id }, addedBook);
        }

        /// <summary>
        /// Updates a book by ID.
        /// </summary>
        /// <param name="id">The identification of the book that wants to update.</param>
        /// <param name="updatedBook">The book with updated information.</param>
        /// <returns>The updated book if updates book successfully; otherwise, null.</returns>
        /// <response code="200">Success.</response>
        /// <response code="404">The book of specific ID is not existing.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Book>> Put(int id, [FromBody] Book updatedBook)
        {
            var result = await _bookDbService.UpdateBookAsync(id, updatedBook);
            return result == null ? NotFound() : Ok(result);
        }

        /// <summary>
        /// Deletes a book by ID.
        /// </summary>
        /// <param name="id">The identification of the book that wants to delete.</param>
        /// <returns>True if deletes the book successfully; otherwise, false.</returns>
        /// <response code="204">Success.</response>
        /// <response code="404">The book of specific ID is not existing.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _bookDbService.DeleteBookAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}
