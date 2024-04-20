using BookbrowseAPI.Models;

namespace BookbrowseAPI.Services
{
    public interface IBookDbService
    {
        Task<Book?> AddBookAsync(Book newBook);
        Task<bool> DeleteBookAsync(int id);
        Task<Book?> GetBookAsync(int id);
        Task<IEnumerable<Book>> GetBooksAsync();
        Task<Book?> UpdateBookAsync(int id, Book updatedBook);
    }
}
