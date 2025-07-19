using System.Collections.Generic;
using System.Threading.Tasks;
using BookListingAPI.DTO;

namespace BookListingAPI.Services.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllBooksAsync();
        Task<BookDto> GetBookByIdAsync(Guid id);
        Task AddBookAsync(BookDto book);
        Task UpdateBookAsync(BookDto book);
        Task DeleteBookAsync(Guid id);
    }
}