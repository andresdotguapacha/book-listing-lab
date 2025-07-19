using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookListingAPI.Models
{
    public class Author
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
