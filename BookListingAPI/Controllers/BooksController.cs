using System.Collections.Generic;
using System.Threading.Tasks;
using BookListingAPI.DTO;
using BookListingAPI.Models;
using BookListingAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookListingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetAllBooks()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBookById(Guid id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult> AddBookAsync([FromBody] BookDto book)
        {
            await _bookService.AddBookAsync(book);
            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookAsync(Guid id, [FromBody] BookDto book)
        {
            if (book == null)
                return BadRequest();

            book.Id = id;

            await _bookService.UpdateBookAsync(book);

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBookAsync(Guid id)
        {
            await _bookService.DeleteBookAsync(id);
            return NoContent();
        }
    }
}