
using System.Collections.Generic;
using System.Threading.Tasks;

using BookListingAPI.Models;

namespace BookListingAPI.Repositories.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book> GetBookByIdAsync(Guid id);
        Task AddBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(Guid id);
    }
}