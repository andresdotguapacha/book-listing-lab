using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BookListingAPI.DTO;
using BookListingAPI.Models;
using BookListingAPI.Repositories.Interfaces;
using BookListingAPI.Services.Interfaces;

namespace BookListingAPI.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllBooksAsync();
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<BookDto> GetBookByIdAsync(Guid id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            return _mapper.Map<BookDto>(book);
        }

        public async Task AddBookAsync(BookDto bookDto)
        {
            var bookEntity = _mapper.Map<Book>(bookDto);

            if (bookEntity.Id == Guid.Empty)
            {
                bookEntity.Id = Guid.NewGuid();
            }

            if (bookDto.Authors != null)
            {
                bookEntity.Authors = new List<BookAuthor>();

                foreach (var authorDto in bookDto.Authors)
                {
                    var authorEntity = _mapper.Map<Author>(authorDto);

                    if (authorEntity.Id == Guid.Empty)
                    {
                        authorEntity.Id = Guid.NewGuid();
                    }

                    bookEntity.Authors.Add(new BookAuthor
                    {
                        BookId = bookEntity.Id,
                        Book = bookEntity,
                        AuthorId = authorEntity.Id,
                        Author = authorEntity
                    });
                }
            }

            await _bookRepository.AddBookAsync(bookEntity);
        }

        public async Task UpdateBookAsync(BookDto bookDto)
        {
            var existingBook = await _bookRepository.GetBookByIdAsync(bookDto.Id);

            if (existingBook == null)
                throw new KeyNotFoundException($"Book with ID {bookDto.Id} not found.");

            existingBook.Title = bookDto.Title;
            existingBook.PublicationDate = bookDto.PublicationDate;

            var updatedAuthors = bookDto.Authors ?? new List<AuthorDto>();

            var newBookAuthors = new List<BookAuthor>();

            foreach (var authorDto in updatedAuthors)
            {
                var authorEntity = _mapper.Map<Author>(authorDto);

                if (authorEntity.Id == Guid.Empty)
                    authorEntity.Id = Guid.NewGuid();

                newBookAuthors.Add(new BookAuthor
                {
                    BookId = existingBook.Id,
                    AuthorId = authorEntity.Id,
                    Author = authorEntity
                });
            }

            existingBook.Authors = newBookAuthors;

            await _bookRepository.UpdateBookAsync(existingBook);
        }

        public async Task DeleteBookAsync(Guid id)
        {
            await _bookRepository.DeleteBookAsync(id);
        }
    }
}
