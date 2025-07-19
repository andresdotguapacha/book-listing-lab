using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookListingAPI.Data;
using BookListingAPI.Models;
using BookListingAPI.Repositories.Interfaces;

namespace BookListingAPI.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _context.Books
                .Include(b => b.Authors)
                    .ThenInclude(ba => ba.Author)
                .ToListAsync();
        }

        public async Task<Book> GetBookByIdAsync(Guid id)
        {
            return await _context.Books
                .Include(b => b.Authors)
                    .ThenInclude(ba => ba.Author)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task AddBookAsync(Book book)
        {
            if (book.Authors != null)
            {
                foreach (var bookAuthor in book.Authors)
                {
                    if (bookAuthor.Author != null)
                    {
                        var existingAuthor = await _context.Authors.FindAsync(bookAuthor.AuthorId);
                        if (existingAuthor != null)
                        {
                            bookAuthor.Author = existingAuthor;
                        }
                        else
                        {
                            _context.Authors.Add(bookAuthor.Author);
                        }
                    }
                }
            }

            _context.Books.Add(book);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateBookAsync(Book book)
        {
            var existingBook = await _context.Books
                .Include(b => b.Authors)
                    .ThenInclude(ba => ba.Author)
                .FirstOrDefaultAsync(b => b.Id == book.Id);

            if (existingBook == null)
                throw new KeyNotFoundException($"Book with ID {book.Id} not found.");

            existingBook.Title = book.Title;
            existingBook.PublicationDate = book.PublicationDate;

            var newAuthors = book.Authors != null
                ? new List<BookAuthor>(book.Authors)
                : new List<BookAuthor>();

            existingBook.Authors.Clear();

            foreach (var newLink in newAuthors)
            {
                var author = await _context.Authors.FindAsync(newLink.AuthorId);
                if (author == null)
                {
                    author = newLink.Author;
                    if (author.Id == Guid.Empty)
                        author.Id = Guid.NewGuid();
                    _context.Authors.Add(author);
                }

                existingBook.Authors.Add(new BookAuthor
                {
                    BookId = existingBook.Id,
                    AuthorId = author.Id,
                    Author = author
                });
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(Guid id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }
    }
}
