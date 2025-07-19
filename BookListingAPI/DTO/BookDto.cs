namespace BookListingAPI.DTO
{
    public class BookDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string PublicationDate { get; set; }
        public List<AuthorDto> Authors { get; set; } = new List<AuthorDto>();
    }
}