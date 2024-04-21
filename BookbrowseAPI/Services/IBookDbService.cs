using BookbrowseAPI.Models;

namespace BookbrowseAPI.Services
{
    /// <summary>
    /// Defines base database operations for <see cref="Book"/>.
    /// </summary>
    public interface IBookDbService
    {
        /// <summary>
        /// Adds new book to database.
        /// </summary>
        /// <param name="newBook">The new book.</param>
        /// <returns>The added book if adds new book successfully; otherwise, null.</returns>
        Task<Book?> AddBookAsync(Book newBook);
        /// <summary>
        /// Deletes a book from database by ID.
        /// </summary>
        /// <param name="id">The identification of the book that wants to delete.</param>
        /// <returns>True if deletes the book successfully; otherwise, false.</returns>
        Task<bool> DeleteBookAsync(int id);
        /// <summary>
        /// Gets a book from database by ID.
        /// </summary>
        /// <param name="id">The identification of the book that wants to get.</param>
        /// <returns>The book with the ID if gets book successfully; otherwise, null.</returns>
        Task<Book?> GetBookAsync(int id);
        /// <summary>
        /// Gets a list of the existing books in database.
        /// </summary>
        /// <returns>A list of the existing books.</returns>
        Task<IEnumerable<Book>> GetBooksAsync();
        /// <summary>
        /// Updates a book from database by ID.
        /// </summary>
        /// <param name="id">The identification of the book that wants to update.</param>
        /// <param name="updatedBook">The book with updated information.</param>
        /// <returns>The updated book if updates book successfully; otherwise, null.</returns>
        Task<Book?> UpdateBookAsync(int id, Book updatedBook);
    }
}
