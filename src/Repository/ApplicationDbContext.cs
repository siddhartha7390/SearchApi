using Microsoft.EntityFrameworkCore;
using Application.Core.Models.Poco;

namespace Repository
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(book =>
            {
                book.HasKey(x => x.BookId);
                book.Property(x => x.BookId).HasColumnName("Book_Id");
                book.Property(x => x.AuthorId).HasColumnName("Author_Id");
                book.Property(x => x.PublicationYear).HasColumnName("Publication_Year");
            });

            modelBuilder.Entity<Author>(author =>
            {
                author.HasKey(x => x.AuthorId);
                author.Property(x => x.AuthorId).HasColumnName("Author_Id");
                author.Property(x => x.FirstName).HasColumnName("First_Name");
                author.Property(x => x.LastName).HasColumnName("Last_Name");
                author.Property(x => x.DateOfBirth).HasColumnName("date_of_birth");
                author.HasOne(x => x.Book)
                    .WithMany(p => p.Authors)
                    .HasForeignKey(d => d.AuthorId);
            });
        }
    }
}