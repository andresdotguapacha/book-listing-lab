using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookListingAPI.Models
{
    public class Book
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string PublicationDate { get; set; }
        public ICollection<BookAuthor> Authors { get; set; } = new List<BookAuthor>();
    }
}
