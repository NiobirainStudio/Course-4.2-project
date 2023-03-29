using CourseProject.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.Data
{
    public class DataDBContext : DbContext
    {
        public DataDBContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookAndAuthor>()
                .HasKey(
                    nameof(BookAndAuthor.BookId),
                    nameof(BookAndAuthor.AuthorId)
                );

            modelBuilder.Entity<BookAndGenre>()
                .HasKey(
                    nameof(BookAndGenre.BookId),
                    nameof(BookAndGenre.GenreId)
                );


            modelBuilder.Entity<Chapter>()
                .HasKey(
                    nameof(Chapter.BookId),
                    nameof(Chapter.BookChapterId)
                );

            modelBuilder.Entity<UserEntry>()
                .HasKey(
                    nameof(UserEntry.BookId),
                    nameof(UserEntry.EntryIp)
                );
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<UserEntry> UserEntries { get; set; }
        public DbSet<BookAndAuthor> BookAndAuthors { get; set; }
        public DbSet<BookAndGenre> BookAndGenres { get; set; }   
    }
}
