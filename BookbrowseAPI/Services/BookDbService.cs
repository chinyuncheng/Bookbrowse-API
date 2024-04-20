using Microsoft.EntityFrameworkCore;

using BookbrowseAPI.Models;

namespace BookbrowseAPI.Services
{
    public class BookDbService : IBookDbService
    {
        private readonly BookDbContext _dbContext;

        public BookDbService(BookDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Book?> AddBookAsync(Book newBook)
        {
            var book = await GetBookAsync(newBook.Id);
            if (book != null)
                return null;

            _dbContext.Books.Add(newBook);
            await _dbContext.SaveChangesAsync();
            return newBook;
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var book = await GetBookAsync(id);
            if (book == null)
                return false;

            _dbContext.Books.Remove(book);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Book?> GetBookAsync(int id)
        {
            var result = await _dbContext.Books.FindAsync(id);
            return result;
        }

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            return await _dbContext.Books.ToListAsync();
        }

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
