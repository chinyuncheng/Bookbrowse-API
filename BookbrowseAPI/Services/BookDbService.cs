using Microsoft.EntityFrameworkCore;

using BookbrowseAPI.Models;

namespace BookbrowseAPI.Services
{
    /// <summary>
    /// Defines database operations for <see cref="Book"/>.
    /// </summary>
    public class BookDbService : IBookDbService
    {
        private readonly BookDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookDbService" /> class.
        /// </summary>
        /// <param name="dbContext">Represents a session with the database and can be used to query and save instances of <see cref="Book" />.</param>
        public BookDbService(BookDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Adds new book to database.
        /// </summary>
        /// <param name="newBook">The new book.</param>
        /// <returns>The added book if adds new book successfully; otherwise, null.</returns>
        public async Task<Book?> AddBookAsync(Book newBook)
        {
            var book = await GetBookAsync(newBook.Id);
            if (book != null)
                return null;

            _dbContext.Books.Add(newBook);
            await _dbContext.SaveChangesAsync();
            return newBook;
        }

        /// <summary>
        /// Deletes a book from database by ID.
        /// </summary>
        /// <param name="id">The identification of the book that wants to delete.</param>
        /// <returns>True if deletes the book successfully; otherwise, false.</returns>
        public async Task<bool> DeleteBookAsync(int id)
        {
            var book = await GetBookAsync(id);
            if (book == null)
                return false;

            _dbContext.Books.Remove(book);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Gets a book from database by ID.
        /// </summary>
        /// <param name="id">The identification of the book that wants to get.</param>
        /// <returns>The book with the ID if gets book successfully; otherwise, null.</returns>
        public async Task<Book?> GetBookAsync(int id)
        {
            var result = await _dbContext.Books.FindAsync(id);
            return result;
        }

        /// <summary>
        /// Gets a list of the existing books in database.
        /// </summary>
        /// <returns>A list of the existing books.</returns>
        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            return await _dbContext.Books.ToListAsync();
        }

        /// <summary>
        /// Updates a book from database by ID.
        /// </summary>
        /// <param name="id">The identification of the book that wants to update.</param>
        /// <param name="updatedBook">The book with updated information.</param>
        /// <returns>The updated book if updates book successfully; otherwise, null.</returns>
        public async Task<Book?> UpdateBookAsync(int id, Book updatedBook)
        {
            var book = await GetBookAsync(id);
            if (book == null)
                return null;

            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.Year = updatedBook.Year;

            await _dbContext.SaveChangesAsync();
            return book;
        }
    }
}
