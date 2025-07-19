
using BookListingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookListingAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // === GUIDs for seeding ===
            var author1Id = Guid.Parse("e1d2c3b4-a567-8901-b234-c56789d01234");
            var author2Id = Guid.Parse("f2e3d4c5-b678-9012-c345-d67890e12345");
            var book1Id = Guid.Parse("a1b2c3d4-e567-8901-f234-567890a12345");
            var book2Id = Guid.Parse("b2c3d4e5-f678-9012-a345-678901b23456");

            // === Configure relationships ===
            modelBuilder.Entity<BookAuthor>()
                .HasKey(ba => new { ba.BookId, ba.AuthorId });

            modelBuilder.Entity<BookAuthor>()
                .HasOne(ba => ba.Book)
                .WithMany(b => b.Authors)
                .HasForeignKey(ba => ba.BookId);

            modelBuilder.Entity<BookAuthor>()
                .HasOne(ba => ba.Author)
                .WithMany(a => a.BookAuthors)
                .HasForeignKey(ba => ba.AuthorId);

            // === Seed Authors ===
            modelBuilder.Entity<Author>().HasData(
                new Author { Id = author1Id, FirstName = "George", LastName = "Orwell" },
                new Author { Id = author2Id, FirstName = "Aldous", LastName = "Huxley" }
            );

            // === Seed Books ===
            modelBuilder.Entity<Book>().HasData(
                new Book { Id = book1Id, Title = "1984", PublicationDate = "1949-06-08" },
                new Book { Id = book2Id, Title = "Brave New World", PublicationDate = "1932-01-01" }
            );

            // === Seed BookAuthor relationships ===
            modelBuilder.Entity<BookAuthor>().HasData(
                new BookAuthor { BookId = book1Id, AuthorId = author1Id },
                new BookAuthor { BookId = book2Id, AuthorId = author2Id }
            );
        }

    }
}